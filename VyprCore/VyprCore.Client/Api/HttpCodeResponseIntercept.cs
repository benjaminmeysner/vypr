// <copyright file="HttpCodeResponseHandler" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Api
{
    using VyprCore.Interfaces.Client;
    using VyprCore.Models.Resources;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Http code responsive handler.
    /// </summary>
    /// <seealso cref="VyprCore.Interfaces.Client.IApiResponseIntercept" />
    public class HttpCodeResponseIntercept : IApiResponseIntercept
    {
        public async Task<HttpResponseMessage> InterceptAsync(HttpResponseMessage response, IToastService toastService = null)
        {
            if (response is null
                || toastService is null)
            {
                return default;
            }

            return response.StatusCode switch
            {
                HttpStatusCode.Accepted => AcceptedResponse(),
                HttpStatusCode.Forbidden => ForbiddenResponse(),
                HttpStatusCode.BadRequest => await BadRequestResponse(),
                _ => response
            };

            HttpResponseMessage AcceptedResponse()
            {
                toastService?.ShowInfo(StandardText.Offline, StandardText.WarningOfflineRequestQueued);
                return response;
            }

            // 400 - BAD REQUEST RESPONSE HANDLE
            async Task<HttpResponseMessage> BadRequestResponse()
            {
                toastService?.ShowError(StandardText.UnableToProcessRequest, await response.Content.ReadAsStringAsync());
                return response;
            }

            // 403 - FORBIDDEN RESPONSE HANDLE
            HttpResponseMessage ForbiddenResponse()
            {
                toastService?.ShowWarning(StandardText.UnableToProcessRequest, StandardText.MissingPermission);
                return response;
            }
        }
    }
}
