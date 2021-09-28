// <copyright file="HttpClientHelpers.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Extensions
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Remote.Linq;
    using VyprCore.Models.ViewModels;
    using VyprCore.Utilities.Helpers;

    /// <summary>
    /// Http Client helper methods class.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Posts the model as JSON to the request uri asynchronously.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostAsync<TModel>(this HttpClient client, string requestUri, TModel model)
        {
            try
            {
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings().ConfigureRemoteLinq();
                serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                serializerSettings.TypeNameHandling = TypeNameHandling.Auto;

                var stringContent = new StringContent(JsonConvert.SerializeObject(model, serializerSettings), Encoding.UTF8, "application/json");
                return await client.PostAsync(requestUri, stringContent);
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Posts the query model as JSON to the request uri asynchronously.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> QueryAsync<TModel>(this HttpClient client, string requestUri, QueryViewModel<TModel> model)
        {
            return await PostAsync(client, requestUri, model);
        }

        /// <summary>
        /// Posts the model as JSON to the request uri asynchronously.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PutAsync<TModel>(this HttpClient client, string requestUri, TModel model)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                return await client.PutAsync(requestUri, stringContent);
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Deletes the model as JSON to the request uri asynchronously.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient client, string requestUri)
        {
            try
            {
                return await client.DeleteAsync($"{requestUri.RemoveTrailingSlash()}");
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Gets the with parameters asynchronous.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> GetWithParametersAsync(this HttpClient client, string requestUri, params (string Name, object Value)[] parameters)
        {
            if (!(parameters is null) && parameters.Any())
            {
                requestUri = $"{requestUri.RemoveTrailingSlash()}?";
                foreach (var (Name, Value) in parameters)
                {
                    if (!(Value is null))
                    {
                        requestUri += $"{Name}={Value}&";
                    }
                }
            }

            return await GetAsync(client, StringHelpers.SanitiseQueryString(requestUri));
        }

        /// <summary>
        /// Get the content as JSON and converts to model from request uri asynchronously.
        /// This returns the model with the provided id.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> GetAsync(this HttpClient client, string requestUri, int id)
        {
            try
            {
                return await client.GetAsync($"{requestUri.RemoveTrailingSlash()}/{id}");
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get the content as JSON and converts to model from request uri asynchronously.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> GetAsync(this HttpClient client, string requestUri)
        {
            try
            {
                return await client.GetAsync($"{requestUri}");
            }
            catch
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Determines whether the response [is bad request response].
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        ///   <c>true</c> if [is bad request response] [the specified message]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBadRequestResponse(this HttpResponseMessage message)
        {
            return message.StatusCode == HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// Determines whether the response [is unauthorised response].
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        ///   <c>true</c> if [is unauthorised request response] [the specified message]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUnauthorisedResponse(this HttpResponseMessage message)
        {
            return message.StatusCode == HttpStatusCode.Unauthorized;
        }

        /// <summary>
        /// Determines whether the response [is OK response].
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        ///   <c>true</c> if [is OK request response] [the specified message]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOkResponse(this HttpResponseMessage message)
        {
            return (message?.IsSuccessStatusCode) ?? false;
        }

        /// <summary>
        /// Determines whether [is conflict of resource response].
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        ///   <c>true</c> if [is conflict of resource response] [the specified message]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsConflictOfResourceResponse(this HttpResponseMessage message)
        {
            return message.StatusCode == HttpStatusCode.Conflict;
        }

        /// <summary>
        /// Determines whether the response [is OK response].
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        ///   <c>true</c> if [is OK request response] [the specified message]; otherwise, <c>false</c>.
        /// </returns>
        public static async Task<bool> IsTrueResponseAsync(this HttpResponseMessage message)
        {
            if (bool.TryParse(await message.Content.ReadAsStringAsync(), out bool value))
            {
                return value;
            }
            else
            {
                throw new FormatException($"Format of the HttpContent is not Boolean.");
            }
        }

        /// <summary>
        /// Determines whether the response [is OK response].
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        ///   <c>true</c> if [is OK request response] [the specified message]; otherwise, <c>false</c>.
        /// </returns>
        public static async Task<string> ContentAsStringAsync(this HttpResponseMessage message)
        {
            return await message.Content.ReadAsStringAsync();
        }
    }
}