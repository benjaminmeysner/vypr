// <copyright file="VyprCoreLoggingConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Logging.Models
{
    using VyprCore.Logging.Enum;

    /// <summary>
    /// Vypr Core Logging Configuration Model.
    /// </summary>
    public class VyprLoggingConfiguration
    {
        /// <summary>
        /// Sets the Log file base destination folder for file logging.
        /// Provide the base path without the trailing slash.
        /// </summary>
        /// <remarks>
        /// You can use tokenised expressions such as ${aspnet-appbasepath}, in which
        /// case would target the project root directory.
        /// </remarks>
        /// <value>
        /// The log file destination folder.
        /// </value>
        public string LogFileDestinationFolder { get; set; }

        /// <summary>
        /// Sets the Log file prefix for file logging.
        /// For example 'myproject' would result in 'myproject_vypr-log_x.log' where x is
        /// some arbitrary date time.
        /// </summary>
        /// <value>
        /// The log file prefix.
        /// </value>
        public string LogFilePrefix { get; set; }

        /// <summary>
        /// Delete logs after x number of days. Ie. How many days should you archive the
        /// log files. This corresponds to file logging only.
        /// </summary>
        /// <value>
        /// Delete logs after x days.
        /// </value>
        public int DeleteLogsAfterXDays { get; set; }

        /// <summary>
        /// Gets or sets the global threshold.
        /// Log events below this threshold are not logged.
        /// </summary>
        /// <value>
        /// The global threshold.
        /// </value>
        public VyprLogLevel GlobalThreshold { get; set; } = VyprLogLevel.Trace;
    }
}
