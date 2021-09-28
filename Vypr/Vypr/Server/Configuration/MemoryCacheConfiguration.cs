// <copyright file="MemoryCacheConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Configuration
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Vypr.Server.Caching;
    using VyprCore.Interfaces.Cache;

    /// <summary>
    /// Memory Cache configuration extension.
    /// </summary>
    public static class MemoryCacheConfiguration
    {
        /// <summary>
        /// Adds the memory caching.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The modified services.</returns>
        public static IServiceCollection AddMemoryCachingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Use bind/singleton method because it's objectively better.
            var configSection = new VyprCacheConfiguration()
            {
                LockTimeout = 1000,
                SlidingExpiration = TimeSpan.FromMinutes(5),
                TimeToLive = TimeSpan.FromMinutes(30)
            };
            configuration.Bind(VyprCacheConfiguration.CacheConfiguration, configSection);
            services.AddSingleton(configSection);

            services.AddSingleton<IInMemoryCache, VyprMemoryCache>();
            return services;
        }
    }
}
