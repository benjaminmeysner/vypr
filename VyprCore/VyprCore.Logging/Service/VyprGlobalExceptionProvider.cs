// <copyright file="VyprCoreGlobalExceptionProvider.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Logging.Service
{
    using Microsoft.Extensions.Logging;
    using VyprCore.Interfaces.Logging;

    /// <summary>
    /// Vypr Core Global Exception Provider.
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Logging.ILoggerProvider" />
    public class VyprGlobalExceptionProvider : ILoggerProvider
    {
        /// <summary>
        /// The handler.
        /// </summary>
        public readonly IGlobalExceptionHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprGlobalExceptionProvider"/> class.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public VyprGlobalExceptionProvider(IGlobalExceptionHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance.
        /// </summary>
        /// <param name="categoryName">The category name for messages produced by the logger.</param>
        /// <returns>
        /// The instance of <see cref="T:Microsoft.Extensions.Logging.ILogger" /> that was created.
        /// </returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new InternalGlobalExceptionHandler(_handler);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
        }
    }
}
