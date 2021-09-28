// <copyright file="MainPageContentLayout.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Client.Areas.Main.Shared
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Main page title layout.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.LayoutComponentBase" />
    public partial class MainPageContentLayout
    {
        private string _pageTitle;
        private string _applicationName;
        private string[] _breadCrumbs;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageTitleLayout"/> class.
        /// </summary>
        public MainPageContentLayout()
        {
        }

        /// <summary>
        /// Updates the page content header.
        /// </summary>
        /// <param name="title">The title.</param>
        public async Task UpdatePageContentHeaderAsync(string title, params string[] breadCrumbs)
        {
            _pageTitle = title;

            // TODO! Make configurable
            _applicationName = "Natus Vincere";
            _breadCrumbs = breadCrumbs;

            await InvokeAsync(StateHasChanged);
        }
    }
}