// <copyright file="IQueryRoute.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Client
{
    /// <summary>
    /// Query Api route interface.
    /// </summary>
    public interface IQueryRoute
    {
        /// <summary>
        /// Gets the query route.
        /// </summary>
        /// <value>
        /// The query route.
        /// </value>
        public string QueryRoute { get; }
    }
}
