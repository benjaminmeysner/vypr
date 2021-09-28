// <copyright file="ContextExtensions.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using VyprCore.Foundation.Classes.Context;
    using System;
    using VyprCore.Foundation.Context;
    using VyprCore.Interfaces.Context;
    using VyprCore.Models.Domain;

    /// <summary>
    /// Extensions for project contexts.
    /// </summary>
    public static class ContextExtensions
    {
        /// <summary>
        /// Adds core application context to the project. Configurable with options.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationContext(this IServiceCollection services, Action<ApplicationContextConfiguration> config = null)
        {
            if (!(config is null))
            {
                var appCtxConfig = new ApplicationContextConfiguration();
                config(appCtxConfig); // TODO!
            }

            return services.AddScoped<IApplicationContext<VyprUser>, ApplicationContext>();
        }

        /// <summary>
        /// Adds vypr core db context to the project.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddVyprDbContext<TDbContext>(this IServiceCollection services)
            where TDbContext : VyprDbContext
        {
            return services.AddTransient<VyprDbContext, TDbContext>();
        }
    }
}
