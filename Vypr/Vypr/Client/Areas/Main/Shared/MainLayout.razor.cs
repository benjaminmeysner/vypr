// <copyright file="MainLayout.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Client.Areas.Main.Shared
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Vypr.Client.Api;
    using Vypr.Client.Enums;
    using VyprCore.RazorComponents.PageElements;

    /// <summary>
    /// Main layout.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.LayoutComponentBase" />
    public partial class MainLayout
    {
        private bool _showMenuSlideOut;
        private bool _showSpinnerOverlay;
        private string _environment;
        private bool _displayDevBanner;
        private VyprConnectionStatus _connectionStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainLayout"/> class.
        /// </summary>
        public MainLayout()
        {
            _showMenuSlideOut = false;
        }

        [Inject]
        public ConfigurationApi ConfigurationApi { get; set; }

        /// <summary>
        /// Gets or sets the nav target.
        /// </summary>
        /// <value>
        /// The nav target.
        /// </value>
        public MenuNavTarget NavTarget { get; set; }

        public bool ShowSpinnerOverlay
        {
            get => _showSpinnerOverlay;
            set
            {
                _showSpinnerOverlay = value;
                StateHasChanged();
            }
        }

        public bool ShowMenuSlideOut
        {
            get => _showMenuSlideOut;
            set
            {
                _showMenuSlideOut = value;
                StateHasChanged();
            }
        }

        public void ToggleMenuSlideOut() => _showMenuSlideOut = !_showMenuSlideOut;

        public void ShowSpinner() => ShowSpinnerOverlay = true;

        public void HideSpinner() => ShowSpinnerOverlay = false;

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await LoadBanner();
        }

        /// <summary>
        /// Loads the banner.
        /// </summary>
        /// <returns>Task for loading banner.</returns>
        private async Task LoadBanner()
        {
            try
            {
                _environment = await ConfigurationApi.GetEnvironmentName();
                var banner = bool.TryParse(await ConfigurationApi.DisplayBanner(), out _displayDevBanner);
                if (banner == false)
                {
                    _displayDevBanner = _environment != "Live";
                }
            }
            catch
            {
                _displayDevBanner = false;
            }

            StateHasChanged();
        }
    }
}
