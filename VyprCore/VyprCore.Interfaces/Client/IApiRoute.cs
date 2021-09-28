// <copyright file="ClientApiAdmin.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Client
{
    /// <summary>
    /// Api route interface.
    /// </summary>
    public interface IApiRoute
    {
        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <value>
        /// The route.
        /// </value>
        public string Route { get; }
    }
}
