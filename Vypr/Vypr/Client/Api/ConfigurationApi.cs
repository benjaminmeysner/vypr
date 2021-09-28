// <copyright file="ConfigurationApi.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Client.Api
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using VyprCore.Client.Api;
    using VyprCore.Interfaces.Client;

    /// <summary>
    /// Configuration api.
    /// </summary>
    public class ConfigurationApi
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;
        private readonly HttpCodeResponseIntercept _httpCodeHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationApi"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="toastService">The toast service.</param>
        public ConfigurationApi(HttpClient httpClient, IToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
            _httpCodeHandler = new HttpCodeResponseIntercept();
        }

        /// <summary>
        /// Gets the environment name.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<string> GetEnvironmentName()
        {
            var res = await _httpClient.GetAsync($"api/{new ConfigurationApiRoute().Route}/environment");
            await _httpCodeHandler?.InterceptAsync(res, _toastService);

            return res.IsSuccessStatusCode ? await res.Content.ReadAsStringAsync() : null;
        }

        /// <summary>
        /// Gets the environment name.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<string> DisplayBanner()
        {
            var res = await _httpClient.GetAsync($"api/{new ConfigurationApiRoute().Route}/banner");
            await _httpCodeHandler?.InterceptAsync(res, _toastService);

            return res.IsSuccessStatusCode ? await res.Content.ReadAsStringAsync() : null;
        }
    }
}
