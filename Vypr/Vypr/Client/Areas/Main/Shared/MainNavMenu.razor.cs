// <copyright file="MainNavMenu.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Client.Areas.Main.Shared
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Vypr.Client.Enums;
    using VyprCore.Interfaces.Client;
    using VyprCore.Interfaces.Context;

    /// <summary>
    /// Nav Menu.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.LayoutComponentBase" />
    public partial class MainNavMenu
    {
        private MenuNavTarget _target;
        private ElementReference _dashboardRef;
        private ElementReference _teamsRef;
        private ElementReference _bulletinsRef;
        private ElementReference _calendarRef;
        private ElementReference _settingsRef;
        private ElementReference _matchesRef;
        private ElementReference _statisticsRef;
        private ElementReference _apiRef;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainNavMenu"/> class.
        /// </summary>
        public MainNavMenu()
        {
        }

        [Inject]
        public IClientApplicationContext ClientApplicationContext { get; set; }

        [Inject]
        public IToolTipService ToolTipService { get; set; }

        [Parameter]
        public EventCallback<MenuNavTarget> Change { get; set; }

        [CascadingParameter]
        public MainLayout Main { get; set; }

        /// <summary>
        /// Called when [menu with extended clicked asynchronous].
        /// </summary>
        /// <param name="target">The target.</param>
        protected async Task OnMainMenuItemClicked(MenuNavTarget target)
        {
            _target = target;
            await Change.InvokeAsync(_target);
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
