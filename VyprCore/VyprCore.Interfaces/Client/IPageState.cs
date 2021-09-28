// <copyright file="IPageHistoryState.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Client
{
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;

    /// <summary>
    /// IPageHistoryState.
    /// </summary>
    public interface IPageState
    {
        /// <summary>
        /// Adds the page to history.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        public Task AddAsync(string pageName);

        /// <summary>
        /// Gets the go back page.
        /// </summary>
        /// <returns></returns>
        public Task<string> GetPreviousAsync();

        /// <summary>
        /// Determines whether user [can go back].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this user [can go back]; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGoBack();

        /// <summary>
        /// Gets or sets the change.
        /// </summary>
        /// <value>
        /// The change.
        /// </value>
        public EventCallback Change { get; set; }
    }
}
