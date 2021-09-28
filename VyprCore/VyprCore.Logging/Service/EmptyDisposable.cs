// <copyright file="EmptyDisposable.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Logging.Service
{
    using System;

    /// <summary>
    /// An empty disposable class.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class EmptyDisposable : IDisposable
    {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
        }
    }
}
