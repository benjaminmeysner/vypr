// <copyright file="UserSettings.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Client.Areas.Main.Settings.UserSettings
{
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;
    using Vypr.Client.Api;
    using Vypr.Client.Areas.Main.Shared;
    using VyprCore.Client.Api;
    using VyprCore.Interfaces.Client;
    using VyprCore.Models.Resources;
    using VyprCore.Models.ViewModels;
    using VyprCore.RazorComponents.PageElements;

    /// <summary>
    /// User settings.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class UserSettings
    {
        VyprMiniCard<int> _totalUsersCard;
        private UsersGrid _usersGrid;
        private int _totalUserCount;
        private bool _launchUserPane;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettings"/> class.
        /// </summary>
        public UserSettings()
        {
        }

        [Inject]
        public IEntityApi Api { get; set; }

        /// <summary>
        /// Gets or sets the layout.
        /// </summary>
        /// <value>
        /// The layout.
        /// </value>
        [CascadingParameter]
        public MainPageContentLayout Layout { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            _totalUserCount = await Api.CountAsync<VyprUserApiRoute>(new HttpCodeResponseIntercept());
            await _totalUsersCard.SetValueAsync(_totalUserCount);

            await Layout.UpdatePageContentHeaderAsync(StandardText.UserManagement, new[] { StandardText.Settings, StandardText.UserManagement });
        }

        /// <summary>
        /// Called when [user selected].
        /// </summary>
        /// <param name="userViewModel">The user view model.</param>
        private void OnUserSelected(VyprUserViewModel userViewModel)
        {
            _launchUserPane = true;
        }
    }
}