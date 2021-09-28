// <copyright file="NoTenantFoundException.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Exceptions
{
    using System;

    /// <summary>
    /// No tenant found exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class NoTenantFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoTenantFoundException"/> class.
        /// </summary>
        public NoTenantFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoTenantFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NoTenantFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoTenantFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public NoTenantFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
