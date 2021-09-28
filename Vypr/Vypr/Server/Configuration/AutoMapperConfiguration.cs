// <copyright file="AutoMapperConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Configuration
{
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Vypr.Server.Extensions;

    /// <summary>
    /// AutoMapper configurations.
    /// </summary>
    public static class AutoMapperConfiguration
    {
        /// <summary>
        /// Adds the AutoMapper Configuration used by the application.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(AutomapperExtensions.GetAutoMapperProfilesFromAllAssemblies().ToArray());

            return services;
        }
    }
}
