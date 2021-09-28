// <copyright file="LoggingExtensions.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Extensions
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NLog;
    using NLog.Web;
    using VyprCore.Logging.Enum;
    using VyprCore.Interfaces.Logging;
    using VyprCore.Logging.Models;
    using VyprCore.Logging.Service;
    using LogLevel = Microsoft.Extensions.Logging.LogLevel;

    /// <summary>
    /// Logging Extensions.
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Configures the application to use Vypr Core framework logging.
        /// This logging module provides debug, file, database and email logging.
        /// </summary>
        /// <param name="hostBuilder">The web builder.</param>
        /// <param name="config">Vypr Logging congiguration.</param>
        /// <returns>Web host builder.</returns>
        public static IWebHostBuilder ConfigureCoreLogging(this IWebHostBuilder hostBuilder, Action<VyprLoggingConfiguration> config)
        {
            return hostBuilder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);

                var loggingOptions = new VyprLoggingConfiguration();
                config(loggingOptions);

                LogManager.GlobalThreshold = VyprLogLevelToNLogLevel(loggingOptions.GlobalThreshold);
                LogManager.Configuration.Variables["archivedays"] = loggingOptions.DeleteLogsAfterXDays.ToString();
                LogManager.Configuration.Variables["basedir"] = loggingOptions.LogFileDestinationFolder;
                LogManager.Configuration.Variables["prefix"] = loggingOptions.LogFilePrefix;
            })
            .UseNLog();  // NLog: setup NLog for Dependency injection
        }

        /// <summary>
        /// Configures the Vypr core global exception handling.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="config">The configuration.</param>
        /// <returns>Web host builder.</returns>
        public static IWebHostBuilder ConfigureGlobalExceptionHandling(this IWebHostBuilder hostBuilder, Action<VyprGlobalExceptionConfiguration> config)
        {
            return hostBuilder.ConfigureLogging(logging =>
            {
                var loggingOptions = new VyprGlobalExceptionConfiguration();
                config(loggingOptions);

                var provider = new VyprGlobalExceptionProvider(loggingOptions.GlobalExceptionHandler);

                logging.AddProvider(provider);
            });
        }

        /// <summary>
        /// Adds the Vypr core logging dependencies. The <see cref="IVyprLogger"/> interface can now be injected in and
        /// the <see cref="VyprLogger.GetLogger(VyprLogType)"/> function can be used
        /// to resolve the appropriate logging instance.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddVyprLogging(this IServiceCollection services)
        {
            return services.AddTransient<IVyprLogger, VyprLogger>();
        }

        /// <summary>
        /// Vyprs the core log level to n log level.
        /// </summary>
        /// <param name="globalThreshold">The global threshold.</param>
        /// <returns></returns>
        private static NLog.LogLevel VyprLogLevelToNLogLevel(VyprLogLevel globalThreshold)
            => globalThreshold switch
            {
                VyprLogLevel.Trace => NLog.LogLevel.Trace,
                VyprLogLevel.Info => NLog.LogLevel.Info,
                VyprLogLevel.Debug => NLog.LogLevel.Debug,
                VyprLogLevel.Warning => NLog.LogLevel.Warn,
                VyprLogLevel.Error => NLog.LogLevel.Error,
                VyprLogLevel.Exception => NLog.LogLevel.Fatal,
                _ => throw new ArgumentException($"Unknown logging type", nameof(globalThreshold)),
            };
    }
}
