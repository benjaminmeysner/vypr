// <copyright file="ClientApiSecure.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Api
{
    using VyprCore.Client.Extensions;
    using VyprCore.Interfaces.Client;
    using VyprCore.Models.ViewModels;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates an instance of an Authorised Api HTTP Client. This is used to make requests
    /// which require an authorised access token.
    /// Using this approach also allows us to abstract the underlying HttpClient entirely,
    /// if we wish. We can write methods on the PublicApi so we never expose the HttpClient to calling code.
    /// </summary>
    public sealed class AccountManageApi
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManageApi"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="toastService">The toast service.</param>
        public AccountManageApi(HttpClient httpClient, IToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
        }

        /// <summary>
        /// Changes the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ChangePasswordAsync(ChangePasswordViewModel model, IApiResponseIntercept handler = null)
        {
            var res = await _httpClient.PostAsync("account/manage/changepassword", model);
            return await handler?.InterceptAsync(res, _toastService) ?? res;
        }

        /// <summary>
        /// Determines whether the user has a password set up on their account.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PasswordCreatedAsync(IApiResponseIntercept handler = null)
        {
            var res = await _httpClient.GetAsync("account/manage/haspassword");
            return await handler?.InterceptAsync(res, _toastService) ?? res;
        }

        /// <summary>
        /// Creates a new password on the user account, the user must not have a password
        /// created in order to be able to create a new one.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SetPasswordAsync(SetPasswordViewModel model, IApiResponseIntercept handler = null)
        {
            var res = await _httpClient.PostAsync("account/manage/setpassword", model);
            return await handler?.InterceptAsync(res, _toastService) ?? res;
        }

        /// <summary>
        /// Get the user initials, ie. first letter of firstname and first letter
        /// of last name.
        /// </summary>
        /// <returns></returns>
        public async Task<ProfileDisplayViewModel> GetProfileDisplayAsync(IApiResponseIntercept handler = null)
        {
            var res = await _httpClient.GetAsync("account/manage/getprofiledisplay");
            await handler?.InterceptAsync(res, _toastService);
            return await res.Content.ReadFromJsonAsync<ProfileDisplayViewModel>();
        }

        /// <summary>
        /// Logs the user out.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> LogOut(IApiResponseIntercept handler = null)
        {
            var res = await _httpClient.PostAsync("account/manage/logout", null);
            return await handler?.InterceptAsync(res, _toastService) ?? res;
        }
    }
}
