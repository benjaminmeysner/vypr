// <copyright file="UserApi.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Client.Api
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using VyprCore.Client.Api;
    using VyprCore.Interfaces.Client;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Trips api.
    /// </summary>
    public class UserApi
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;
        private readonly HttpCodeResponseIntercept _httpCodeHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserApi"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="toastService">The toast service.</param>
        public UserApi(HttpClient httpClient, IToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
            _httpCodeHandler = new HttpCodeResponseIntercept();
        }

        /// <summary>
        /// Gets the trips for this user.
        /// </summary>
        /// <returns>The trips for this logged in user.</returns>
        public async Task<VyprUserViewModel> GetCurrentUserAync()
        {
            var res = await _httpClient.GetAsync($"api/{new VyprUserApiRoute().Route}/me");
            await _httpCodeHandler?.InterceptAsync(res, _toastService);
            return await res.Content.ReadFromJsonAsync<VyprUserViewModel>();
        }
    }
}
