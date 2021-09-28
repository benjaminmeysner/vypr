// <copyright file="ApiClientPublic.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Api
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using VyprCore.Client.Extensions;
    using VyprCore.Interfaces.Client;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Creates an instance of a Public Api HTTP Client. This is used to make requests
    /// which do not require an authorised access token. 
    /// Using this approach also allows us to abstract the underlying HttpClient entirely,
    /// if we wish. We can write methods on the PublicApi so we never expose the HttpClient to calling code.
    /// </summary>
    public sealed class AccountApi
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountApi"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="toastService">The toast service.</param>
        public AccountApi(HttpClient httpClient, IToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
        }

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ResetPasswordAsync(ResetPasswordViewModel model, IApiResponseIntercept handler = null)
        {
            var res = await _httpClient.PostAsync("account/resetpassword", model);
            return await handler?.InterceptAsync(res, _toastService) ?? res;
        }

        /// <summary>
        /// Logins the with password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> LoginWithPasswordAsync(LoginWithPasswordViewModel model, IApiResponseIntercept handler = null)
        {
            var res = await _httpClient.PostAsync("account/authenticate", model);
            return await handler?.InterceptAsync(res, _toastService) ?? res;
        }

        /// <summary>
        /// Forgot password call asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ForgotPasswordAsync(ForgotPasswordViewModel model, IApiResponseIntercept handler = null)
        {
            var res = await _httpClient.PostAsync("account/forgotpassword", model);
            return await handler?.InterceptAsync(res, _toastService) ?? res;
        }

        /// <summary>
        /// Forgot password call asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> InvitationalCreatePasswordAsync(InvitationalCreatePasswordViewModel model, IApiResponseIntercept handler = null)
        {
            var res = await _httpClient.PostAsync("account/invitecreatepassword", model);
            return await handler?.InterceptAsync(res, _toastService) ?? res;
        }

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<IEnumerable<KeyValuePair<string, string>>> GetUserClaimsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<KeyValuePair<string, string>>>("account/getuser");
        }
    }
}
