// <copyright file="IVyprLogger.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Logging
{
    using NLog;
    using System;

    /// <summary>
    /// IVyprLogger Logging service interface.
    /// </summary>
    public interface IVyprLogger : IDisposable
    {
        /// <summary>
        /// Gets the a specific logger of the provided type <see cref="GetLogger{TLogType}"/>.
        /// </summary>
        /// <typeparam name="TLogType">The type of the log type.</typeparam>
        /// <returns>The logger.</returns>
        public ILogger GetLogger<TLogType>()
            where TLogType : ILogType;

        /// <summary>
        /// Turns the logging On or Off for all logging targets.
        /// </summary>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        public void SetEnabled(bool enabled);

        /// <summary>
        /// Gets or sets the Console Logger.
        /// </summary>
        /// <value>
        /// The console logger.
        /// </value>
        public ILogger Console { get; }

        /// <summary>
        /// Gets or sets the File Logger.
        /// </summary>
        /// <value>
        /// The file logger.
        /// </value>
        public ILogger File { get; }

        /// <summary>
        /// Gets the database logger.
        /// </summary>
        /// <value>
        /// The database logger.
        /// </value>
        public ILogger Database { get; }

        /// <summary>
        /// Gets the email logger.
        /// </summary>
        /// <value>
        /// The email logger.
        /// </value>
        public ILogger Email { get; }
    }
}
