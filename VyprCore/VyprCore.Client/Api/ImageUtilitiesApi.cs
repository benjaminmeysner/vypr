// <copyright file="ImageUtilitiesApi.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Api
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using VyprCore.Interfaces.Client;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Image Utilities Api.
    /// </summary>
    public class ImageUtilitiesApi
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;
        private readonly HttpCodeResponseIntercept _httpCodeHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUtilitiesApi"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="toastService">The toast service.</param>
        public ImageUtilitiesApi(HttpClient httpClient, IToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
            _httpCodeHandler = new HttpCodeResponseIntercept();
        }

        /// <summary>
        /// Mutates the image as per the viewmodel.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<string> MutateAsync(ImageMutateViewModel viewModel)
        {
            var res = await _httpClient.PostAsJsonAsync($"{new ImageUtilitiesApiRoute().Route}/mutate", viewModel);
            await _httpCodeHandler?.InterceptAsync(res, _toastService);
            return res.IsSuccessStatusCode ? await res.Content.ReadAsStringAsync() : null;
        }
    }
}
