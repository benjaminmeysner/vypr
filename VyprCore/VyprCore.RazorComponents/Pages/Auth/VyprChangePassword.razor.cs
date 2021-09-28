// <copyright file="VyprCoreChangePassword.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Pages.Auth
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using VyprCore.RazorComponents.Helpers;
    using VyprCore.Client.Extensions;
    using VyprCore.Models.ViewModels;
    using VyprCore.Client.Api;
    using VyprCore.RazorComponents.Layouts;

    /// <summary>
    /// Vypr Core Change Password.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprChangePassword
    {
        protected const string RelativePath = "/changepassword";
        private bool _initialised;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprChangePassword"/> class.
        /// </summary>
        public VyprChangePassword() : base(RelativePath, typeof(VyprChangePassword).Name)
        {
        }
        /// 
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
            _initialised = false;

            await Root.PageState.AddAsync($"/{Root.NavigationManager.ToBaseRelativePath(Root.NavigationManager.Uri)}");

            // Check if user has a local password account set up for this account
            // if not, redirect them away to create one.
            var response = await AccountApi.PasswordCreatedAsync(new HttpCodeResponseIntercept());
            if (!await response.IsTrueResponseAsync())
            {
                NavigationManager.NavigateTo("/account/manage/setpassword");
            }
            _initialised = true;
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override void OnInitialized()
        {
            Model = new ChangePasswordViewModel();
        }

        /// <summary>
        /// Called when [valid form submit].
        /// </summary>
        /// <param name="model">The model.</param>
        private async Task OnProcessChangePasswordAsync(ChangePasswordViewModel model)
        {
            BeginRequest();

            ResponseMessage = await AccountApi.ChangePasswordAsync(model, new HttpCodeResponseIntercept());

            EndRequest();
        }

        [Inject]
        public AccountManageApi AccountApi { get; set; }

        protected HttpResponseMessage ResponseMessage { get; private set; }

        public ChangePasswordViewModel Model { get; set; }
    }
}