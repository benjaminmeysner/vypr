// <copyright file="VyprMemoryCache.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Caching
{
    using Microsoft.Extensions.Caching.Memory;
    using VyprCore.Interfaces.Cache;
    using VyprCore.Interfaces.Logging;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr In Memory Caching
    /// </summary>
    public class VyprMemoryCache : IInMemoryCache
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        private IVyprLogger Logger { get; }

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <value>
        /// The cache.
        /// </value>
        private MemoryCache Cache { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        private VyprCacheConfiguration Configuration { get; }

        /// <summary>
        /// Gets the read/write lock.
        /// </summary>
        /// <value>
        /// The lock.
        /// </value>
        private ReaderWriterLockSlim Lock { get; }

        /// <summary>
        /// Gets the lock timeout.
        /// </summary>
        /// <value>
        /// The lock timeout.
        /// </value>
        private int LockTimeout { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprMemoryCache"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="logger">The logger.</param>
        public VyprMemoryCache(VyprCacheConfiguration config, IVyprLogger logger)
        {
            Configuration = config;
            Logger = logger;

            Lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            LockTimeout = Configuration.LockTimeout;

            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = Configuration.SizeLimit
            });
        }

        /// <summary>
        /// Gets the cached item count.
        /// </summary>
        /// <value>
        /// The cached item count.
        /// </value>
        public int CachedItemCount
        {
            get
            {
                try
                {
                    Lock.EnterReadLock();

                    return Cache.Count;
                }
                catch (Exception ex)
                {
                    Logger.File.Error(ex, "Failed to read cache count.");
                }
                finally
                {
                    if (Lock.IsReadLockHeld)
                        Lock.ExitReadLock();
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public (bool Result, object Value) GetItem(string key)
        {
            return GetItem<object>(key);
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <typeparam name="T">The expected type of the item.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public (bool Result, T Value) GetItem<T>(string key)
        {
            try
            {
                if (Lock.IsWriteLockHeld)
                {
                    return (false, default(T));
                }

                if (!Lock.IsReadLockHeld && Lock.TryEnterReadLock(LockTimeout))
                {
                    return (true, Cache.Get<T>(key));
                }
            }
            catch (Exception ex)
            {
                Logger.File.Error(ex, $"Failed to read cache for key '{key}'.");
            }
            finally
            {
                if (Lock.IsReadLockHeld)
                    Lock.ExitReadLock();
            }
            return (false, default(T));
        }

        /// <summary>
        /// Gets an arbitrary item from the cache if available, otherwise use getMethod to retrieve and store the item.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uid">The unique identifier that will be used as part of the key to retrieve this item.</param>
        /// <param name="getMethod">The method used to retrieve and store the item if not already present in the cache.</param>
        /// <param name="callerName">The name of the caller method used as the second part of the key to retrieve this item.</param>
        /// <returns>The item requested.</returns>
        public async Task<TResult> GetOrCreateAsync<TResult>(string uid, Func<Task<TResult>> getMethod, [CallerMemberName] string callerName = "")
        {
            TResult result;
            var cacheKey = GetCacheKey(uid, callerName);
            var cacheItem = this.GetItem<TResult>(cacheKey);

            // Result can be false if the cache is locked, a null value indicates the item has not been stored
            if (!cacheItem.Result || cacheItem.Value == null)
            {
                result = await getMethod();

                // Update the cache with the new result
                this.SetItem(cacheKey, result);
            }
            else
            {
                result = cacheItem.Value;
            }
            return result;
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="key">The key.</param>
        public void RemoveItem(string key)
        {
            try
            {
                Lock.EnterWriteLock();

                Cache.Remove(key);
            }
            catch (Exception ex)
            {
                Logger.File.Error(ex, $"Failed to invalidate item by key '{key}'.");
            }
            finally
            {
                if (Lock.IsWriteLockHeld)
                    Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Set an item in the cache.
        /// </summary>
        /// <typeparam name="T">The type of the object to insert into the cache.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="ArgumentNullException">key</exception>
        /// <exception cref="ArgumentException">Key belongs to key exclusion list and is not valid. - key</exception>
        public void SetItem<T>(string key, T item, MemoryCacheEntryOptions options = null)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException(nameof(key));

                if (!Exists(key))
                {
                    if (Configuration.CacheKeyExclusionList.Any(exKey => key.Contains(exKey)))
                    {
                        throw new ArgumentException("Key belongs to key exclusion list and is not valid.", nameof(key));
                    }

                    if (!Lock.IsWriteLockHeld && Lock.TryEnterWriteLock(LockTimeout))
                    {
                        options ??= GetCacheEntryOptions();
                        Cache.Set(key, item, options);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.File.Error(ex, $"Failed to add item '{item}' with key '{key}' to cache.");
            }
            finally
            {
                if (Lock.IsWriteLockHeld)
                    Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Purges the cache.
        /// </summary>
        /// <remarks>
        /// Compacts the Cache by 100% i.e removing all items.
        /// </remarks>
        public void Purge()
        {
            Cache.Compact(1.0);
        }

        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <param name="callerName">Name of the calling function.</param>
        /// <returns></returns>
        private static string GetCacheKey(string uid, [CallerMemberName] string callerName = "")
        {
            var unhashedKey = GetUnhashedKey(uid, callerName);
            using var sha1 = new System.Security.Cryptography.SHA1Managed();

            var hash = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(unhashedKey));
            return string.Concat(hash.Select(b => b.ToString("X2"))); //Convert bytes to all caps hex representation
        }

        private static string GetUnhashedKey(string uid, string callerName)
        {
            return $"{uid}_{callerName}";
        }

        /// <summary>
        /// Existses the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private bool Exists(string key)
        {
            try
            {
                return Cache.TryGetValue(key, out var _);
            }
            catch (Exception ex)
            {
                Logger.File.Error(ex, $"Failed to read cache for key '{key}'.");
                throw;
            }
            finally
            {
                if (Lock.IsReadLockHeld)
                    Lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Gets the cache entry options.
        /// </summary>
        /// <returns></returns>
        private MemoryCacheEntryOptions GetCacheEntryOptions()
        {
            var entryOptions = new MemoryCacheEntryOptions();

            if (Configuration.SlidingExpiration.HasValue)
                entryOptions = entryOptions.SetSlidingExpiration(Configuration.SlidingExpiration.Value);

            if (Configuration.TimeToLive.HasValue)
                entryOptions = entryOptions.SetAbsoluteExpiration(Configuration.TimeToLive.Value);

            entryOptions = entryOptions.RegisterPostEvictionCallback(RemovedCallback);

            return entryOptions;
        }

        /// <summary>
        /// Removeds the callback.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="state">The state.</param>
        private void RemovedCallback(object key, object value, EvictionReason reason, object state)
        {
            if (reason != EvictionReason.Removed)
                if (value is IDisposable item)
                    item.Dispose();
        }
    }
}
