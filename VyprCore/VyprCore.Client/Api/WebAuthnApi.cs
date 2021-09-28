// <copyright file="WebAuthnApi.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Api
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Fido2NetLib;
    using VyprCore.Client.Extensions;
    using VyprCore.Interfaces.Client;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Image Utilities Api.
    /// </summary>
    public class WebAuthnApi
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUtilitiesApi"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="navigationManager">The navigation manager.</param>
        public WebAuthnApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Creates the credential options for use in WebAuthn.
        /// </summary>
        /// <param name="viewModel">The viewmodel.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<CredentialCreateOptions> CreateCredentialOptions(WebAuthnCredentialOptionsViewModel viewModel)
        {
            var res = await _httpClient.PostAsJsonAsync($"api/{new WebAuthnApiRoute().Route}/credential/createoptions", viewModel);
            return res.IsSuccessStatusCode ? await res.Content.ReadFromJsonAsync<CredentialCreateOptions>(): null;
        }

        /// <summary>
        /// Creates the credential options for use in WebAuthn.
        /// </summary>
        /// <param name="viewModel">The viewmodel.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<HttpResponseMessage> CreateCredential(WebAuthnCreateCredentialViewModel viewModel)
        {
            var res = await _httpClient.PostAsJsonAsync($"api/{new WebAuthnApiRoute().Route}/credential/make", viewModel);
            return res;
        }

        /// <summary>
        /// Create assertion options.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<AssertionOptions> CreateAssertionOptions(string userName)
        {
            var res = await _httpClient.GetWithParametersAsync($"api/{new WebAuthnApiRoute().Route}/assertion/createoptions", ("userName", userName));
            return res.IsSuccessStatusCode ? await res.Content.ReadFromJsonAsync<AssertionOptions>() : null;
        }

        /// <summary>
        /// Make the assertion.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> MakeAssertion(WebAuthnAuthenticatorAssertionRawResponseViewModel viewModel)
        {
            var res = await _httpClient.PostAsJsonAsync($"api/{new WebAuthnApiRoute().Route}/assertion/make", viewModel);
            return res;
        }

        /// <summary>
        /// Create the credential.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<bool?> VerifyUser(WebAuthnCredentialVerifyViewModel viewModel)
        {
            var res = await _httpClient.PostAsJsonAsync($"api/{new WebAuthnApiRoute().Route}/credential/verify", viewModel);
            return res.IsSuccessStatusCode ? await res.Content.ReadFromJsonAsync<bool?>() : null;
        }
    }
}
