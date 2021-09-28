// <copyright file="VyprCacheConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Caching
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Memory Cache configuration section
    /// </summary>
    public class VyprCacheConfiguration
    {
        public const string CacheConfiguration = "CacheConfiguration";

        /// <summary>
        /// Gets or sets the cache key exclusion list.
        /// </summary>
        /// <value>
        /// The cache key exclusion list.
        /// </value>
        public List<string> CacheKeyExclusionList { get; set; } = new();

        /// <summary>
        /// Gets or sets the lock timeout.
        /// </summary>
        /// <value>
        /// The lock timeout.
        /// </value>
        public int LockTimeout { get; set; }

        /// <summary>
        /// Gets or sets the size limit.
        /// </summary>
        /// <value>
        /// The size limit.
        /// </value>
        public long? SizeLimit { get; set; }

        /// <summary>
        /// Gets or sets the sliding expiration.
        /// </summary>
        /// <value>
        /// The sliding expiration.
        /// </value>
        public TimeSpan? SlidingExpiration { get; set; }

        /// <summary>
        /// Gets or sets the time to live.
        /// </summary>
        /// <value>
        /// The time to live.
        /// </value>
        public TimeSpan? TimeToLive { get; set; }
    }
}
