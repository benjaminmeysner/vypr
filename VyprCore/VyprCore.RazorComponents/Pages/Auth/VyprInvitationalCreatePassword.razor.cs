// <copyright file="VyprCoreInvitationalCreatePassword.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Pages.Auth
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.Client.Extensions;
    using VyprCore.Models.ViewModels;
    using System.Net.Http;
    using System.Threading.Tasks;
    using VyprCore.Client.Api;
    using VyprCore.RazorComponents.Layouts;

    /// <summary>
    /// Vypr core set password.
    /// </summary>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprPageBase" />
    public partial class VyprInvitationalCreatePassword
    {
        protected const string RelativePath = "/invite/create/";

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprInvitationalCreatePassword"/> class.
        /// </summary>
        public VyprInvitationalCreatePassword() : base(RelativePath, typeof(VyprInvitationalCreatePassword).Name)
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
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await Root.PageState.AddAsync($"/{Root.NavigationManager.ToBaseRelativePath(Root.NavigationManager.Uri)}");
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override void OnInitialized()
        {
            Model = new InvitationalCreatePasswordViewModel();

            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            if (NavigationManager.TryGetQueryString("inv_token", out string inviteToken)
                && NavigationManager.TryGetQueryString("user_id", out string userId))
            {
                Model.InviteToken = inviteToken;
                Model.Email = userId;
            }
            else
            {
                NavigationManager.NavigateTo("error/404");
            }
        }

        /// <summary>
        /// Called when [valid form submit].
        /// </summary>
        /// <param name="model">The model.</param>
        private async Task OnProcessCreatePasswordAsync(InvitationalCreatePasswordViewModel model)
        {
            BeginRequest();

            ResponseMessage = await PublicApi.InvitationalCreatePasswordAsync(model, new HttpCodeResponseIntercept());

            if (!ResponseMessage.IsOkResponse())
            {
                Response = VyprCore.Models.Resources.StandardText.SomethingWentWrong;
            }

            EndRequest();
        }

        private void GoToLoginPage()
        {
            NavigationManager.NavigateTo($"{NavigationManager.ToAbsoluteUri("login")}", true);
        }

        public string Response { get; set; }

        [Inject]
        public AccountApi PublicApi { get; set; }

        public InvitationalCreatePasswordViewModel Model { get; set; }

        protected HttpResponseMessage ResponseMessage { get; private set; }
    }
}