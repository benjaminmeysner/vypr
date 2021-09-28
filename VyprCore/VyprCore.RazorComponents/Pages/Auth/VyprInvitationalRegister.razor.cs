// <copyright file="VyprCoreInvitationalRegister.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Pages.Auth
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.Client.Extensions;
    using VyprCore.RazorComponents.Layouts;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr Register from an invite.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprInvitationalRegister
    {
        private string _userName;
        private string _inviteToken;
        protected const string RelativePath = "/invite";

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprInvitationalRegister"/> class.
        /// </summary>
        public VyprInvitationalRegister() : base(RelativePath, typeof(VyprInvitationalRegister).Name)
        {
        }

        /// <summary>
        /// Gets or sets the root.
        /// </summary>
        /// <value>
        /// The root.
        /// </value>
        [CascadingParameter]
        public VyprLoginLandingLayout Root { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override void OnInitialized()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            if (NavigationManager.TryGetQueryString("user_id", out string userId)
                && NavigationManager.TryGetQueryString("inv_token", out string inviteToken))
            {
                _userName = userId;
                _inviteToken = inviteToken;
            }
            else
            {
                NavigationManager.NavigateTo("error/404");
            }
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await Root.PageState.AddAsync($"/{Root.NavigationManager.ToBaseRelativePath(Root.NavigationManager.Uri)}");
        }
    }
}