// <copyright file="VyprCoreResetPassword.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Pages.Auth
{
    using Microsoft.AspNetCore.Components;
    using System.Net.Http;
    using System.Threading.Tasks;
    using VyprCore.Models.ViewModels;
    using VyprCore.Client.Api;
    using VyprCore.RazorComponents.Layouts;
    using VyprCore.Client.Extensions;

    /// <summary>
    /// Vypr core reset password.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprResetPassword
    {
        private string _resetCode;
        private string _userEmail;
        protected const string RelativePath = "/resetpassword";

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprResetPassword"/> class.
        /// </summary>
        public VyprResetPassword() : base(RelativePath, typeof(VyprResetPassword).Name)
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
            base.OnInitialized();

            Model = new ResetPasswordViewModel();

            if (NavigationManager.TryGetQueryString("reset_code", out string resetCode)
                && NavigationManager.TryGetQueryString("usr", out string email))
            {
                _resetCode = resetCode;
                _userEmail = email;
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

        /// <summary>
        /// Called when [valid form submit].
        /// </summary>
        /// <param name="model">The model.</param>
        private async Task OnProcessResetPasswordAsync(ResetPasswordViewModel model)
        {
            BeginRequest();

            model.ResetCode = _resetCode;
            model.UserName = _userEmail;
            ResponseMessage = await PublicApi.ResetPasswordAsync(model, new HttpCodeResponseIntercept());

            EndRequest();
        }

        [Inject]
        public AccountApi PublicApi { get; set; }

        protected HttpResponseMessage ResponseMessage { get; private set; }

        public ResetPasswordViewModel Model { get; set; }
    }
}