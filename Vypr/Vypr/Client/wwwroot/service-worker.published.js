importScripts('https://storage.googleapis.com/workbox-cdn/releases/6.1.5/workbox-sw.js');

if (workbox) {
    console.log('Workbox loaded successfully');
} else {
    console.log('Workbox failed to load');
}

const CACHE_NAME = "vypr-cache-v1";
const CACHE_VERSION = 1;
const TRIPS_DB_NAME = "vypr-trips";
const TRIPS_DB_VERSION = 1;
const TRIPS_STORE_NAME = "trips";
var urlsToCache = [
    '/driver/trips',
    '/driver/viewtrip',
    '/driver/viewdrop',
    '/driver/acceptdelivery',
    '/driver/rejectdelivery',
    '/driver/completedeliveryproof',
    '/css/app.min.css',
    '/css/tenant/bms.css',
];

const queue = new workbox.backgroundSync.Queue('epodQueue')

self.addEventListener('install', event => {
    event.waitUntil(
        createCache()
    );
})

self.addEventListener('fetch', event => {
    //If the user is logging out, we need to clear the cache and return the fetch response.
    if (event.request.url.includes("/account/manage/logout")) {
        return fetch(event.request).then(response => {
            caches.delete(CACHE_NAME);
            createCache();
            return response;
        });
    }

    //General GET requests processing
    if (event.request.method === "GET") {
        event.respondWith(
            caches.open(CACHE_NAME).then(cache => {

                //Attempt to fetch online data before using the cache (online-first)
                return fetch(event.request).then(response => {
                    //If we get a valid response (e.g .catch promise does not get resolved) update the cache
                    //At the moment we do this for any get request
                    cache.put(event.request, response.clone());
                    return response;
                })
                    .catch(async (error) => {
                        //In case we get an error from fetch, look for something in the cache
                        var response = await cache.match(event.request);

                        //If we find nothing we need to report back the error
                        if (!response) {
                            console.warn(error.message);
                        }

                        //If we're processing a get fetch to /api/trips/me we need to do processing to properly report progress through trips
                        if (event.request.url.includes("/api/trips/me")) {
                            //Clone the response to prevent body stream from being lost
                            var responseClone = response.clone();

                            //Get the trips in json format from the cached response
                            var tripsJson = await responseClone.json();

                            //Get the failed post requests to /api/trips from the queue
                            var items = await queue.getAll();

                            for (var item of items) {
                                var request = item.request.clone();

                                //If the failed post request we're processing is to /completedrop, we need to show a matching drop as accepted or rejected
                                if (request.url.includes("/drop/complete")) {
                                    var dropJson = await request.json();

                                    for (var trip of tripsJson) {

                                        //Match the trip with the drop's trip id
                                        if (trip.id === dropJson.tripId) {

                                            //Loop through the drops
                                            for (var i = 0; i < trip.drops.length; i++) {
                                                if (trip.drops[i].dropId === dropJson.dropId) {
                                                    //Use the request's approved bool to determine whether the drop was accepted or rejected
                                                    trip.drops[i].dropStatus = dropJson.accepted ? 1 : 2;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            //Serialise the modified trip json as a blob
                            var blob = new Blob([JSON.stringify(tripsJson, null, 2)], { type: 'application/json' });

                            //Create the new response with the newly serialised trip json
                            var modifiedResponse = new Response(blob, {
                                status: response.status,
                                statusText: response.statusText,
                                headers: response.headers
                            });

                            //Return the modified response data in a promise
                            return new Promise((resolve, reject) => {
                                resolve(modifiedResponse);
                            });
                        }

                        return response;
                    });
            })
        );
    }

    //Failed POST requests to replayable endpoints
    if (event.request.method === "POST" && isReplayableEndpoint(event.request.url)) {
        //Setup Workbox background sync
        const bgSyncLogic = async () => {
            try {
                //Attempt to simply process the request
                //Todo: consider changing approach to promise .then().catch(), or modify above to match this (probably the latter)
                const response = await fetch(event.request.clone());
                return response;
            } catch (error) {
                //On failure, push the request onto the queue for processing when a sync (online functionality restored) event is triggered
                //N.B: Queue persists offline and will send as soon as the app launches if online functionality is available
                await queue.pushRequest({ request: event.request });
                return new Response(null, {
                    "status": 202, // Return status code "accepted" indicating the request has been received but not acted upon yet
                    "statusText": "Success"
                });
            }
        }

        event.respondWith(bgSyncLogic());
    }
})

function isReplayableEndpoint(url) {
    return url.includes("/api/trips") ||
        url.includes("/utilities/image") ||
        url.includes("/driver/upload");
}

function createCache() {
    caches.open(CACHE_NAME)
        .then(cache => {
            return cache.addAll(urlsToCache);
        })
}

/**
 * Register Service Worker
 */
//navigator.serviceWorker
//    .register('/***.js', { scope: '/' })
//    .then(() => {
//        console.log('Service Worker Registered');
//    });