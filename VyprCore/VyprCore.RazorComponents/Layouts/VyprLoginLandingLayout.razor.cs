// <copyright file="VyprCoreLoginLandingLayout" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Layouts
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.Interfaces.Client;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr Component base class.
    /// Logically bundles common functionality between Vypr Components.
    /// This class inherits <see cref="Microsoft.AspNetCore.Components.ComponentBase"/>.
    /// </summary>
    public partial class VyprLoginLandingLayout
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprLoginLandingLayout"/> class.
        /// </summary>
        public VyprLoginLandingLayout()
        {
        }

        /// <summary>
        /// Gets or sets the state of the page.
        /// </summary>
        /// <value>
        /// The state of the page.
        /// </value>
        [Inject]
        public IPageState PageState { get; set; }

        /// <summary>
        /// Gets or sets the navigation manager.
        /// </summary>
        /// <value>
        /// The navigation manager.
        /// </value>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            PageState.Change = new EventCallback(this, (Action)(() => StateHasChanged()));

            // Let's check to see if the user is already authenticated before
            // loading the component.
            if (!(await AuthState.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated)
            {
                AlreadyLoggedIn = false;
            }
            else
            {
                NavigationManager.NavigateTo($"{NavigationManager.ToAbsoluteUri("/")}");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user is [already logged in].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [already logged in]; otherwise, <c>false</c>.
        /// </value>
        public bool AlreadyLoggedIn { get; set; } = false;
    }
}
