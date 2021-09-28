/// <summary>
/// Called when [webAuthn_isUserVerifyingPlatformAuthenticatorAvailable].
/// </summary>
/// <param name="model">The model.</param>
async function webAuthn_isUserVerifyingPlatformAuthenticatorAvailable() {
    if (window.PublicKeyCredential) {
        return PublicKeyCredential.isUserVerifyingPlatformAuthenticatorAvailable();
    }
    return false;
}

/// <summary>
/// Called when [valid form submit].
/// </summary>
/// <param name="model">The model.</param>
async function webAuthn_register(makeCredentialOptions) {
    makeCredentialOptions.attestation = 'direct';
    makeCredentialOptions.authenticatorSelection.requireResidentKey = false;
    makeCredentialOptions.authenticatorSelection.userVerification = 'required';
    makeCredentialOptions.authenticatorSelection.authenticatorAttachment = 'platform';
    Array.prototype.forEach.call(makeCredentialOptions.pubKeyCredParams, x => {
        x.type = 'public-key';
    });
    Array.prototype.forEach.call(makeCredentialOptions.excludeCredentials, x => {
        x.type = 'public-key';
        x.transports = ["internal"]
    });

    makeCredentialOptions.challenge = coerceToArrayBuffer(makeCredentialOptions.challenge);
    makeCredentialOptions.user.id = coerceToArrayBuffer(makeCredentialOptions.user.id);
    makeCredentialOptions.excludeCredentials = makeCredentialOptions.excludeCredentials.map((c) => {
        c.id = coerceToArrayBuffer(c.id);
        return c;
    });

    if (makeCredentialOptions.authenticatorSelection.authenticatorAttachment === null)
        makeCredentialOptions.authenticatorSelection.authenticatorAttachment = undefined;

    let newCredential;
    try { newCredential = await navigator.credentials.create({ publicKey: makeCredentialOptions }); } catch (e) { console.log(e); return false; }
    try { return createCredentialModel(newCredential); } catch (err) { return null; }
}

/// <summary>
/// Called when [valid form submit].
/// </summary>
/// <param name="model">The model.</param>
async function createCredentialModel(newCredential) {
    let attestationObject = new Uint8Array(newCredential.response.attestationObject);
    let clientDataJSON = new Uint8Array(newCredential.response.clientDataJSON);
    let rawId = new Uint8Array(newCredential.rawId);

    const data = {
        id: newCredential.id,
        rawId: btoa(String.fromCharCode.apply(null, rawId)),
        type: newCredential.type,
        extensions: newCredential.getClientExtensionResults(),
        response: {
            AttestationObject: btoa(String.fromCharCode.apply(null, attestationObject)),
            clientDataJson: btoa(String.fromCharCode.apply(null, clientDataJSON))
        }
    };

    return JSON.stringify(data);
}



/// <summary>
/// Called when [valid sign-in form submit].
/// </summary>
/// <param name="model">The model.</param>
async function webAuthn_assert(makeAssertionOptions) {
    const challenge = makeAssertionOptions.challenge.replace(/-/g, "+").replace(/_/g, "/");
    makeAssertionOptions.challenge = Uint8Array.from(atob(challenge), c => c.charCodeAt(0));
    makeAssertionOptions.userVerification = 'required';
    makeAssertionOptions.extensions.authenticatorSelection = { requireResidentKey: false, userVerification: 'required', authenticatorAttachment: 'platform' };
    makeAssertionOptions.extensions.biometricAuthenticatorPerformanceBounds = { "far": 3.4028235e+38, "frr": 3.4028235e+38 }
    Array.prototype.forEach.call(makeAssertionOptions.allowCredentials, x => {
        var fixedId = x.id.replace(/\_/g, "/").replace(/\-/g, "+");
        x.id = Uint8Array.from(atob(fixedId), c => c.charCodeAt(0));
        x.type = 'public-key';
        x.transports = ["internal"];
    });

    let credential;
    try { credential = await navigator.credentials.get({ publicKey: makeAssertionOptions })} catch (err) { return null; }
    return await createAssertionModel(credential);
}

/// <summary>
/// Verify assertion with server.
/// </summary>
/// <param name="model">The model.</param>
async function webAuthn_emptyAllowedCredentials(makeAssertionOptions) {
    return (makeAssertionOptions.allowCredentials === undefined || makeAssertionOptions.allowCredentials.length == 0);
}

/// <summary>
/// Verify assertion with server.
/// </summary>
/// <param name="model">The model.</param>
async function createAssertionModel(assertedCredential) {
    let authData = new Uint8Array(assertedCredential.response.authenticatorData);
    let clientDataJSON = new Uint8Array(assertedCredential.response.clientDataJSON);
    let rawId = new Uint8Array(assertedCredential.rawId);
    let sig = new Uint8Array(assertedCredential.response.signature);

    const data = {
        id: coerceToBase64Url(rawId),
        rawId: coerceToBase64Url(rawId),
        type: assertedCredential.type,
        extensions: assertedCredential.getClientExtensionResults(),
        response: {
            authenticatorData: coerceToBase64Url(authData),
            clientDataJson: coerceToBase64Url(clientDataJSON),
            signature: coerceToBase64Url(sig)
        }
    };

    return JSON.stringify(data);
}

//
// Helper functions
//
coerceToArrayBuffer = function (thing, name) {
    if (typeof thing === "string") {
        // base64url to base64
        thing = thing.replace(/-/g, "+").replace(/_/g, "/");

        // base64 to Uint8Array
        var str = window.atob(thing);
        var bytes = new Uint8Array(str.length);
        for (var i = 0; i < str.length; i++) {
            bytes[i] = str.charCodeAt(i);
        }
        thing = bytes;
    }

    // Array to Uint8Array
    if (Array.isArray(thing)) {
        thing = new Uint8Array(thing);
    }

    // Uint8Array to ArrayBuffer
    if (thing instanceof Uint8Array) {
        thing = thing.buffer;
    }

    // error if none of the above worked
    if (!(thing instanceof ArrayBuffer)) {
        throw new TypeError("could not coerce '" + name + "' to ArrayBuffer");
    }

    return thing;
};


coerceToBase64Url = function (thing) {
    // Array or ArrayBuffer to Uint8Array
    if (Array.isArray(thing)) {
        thing = Uint8Array.from(thing);
    }

    if (thing instanceof ArrayBuffer) {
        thing = new Uint8Array(thing);
    }

    // Uint8Array to base64
    if (thing instanceof Uint8Array) {
        var str = "";
        var len = thing.byteLength;

        for (var i = 0; i < len; i++) {
            str += String.fromCharCode(thing[i]);
        }
        thing = window.btoa(str);
    }

    if (typeof thing !== "string") {
        throw new Error("could not coerce to string");
    }

    // base64 to base64url
    // NOTE: "=" at the end of challenge is optional, strip it off here
    //thing = thing.replace(/\+/g, "-").replace(/\//g, "_").replace(/=*$/g, "");

    return thing;
};

/// <summary>
/// Determines whether the device is able to support web authn.
/// </summary>
function isWebAuthnSupportingDevice() {
    return typeof (PublicKeyCredential) != "undefined";
}

async function debug_log(exception) {
    console.log(exception);
}

/**
 * 
 * Get a form value
 * @param {any} selector
 */
function value(selector) {
    var el = document.querySelector(selector);
    if (el.type === "checkbox") {
        return el.checked;
    }
    return el.value;
}