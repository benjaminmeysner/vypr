// <copyright file="IApiClientComponent.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Client
{
    /// <summary>
    /// Interface which describes classes/components which have functionality
    /// which interacts with the server API. For example, this will allow components
    /// to show loading/spinners on async calls to the API.
    /// </summary>
    public interface IApiClientComponent
    {
        /// <summary>
        /// Gets or sets a value indicating whether the request to the server API is in progress.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [request in progress]; otherwise, <c>false</c>.
        /// </value>
        public bool WaitingOnResponse { get; }

        /// <summary>
        /// Begins the request.
        /// </summary>
        public void BeginRequest();

        /// <summary>
        /// Ends the request.
        /// </summary>
        public void EndRequest();
    }
}
