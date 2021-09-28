// <copyright file="VyprCoreInternalGlobalExceptionHandler.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Logging.Service
{
    using Microsoft.Extensions.Logging;
    using VyprCore.Interfaces.Logging;
    using System;

    /// <summary>
    /// Global Exception Handler.
    /// </summary>
    internal class InternalGlobalExceptionHandler : ILogger
    {
        private readonly IGlobalExceptionHandler _customHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalGlobalExceptionHandler"/> class.
        /// </summary>
        /// <param name="customHandler">The custom handler.</param>
        public InternalGlobalExceptionHandler(IGlobalExceptionHandler customHandler)
        {
            _customHandler = customHandler;
        }

        /// <summary>
        /// Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState">The type of the state to begin scope for.</typeparam>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>
        /// An <see cref="T:System.IDisposable" /> that ends the logical operation scope on dispose.
        /// </returns>
        public IDisposable BeginScope<TState>(TState state) => null;

        /// <summary>
        /// Checks if the given <paramref name="logLevel" /> is enabled.
        /// </summary>
        /// <param name="logLevel">Level to be checked.</param>
        /// <returns>
        ///   <c>true</c> if enabled.
        /// </returns>
        public bool IsEnabled(LogLevel logLevel) => true;

        /// <summary>
        /// Writes a log entry.
        /// </summary>
        /// <typeparam name="TState">The type of the object to be written.</typeparam>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">Function to create a <see cref="T:System.String" /> message of the <paramref name="state" /> and <paramref name="exception" />.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // We hit this without any exception on startup which isn't really needed,
            // or application relevant so we do this check here.
            if (exception is null)
            {
                return;
            }

            // If we have been provided a custom handle then invoke it here
            // Else we fall back on the normal console pipeline.
            if (!(_customHandler is null))
            {
                _customHandler.Handle(exception);
            }
            else
            {
                Console.WriteLine($"{exception}, {exception.Message}");
            }
        }
    }
}
