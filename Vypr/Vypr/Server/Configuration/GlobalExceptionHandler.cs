// <copyright file="GlobalExceptionHandler.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Configuration
{
    using System;
    using VyprCore.Interfaces.Logging;
    using VyprCore.Logging.Service;

    /// <summary>
    /// Global Exception Handler for EPOD.
    /// </summary>
    /// <seealso cref="VyprCore.Logging.Interfaces.IGlobalExceptionHandler" />
    public sealed class GlobalExceptionHandler : IGlobalExceptionHandler
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly IVyprLogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalExceptionHandler"/> class.
        /// </summary>
        public GlobalExceptionHandler()
        {
            // We can create a new instance of the logger here
            // As at this point it hasnt been added to the service collection.
            _logger = new VyprLogger();
        }

        /// <summary>
        /// Occurs when an [unhandled exception thrown].
        /// </summary>
        /// <param name="e">The exception.</param>
        public void Handle(Exception e)
        {
            _logger.Console.Fatal(e.ToString());
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Shut down any open logging targets
            _logger.Dispose();
        }
    }
}
