// <copyright file="SettingConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Vypr.Server.Settings;

    /// <summary>
    /// Setting Configuration.
    /// </summary>
    public static class SettingConfiguration
    {
        /// <summary>
        /// Adds the setting configuration.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service.</returns>
        public static IServiceCollection AddSettingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailConfig>(config => configuration.GetSection("EmailConfig").Bind(config));
            services.Configure<ApplicationConfig>(config => configuration.GetSection("ApplicationSettings").Bind(config));

            return services;
        }
    }
}
