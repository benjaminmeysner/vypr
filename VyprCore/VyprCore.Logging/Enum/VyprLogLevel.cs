// <copyright file="VyprCoreLogLevel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Logging.Enum
{
    /// <summary>
    /// Vypr Logging Levels.
    /// </summary>
    public enum VyprLogLevel
    {
        /// <summary>
        /// The off log level. Not enabled.
        /// </summary>
        Off,

        /// <summary>
        /// The trace log level.
        /// </summary>
        Trace,

        /// <summary>
        /// The information log level.
        /// </summary>
        Info,

        /// <summary>
        /// The debug log level.
        /// </summary>
        Debug,

        /// <summary>
        /// The warning log level.
        /// </summary>
        Warning,

        /// <summary>
        /// The error log level.
        /// </summary>
        Error,

        /// <summary>
        /// The exception log level.
        /// </summary>
        Exception,
    }
}
