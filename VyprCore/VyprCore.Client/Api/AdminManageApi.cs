// <copyright file="ClientApiAdmin.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Api
{
    using System.Net.Http;
    using VyprCore.Client.Extensions;
    using System.Threading.Tasks;
    using VyprCore.Models.ViewModels;
    using VyprCore.Interfaces.Client;

    /// <summary>
    /// Creates an instance of an Admin Authorised Api HTTP Client. This is used to make requests
    /// which require an authorised access token.
    /// Using this approach also allows us to abstract the underlying HttpClient entirely,
    /// if we wish. We can write methods on the PublicApi so we never expose the HttpClient to calling code.
    /// </summary>
    public sealed class AdminManageApi
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountApi"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="toastService">The toast service.</param>
        public AdminManageApi(HttpClient httpClient, IToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
        }

        /// <summary>
        /// Sends the user an invite.
        /// </summary>model">The model.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendInvite(SendUserInviteViewModel model, IApiResponseIntercept handler = null)
        {
            var res = await _httpClient.PostAsync("admin/senduserinvite", model);
            return await handler?.InterceptAsync(res, _toastService) ?? res;
        }
    }
}
