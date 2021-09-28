// <copyright file="VyprCoreLogin.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Pages.Auth
{
    using System.Linq;
    using System.Threading.Tasks;
    using VyprCore.RazorComponents.Helpers;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.WebUtilities;
    using VyprCore.Client.Extensions;
    using VyprCore.Models.ViewModels;
    using VyprCore.Client.Api;
    using VyprCore.Interfaces.Client;
    using VyprCore.RazorComponents.Layouts;
    using Microsoft.JSInterop;
    using VyprCore.Models.Resources;
    using Fido2NetLib;
    using System;
    using Blazored.LocalStorage;
    using Fido2NetLib.Objects;
    using System.Net.Http.Json;

    /// <summary>
    /// Login razor component.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprLogin
    {
        private string _returnUrl;
        private bool _useWebAuthnMethod;
        protected const string RelativePath = "/login";

        private LoginWithWebAuthnViewModel _webAuthnModel;
        private LoginWithPasswordViewModel _passwordModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public VyprLogin() : base(RelativePath, typeof(VyprLogin).Name)
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
            _webAuthnModel = new LoginWithWebAuthnViewModel();
            _passwordModel = new LoginWithPasswordViewModel();

            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            await Root.PageState.AddAsync($"/{Root.NavigationManager.ToBaseRelativePath(Root.NavigationManager.Uri)}");

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("ReturnUrl", out var url))
            {
                _returnUrl = url.First();
            }

            // Check if we wish to trigger the web authentication api method instead of password login.
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("U_WebAuthn", out var useWebAuthn))
            {
                if (string.Equals(useWebAuthn.First(), bool.TrueString, System.StringComparison.OrdinalIgnoreCase))
                {
                    if (await JSInterop.InvokeAsync<bool>("isWebAuthnSupportingDevice"))
                    {
                        // If it is a WebAuthn device and user has already setup credentials, we can hide the password input at this stage and let the user proceed.
                        _useWebAuthnMethod = true;
                    }
                }
            }

            // Check if we wish to trigger the web authentication api method instead of password login.
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("E_Code", out var errorCode))
            {
                if (string.Equals(errorCode.First(), VyprComponentHelpers.LOGIN_ECODE_MISSINGCREDENTIALS, System.StringComparison.OrdinalIgnoreCase))
                {
                    Response = StandardText.MissingWebAuthnCredentials;
                }
            }
        }

        /// <summary>
        /// Called on processing webAuthn login.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task OnProcessWebAuthnLoginAsync(LoginWithWebAuthnViewModel model)
        {
            BeginRequest();

            // Firstly check if device has a capable authenticator (platform).
            if (!await JSInterop.InvokeAsync<bool>("webAuthn_isUserVerifyingPlatformAuthenticatorAvailable"))
            {
                EndRequest();
                Response = StandardText.WebAuthnNoAuthenticatorAvailable;
                return;
            }

            // Show the authenticator/login of device.
            var assertionOptions = await WebAuthnApi.CreateAssertionOptions(model.UserName);
            if (!(assertionOptions is null))
            {
                await LocalStorage.SetItemAsync("webauthn.assertionOptions", assertionOptions);
                // Check if there's any existing keys/'allowedCredentials' before prompting.
                // If there's not we will just forward them to create. This is important as some versions of 
                // browsers throw errors on empty allowedCredentials. Either way they won't be able to login
                // if we don't have any.
                if (await JSInterop.InvokeAsync<bool>("webAuthn_emptyAllowedCredentials", assertionOptions))
                {
                    NavigationManager.NavigateTo($"/create_webauthn_cred?ReturnUrl={Uri.EscapeDataString(new Uri(NavigationManager.Uri).PathAndQuery)}&User={model.UserName}");
                    return;
                }

                // There is some credentials in the database for this user
                // We don't know if they are for this current device so we have to check them all (assert)
                AuthenticatorAssertionRawResponse assertion;
                try
                {
                    var jObject = await JSInterop.InvokeAsync<string>("webAuthn_assert", assertionOptions);
                    assertion = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticatorAssertionRawResponse>(jObject);
                }
                catch
                {
                    // User cancelled the verification, or there was a problem in the webauthn api.
                    NavigationManager.NavigateTo($"/create_webauthn_cred?E_Code=EC1&ReturnUrl={Uri.EscapeDataString(new Uri(NavigationManager.Uri).PathAndQuery)}&User={model.UserName}");
                    return;
                }

                if (!(assertion is null))
                {
                    var options = await LocalStorage.GetItemAsync<AssertionOptions>("webauthn.assertionOptions");
                    var assertionResponse = await WebAuthnApi.MakeAssertion(new WebAuthnAuthenticatorAssertionRawResponseViewModel { Response = assertion, AssertionOptions = options });

                    if (!(assertionResponse is null) && assertionResponse.IsSuccessStatusCode)
                    {
                        // The assertion request was successful, check the status/result of the assertion,
                        // to determine where the user should go next.
                        var content = await assertionResponse.Content.ReadFromJsonAsync<AssertionVerificationResult>();
                        if (content.Status.ToUpper() == "OK")
                        {
                            NavigationManager.NavigateTo($"{NavigationManager.ToAbsoluteUri(_returnUrl)}", true);
                            return;
                        }
                        else
                        {
                            // The assertion request was successful but the user was unable to login due to
                            // a setup error, possibly no tenant found for user.
                            Response = StandardText.WebAuthnLoginProblem;
                            EndRequest();
                            return;
                        }
                    }
                    else if (assertionResponse.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        // The assertion request was successful but the user was unable to login due to
                        // a setup error, possibly no tenant found for user.
                        Response = StandardText.WebAuthnProblemAfterVerify;
                        EndRequest();
                        return;
                    }
                    else // Bad Request.
                    {
                        // The assertion failed, user has not setup webauthn for this device.
                        // Forward them to credential create screen to do this.
                        NavigationManager.NavigateTo($"/create_webauthn_cred?ReturnUrl={Uri.EscapeDataString(new Uri(NavigationManager.Uri).PathAndQuery)}&User={model.UserName}");
                        return;
                    }
                }
            }
            else
            {
                Response = StandardText.WebAuthnLoginProblem;
            }

            EndRequest();
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Called when [valid form submit].
        /// </summary>
        /// <param name="model">The model.</param>
        private async Task OnProcessLoginAsync(LoginWithPasswordViewModel model)
        {
            BeginRequest();

            var response = await PublicApi.LoginWithPasswordAsync(model, new HttpCodeResponseIntercept());

            if (response.IsOkResponse())
            {
                NavigationManager.NavigateTo($"{NavigationManager.ToAbsoluteUri(_returnUrl)}", true);
            }
            else
            {
                Response = await response.Content.ReadAsStringAsync();
            }

            EndRequest();
        }

        [CascadingParameter]
        public VyprLoginLandingLayout Root { get; set; }

        [Inject]
        public IJSRuntime JSInterop { get; set; }

        [Inject]
        public AccountApi PublicApi { get; set; }

        [Inject]
        public WebAuthnApi WebAuthnApi { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        public string Response { get; set; }
    }
}