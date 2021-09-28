// <copyright file="IGlobalExceptionHandler.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Logging
{
    using System;

    /// <summary>
    /// Vypr Global Exception Handler Interface.
    /// </summary>
    public interface IGlobalExceptionHandler : IDisposable
    {
        /// <summary>
        /// Occurs when an [unhandled exception thrown].
        /// </summary>
        public void Handle(Exception e);
    }
}
