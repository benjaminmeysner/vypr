// <copyright file="IApiClientComponent.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Client
{
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface which describes classes/components which have functionality
    /// which interacts with the server API. For example, this will allow components
    /// to show loading/spinners on async calls to the API.
    /// </summary>
    public interface IApiResponseIntercept
    {
        /// <summary>
        /// Handles the asynchronous.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="toastService">The toast service.</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> InterceptAsync(HttpResponseMessage response, IToastService toastService = null);
    }
}
