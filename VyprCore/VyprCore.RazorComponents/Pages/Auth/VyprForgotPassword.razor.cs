// <copyright file="VyprCoreForgotPassword.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Pages.Auth
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.RazorComponents.Helpers;
    using System.Net.Http;
    using System.Threading.Tasks;
    using VyprCore.Models.ViewModels;
    using VyprCore.Client.Api;
    using VyprCore.RazorComponents.Layouts;
    using Microsoft.AspNetCore.WebUtilities;
    using System.Linq;

    /// <summary>
    /// core forgot password component.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprForgotPassword
    {
        private bool _useWebAuthn;
        protected const string RelativePath = "/forgotpassword";

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprForgotPassword"/> class.
        /// </summary>
        public VyprForgotPassword() : base(RelativePath, typeof(VyprForgotPassword).Name)
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
            Model = new ForgotPasswordViewModel();
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
            
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("U_WebAuthn", out var useWebAuthn))
            {
                if (string.Equals(useWebAuthn.First(), bool.TrueString, System.StringComparison.OrdinalIgnoreCase))
                {
                    _useWebAuthn = true;
                }
            }
        }

        /// <summary>
        /// Called when [valid form submit].
        /// </summary>
        /// <param name="model">The model.</param>
        private async Task OnProcessForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            BeginRequest();

            ResponseMessage = await PublicApi.ForgotPasswordAsync(model, new HttpCodeResponseIntercept());

            EndRequest();
        }

        [Inject]
        public AccountApi PublicApi { get; set; }

        protected HttpResponseMessage ResponseMessage { get; private set; }

        public string ResponseText { get; set; }

        public ForgotPasswordViewModel Model { get; set; }
    }
}