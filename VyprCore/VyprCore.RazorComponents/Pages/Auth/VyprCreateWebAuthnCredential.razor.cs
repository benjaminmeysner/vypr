// <copyright file="VyprCoreCreateWebAuthnCredential.cs" company="Vypr Systems">
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
    using Microsoft.AspNetCore.WebUtilities;
    using System.Linq;
    using VyprCore.Models.Resources;
    using Microsoft.JSInterop;
    using Fido2NetLib;
    using System;
    using Blazored.LocalStorage;
    using Fido2NetLib.Objects;
    using System.Net.Http.Json;

    /// <summary>
    /// Vypr core create web authn credential.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprCreateWebAuthnCredential
    {
        private string _userName;
        private string _returnUrl;
        private string _response;
        private string _errorCode;
        private WebAuthnCredentialVerifyViewModel _model;

        protected const string RelativePath = "/create_webauthn_cred";

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprCreateWebAuthnCredential"/> class.
        /// </summary>
        public VyprCreateWebAuthnCredential() : base(RelativePath, typeof(VyprCreateWebAuthnCredential).Name)
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
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("ReturnUrl", out var url))
            {
                _returnUrl = url.First();
            }

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("User", out var userName))
            {
                _response = null;
                _userName = userName.First();
                _model = new WebAuthnCredentialVerifyViewModel { UserName = _userName };
            }
            else
            {
                NavigationManager.NavigateTo("error/404");
            }

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("E_Code", out var errorCode))
            {
                _errorCode = errorCode.First();
                if (!string.IsNullOrEmpty(_errorCode))
                {
                    _response = StandardText.WebAuthnUnableToEstablishVerification;
                }
            }
        }

        /// <summary>
        /// Called when [valid form submit].
        /// </summary>
        /// <param name="model">The model.</param>
        private async Task OnProcessConfirmPasswordAsync(WebAuthnCredentialVerifyViewModel model)
        {
            BeginRequest();
            var verified = await WebAuthnApi.VerifyUser(model);
            if (verified.HasValue && verified.Value)
            {
                // Proceed to create credential, we will check the password again
                // On credential create in server for security reasons.
                var options = await WebAuthnApi.CreateCredentialOptions(new WebAuthnCredentialOptionsViewModel { UserName = model.UserName, DisplayName = model.UserName });
                if (!(options is null))
                {
                    AuthenticatorAttestationRawResponse deviceResponse;
                    try
                    {
                        await LocalStorage.SetItemAsync("webauthn.attestationOptions", options);
                        var jObject = await JSInterop.InvokeAsync<string>("webAuthn_register", options);
                        deviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticatorAttestationRawResponse>(jObject);
                        var serverResponse = await WebAuthnApi.CreateCredential(new WebAuthnCreateCredentialViewModel(model.UserName, model.Password, deviceResponse, options));
  
                        if (!serverResponse.IsSuccessStatusCode || (await serverResponse.Content.ReadFromJsonAsync<AssertionVerificationResult>()).Status.ToUpper() != "OK")
                        {
                            _response = await serverResponse.Content.ReadAsStringAsync();
                            EndRequest();
                            return;
                        }

                        // The user at this point should be authenticated and logged in, redirect them back to the return url.
                        NavigationManager.NavigateTo("/", true);
                    }
                    // This will be thrown in the serialising back to response,
                    // If theres an exception thrown in the registration script it's usually a platform error.
                    catch (Exception)
                    {
                        _response = StandardText.WebAuthnTimeOutOrNotAllowed;
                        EndRequest();
                        return;
                    }
                }
                else
                {
                    NavigationManager.NavigateTo($"{NavigationManager.ToAbsoluteUri(_returnUrl)}", true);
                }
            }
            else
            {
                _response = StandardText.WebAuthnVerifyProblem;
            }
            EndRequest();
        }

        [Inject]
        public WebAuthnApi WebAuthnApi { get; set; }

        [Inject]
        public IJSRuntime JSInterop { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        protected HttpResponseMessage ResponseMessage { get; private set; }
    }
}