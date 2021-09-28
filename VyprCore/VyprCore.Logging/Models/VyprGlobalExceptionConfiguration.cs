// <copyright file="VyprCoreGlobalExceptionConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Logging.Models
{
    using VyprCore.Interfaces.Logging;

    /// <summary>
    /// Vypr Core Global Exception Configuration Model.
    /// </summary>
    public class VyprGlobalExceptionConfiguration
    {
        /// <summary>
        /// Gets or sets the handler. If no handler is provided,
        /// the default behaviour is to pipe the exception to the console
        /// output window.
        /// </summary>
        /// <value>
        /// The handler.
        /// </value>
        public IGlobalExceptionHandler GlobalExceptionHandler;
    }
}
