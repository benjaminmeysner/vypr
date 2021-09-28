// <copyright file="NullModelException.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Exceptions
{
    using System;

    /// <summary>
    /// No such model exists exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class NullModelException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullModelException"/> class.
        /// </summary>
        public NullModelException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullModelException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NullModelException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoSuchAttributeException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public NullModelException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
