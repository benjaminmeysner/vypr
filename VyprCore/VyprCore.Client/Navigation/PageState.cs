// <copyright file="PageHistoryState.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Navigation
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.Interfaces.Client;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Manages the page history of the components, allows navigational
    /// actions based on this. TODO: This is currently unfinished/untested,
    /// there may be a better way to do this with Queue/Stack.
    /// </summary>
    /// <seealso cref="VyprCore.Client.Interfaces.IPageHistoryState" />
    public class PageState : IPageState
    {
        private readonly Stack<string> _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageState"/> class.
        /// </summary>
        public PageState()
        {
            _state = new Stack<string>();
        }

        /// <summary>
        /// Adds the page to history.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        public async Task AddAsync(string pageName)
        {
            if (_state != null && (!_state.Any() || _state.Peek() != pageName))
            {
                _state.Push(pageName);

                if (Change.HasDelegate)
                {
                    await Change.InvokeAsync();
                }
            }
        }

        /// <summary>
        /// Gets the go back page.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetPreviousAsync()
        {
            if (_state.Count > 1)
            {
                // Pop the last added page
                _state.Pop();

                if (Change.HasDelegate)
                {
                    await Change.InvokeAsync();
                }

                return _state.Peek();
            }

            // Can't go back because you didn't navigate enough
            return _state.FirstOrDefault();
        }

        /// <summary>
        /// Determines whether user [can go back].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this user [can go back]; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGoBack()
        {
            return _state.Count > 1;
        }

        /// <summary>
        /// Gets or sets the render callback.
        /// </summary>
        /// <value>
        /// The render callback.
        /// </value>
        public EventCallback Change { get; set; }
    }
}
