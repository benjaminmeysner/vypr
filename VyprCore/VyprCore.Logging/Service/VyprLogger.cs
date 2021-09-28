// <copyright file="VyprCoreLogger.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Logging.Service
{
    using System;
    using NLog;
    using VyprCore.Interfaces.Logging;

    /// <summary>
    /// Vypr Logger.
    /// </summary>
    public class VyprLogger : IVyprLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprLogger"/> class.
        /// </summary>
        public VyprLogger()
        {
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// The logger.
        /// </returns>
        public ILogger GetLogger<TLogType>() where TLogType : ILogType
        {
            return LogManager.GetLogger(ILogTypeToNLogRuleName<TLogType>());
        }

        /// <summary>
        /// Turns the logging On or Off for all logging targets.
        /// </summary>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        public void SetEnabled(bool enabled)
        {
            if (enabled && !LogManager.IsLoggingEnabled())
            {
                LogManager.EnableLogging();
                return;
            }

            if (!enabled && LogManager.IsLoggingEnabled())
            {
                LogManager.DisableLogging();
                return;
            }

            // Other cases require no action.
        }

        public ILogger Console => LogManager.GetLogger(ILogTypeToNLogRuleName<ConsoleLog>());

        public ILogger File => LogManager.GetLogger(ILogTypeToNLogRuleName<FileLog>());

        public ILogger Database => LogManager.GetLogger(ILogTypeToNLogRuleName<DatabaseLog>());

        public ILogger Email => LogManager.GetLogger(ILogTypeToNLogRuleName<EmailLog>());

        /// <summary>
        /// Converts log type to NLog configured name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>NLog logging configured name.</returns>
        private static string ILogTypeToNLogRuleName<TLogType>()
            where TLogType : ILogType
            => typeof(TLogType) switch
            {
                Type consoleType when consoleType == typeof(ConsoleLog) => "VyprConsoleLog",
                Type fileType when fileType == typeof(FileLog) => "VyprFileLog",
                Type emailType when emailType == typeof(EmailLog) => "VyprEmailLog",
                Type databaseType when databaseType == typeof(DatabaseLog) => "VyprDatabaseLog",
                _ => throw new ArgumentException($"Unknown logging type", nameof(TLogType)),
            };

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            LogManager.Flush();
            LogManager.Shutdown();
        }
    }
}
