// <copyright file="IVyprMemoryCache.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Cache
{
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Memory Cache interface
    /// </summary>
    public interface IInMemoryCache
    {
        /// <summary>
        /// Gets the cached item count.
        /// </summary>
        /// <value>
        /// The cached item count.
        /// </value>
        int CachedItemCount { get; }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The result and the value. Result is only false if the cache is locked.</returns>
        (bool Result, object Value) GetItem(string key);

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <typeparam name="T">The expected type of the item.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        (bool Result, T Value) GetItem<T>(string key);

        /// <summary>
        /// Get or create an entry in the cache asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uid">The unique identifier used as the first part of the cache key.</param>
        /// <param name="getMethod">The function that will retrieve the item in case it does not exist in the cache.</param>
        /// <param name="callerName">The name of the method caller, used as the second part of the cache key.</param>
        /// <returns>The item found using the given cache key parts.</returns>
        Task<TResult> GetOrCreateAsync<TResult>(string uid, Func<Task<TResult>> getMethod, string callerName = "");

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="key">The key.</param>
        void RemoveItem(string key);

        /// <summary>
        /// Set an item in the cache.
        /// </summary>
        /// <typeparam name="T">The type of the object to insert into the cache.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="options">The options.</param>
        void SetItem<T>(string key, T item, MemoryCacheEntryOptions options = null);

        /// <summary>
        /// Purges the cache.
        /// </summary>
        void Purge();
    }
}
