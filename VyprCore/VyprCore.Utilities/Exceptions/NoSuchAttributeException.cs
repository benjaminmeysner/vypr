// <copyright file="NoSuchAttributeException.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Exceptions
{
    using System;

    /// <summary>
    /// No such attribute exists exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class NoSuchAttributeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoSuchAttributeException"/> class.
        /// </summary>
        public NoSuchAttributeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoSuchAttributeException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NoSuchAttributeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoSuchAttributeException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public NoSuchAttributeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
