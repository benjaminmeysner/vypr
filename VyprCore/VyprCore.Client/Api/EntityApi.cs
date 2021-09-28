// <copyright file="EntityApi.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Api
{
    using System.Collections.Generic;
    using System.Net.Http;
    using VyprCore.Client.Extensions;
    using System.Threading.Tasks;
    using VyprCore.Interfaces.Client;
    using VyprCore.Models.ViewModels;
    using System;
    using System.Linq.Expressions;
    using VyprCore.Interfaces.Repository;
    using Remote.Linq;
    using VyprCore.Client.Result;
    using System.Net.Http.Json;
    using System.Linq;

    /// <summary>
    /// Creates an instance of a generic entity API in which the methods
    /// take in a TEntityApiRoute which will provide the base url for any mapping methods.
    /// </summary>
    public sealed class EntityApi : IEntityApi
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityApi"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="toastService">The toast service.</param>
        public EntityApi(HttpClient httpClient, IToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
        }

        public async Task<List<TEntityViewModel>> GetAsync<TEntityApiRoute, TEntityViewModel>(
            IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new()
        {
            var res = await _httpClient.GetAsync($"api/{new TEntityApiRoute().Route}");
            await handler?.InterceptAsync(res, _toastService);
            return await res.Content.ReadFromJsonAsync<List<TEntityViewModel>>();
        }

        public async Task<TEntityViewModel> GetAsync<TEntityApiRoute, TEntityViewModel>(int id,
            IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : class, new()
        {
            var res = await _httpClient.GetAsync($"api/{new TEntityApiRoute().Route}/{id}");
            await handler?.InterceptAsync(res, _toastService);
            return await res.Content.ReadFromJsonAsync<TEntityViewModel>();
        }

        public async Task<List<TEntityViewModel>> GetWithParametersAsync<TEntityApiRoute, TEntityViewModel>(
            IApiResponseIntercept handler = null,
            params (string Name, object Value)[] parameters)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new()
        {
            var res = await _httpClient.GetWithParametersAsync($"api/{new TEntityApiRoute().Route}", parameters);
            await handler?.InterceptAsync(res, _toastService);
            return await res.Content.ReadFromJsonAsync<List<TEntityViewModel>>();
        }

        public async Task<HttpResponseMessage> UpdateAsync<TEntityApiRoute, TEntityViewModel>(
            TEntityViewModel model,
            IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new()
        {
            var res = await _httpClient.PutAsync($"api/{new TEntityApiRoute().Route}", model);
            return handler != null ? await handler.InterceptAsync(res, _toastService) : res;
        }

        public async Task<HttpResponseMessage> DeleteAsync<TEntityApiRoute>(
            int id,
            IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
        {
            var res = await _httpClient.DeleteAsync($"api/{new TEntityApiRoute().Route}/{id}");
            return await handler?.InterceptAsync(res, _toastService) ?? res;
        }

        public async Task<HttpResponseMessage> AddAsync<TEntityApiRoute, TEntityViewModel>(
            TEntityViewModel model,
            IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new()
        {
            var res = await _httpClient.PostAsync($"api/{new TEntityApiRoute().Route}", model);
            return handler != null ? await handler.InterceptAsync(res, _toastService) : res;
        }

        public async Task<List<TEntityViewModel>> QueryAsync<TEntityApiRoute, TEntityViewModel>(
            IQueryRoute routeSuffix,
            int id,
            IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new()
        {
            var res = await _httpClient.GetAsync($"api/{new TEntityApiRoute().Route}/{routeSuffix.QueryRoute}/{id}");
            await handler?.InterceptAsync(res, _toastService);
            return await res.Content.ReadFromJsonAsync<List<TEntityViewModel>>();
        }

        public async Task<IQueryResult<TEntityViewModel>> GridQueryAsync<TEntityApiRoute, TEntityViewModel>(
            IDataGridFilteringArgs args,
            IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new()
        {
            var res = await _httpClient.GetWithParametersAsync(
                $"api/{new TEntityApiRoute().Route}/query",
                ("filter", args?.Filter),
                ("orderBy", args?.OrderBy),
                ("skip", args?.Skip),
                ("top", args?.Take));

            await handler?.InterceptAsync(res, _toastService);
            return await res.Content.ReadFromJsonAsync<BaseQueryResult<TEntityViewModel>>();
        }

        public async Task<IQueryResult<TEntityViewModel>> GridQueryAsync<TEntityApiRoute, TEntityViewModel>(
            IQueryRoute routeSuffix,
            int id,
            IDataGridFilteringArgs args,
            IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new()
        {
            var res = await _httpClient.GetWithParametersAsync(
                $"api/{new TEntityApiRoute().Route}/{routeSuffix.QueryRoute}/{id}/query",
                ("filter", args?.Filter),
                ("orderBy", args?.OrderBy),
                ("skip", args?.Skip),
                ("top", args?.Take));

            await handler?.InterceptAsync(res, _toastService);
            return res.IsSuccessStatusCode
                ? await res.Content.ReadFromJsonAsync<BaseQueryResult<TEntityViewModel>>()
                : new BaseQueryResult<TEntityViewModel> { Count = 0, Data = new List<TEntityViewModel>(), IsFiltered = false };
        }

        public async Task<IQueryResult<TEntityViewModel>> GridQueryAsync<TEntityApiRoute, TEntityViewModel>(
            IQueryRoute routeSuffix,
            int id,
            IDataGridFilteringArgs args,
            IApiResponseIntercept handler = null,
            params (string Name, object Value)[] additionalParams)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new()
        {
            var list = additionalParams.ToList();
            list.Add(("filter", args?.Filter));
            list.Add(("orderBy", args?.OrderBy));
            list.Add(("skip", args?.Skip));
            list.Add(("top", args?.Take));

            var newParams = list.ToArray();

            var res = await _httpClient.GetWithParametersAsync(
                $"api/{new TEntityApiRoute().Route}/{routeSuffix.QueryRoute}/{id}/query",
                newParams);

            await handler?.InterceptAsync(res, _toastService);
            return await res.Content.ReadFromJsonAsync<BaseQueryResult<TEntityViewModel>>();
        }

        public async Task<IQueryResult<TEntityViewModel>> QueryAsync<TEntityApiRoute, TEntity, TEntityViewModel>(
            Expression<Func<TEntity, bool>> where = null,
            Expression<Func<TEntity, object>> orderBy = null,
            int? skip = null,
            int? take = null,
            IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new()
        {
            var res = await _httpClient.QueryAsync(
                $"api/{new TEntityApiRoute().Route}/query",
                new QueryViewModel<TEntity> { Filter = where?.ToRemoteLinqExpression(), OrderBy = orderBy?.ToRemoteLinqExpression(), Skip = skip, Take = take });

            await handler?.InterceptAsync(res, _toastService);
            return await res.Content.ReadFromJsonAsync<BaseQueryResult<TEntityViewModel>>();
        }

        public async Task<int> CountAsync<TEntityApiRoute>(IApiResponseIntercept handler = null) where TEntityApiRoute : IApiRoute, new()
        {
            var res = await _httpClient.GetAsync($"api/{new TEntityApiRoute().Route}/count");
            await handler?.InterceptAsync(res, _toastService);
            return await res.Content.ReadFromJsonAsync<int>();
        }
    }
}
