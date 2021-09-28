// <copyright file="FrameworkConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Configuration
{
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using VyprCore.Extensions;
    using Vypr.Server.Extensions;
    using VyprCore.Logging.Enum;
    using Vypr.Server.Data;

    /// <summary>
    /// Framework configurations.
    /// </summary>
    public static class FrameworkConfiguration
    {
        /// <summary>
        /// Add vypr server framework configuration.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The config.</param>
        /// <param name="useWebAuthn">Whether or not too use passwordless access to the app.</param>
        /// <returns>The service container.</returns>
        public static IServiceCollection AddFrameworkConfiguration(this IServiceCollection services, IConfiguration configuration, bool useWebAuthn = false)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddDbContext<VyprDbContext>();
            services.AddWebAuthn<VyprDbContext>(configuration);
            services.AddVyprLogging();
            services.AddBlazoredLocalStorage();
            services.AddFoundationControllers();
            services.AddIdentityConfiguration(addSwaggerPolicy: true, defaultWebAuthLogin: true);
            services.AddApplicationContext();

            return services;
        }

        /// <summary>
        /// Uses the framework configuration.
        /// </summary>
        /// <param name="webBuilder">The web builder.</param>
        /// <returns>web builder.</returns>
        public static IWebHostBuilder UseFrameworkConfiguration(this IWebHostBuilder webBuilder)
        {
            webBuilder.ConfigureCoreLogging(options =>
             {
                 options.LogFilePrefix = @"EPOD";
                 options.LogFileDestinationFolder = @"${aspnet-appbasepath}";
                 options.GlobalThreshold = VyprLogLevel.Trace;
                 options.DeleteLogsAfterXDays = 30;
             });

            webBuilder.ConfigureGlobalExceptionHandling(options =>
            {
                options.GlobalExceptionHandler = new GlobalExceptionHandler();
            });

            return webBuilder;
        }
    }
}
