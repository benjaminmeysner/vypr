// <copyright file="IEntityApi.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Client
{
    using VyprCore.Interfaces.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IEntityApi
    {
        /// <summary>
        /// Add.
        /// </summary>
        /// <typeparam name="TEntityApiRoute">The type of the entity API route.</typeparam>
        /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> AddAsync<TEntityApiRoute, TEntityViewModel>(TEntityViewModel model, IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new();

        /// <summary>
        /// Delete by id.
        /// </summary>
        /// <typeparam name="TEntityApiRoute">The type of the entity API route.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteAsync<TEntityApiRoute>(int id, IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new();

        /// <summary>
        /// Get all.
        /// </summary>
        /// <typeparam name="TEntityApiRoute">The type of the entity API route.</typeparam>
        /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        Task<List<TEntityViewModel>> GetAsync<TEntityApiRoute, TEntityViewModel>(IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new();

        /// <summary>
        /// Get by id.
        /// </summary>
        /// <typeparam name="TEntityApiRoute">The type of the entity API route.</typeparam>
        /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
        /// <param name="handler">The handler.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<TEntityViewModel> GetAsync<TEntityApiRoute, TEntityViewModel>(int id, IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : class, new();

        /// <summary>
        /// Get with a constructed query string.
        /// </summary>
        /// <typeparam name="TEntityApiRoute">The type of the entity API route.</typeparam>
        /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
        /// <param name="handler">The handler.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<List<TEntityViewModel>> GetWithParametersAsync<TEntityApiRoute, TEntityViewModel>(IApiResponseIntercept handler = null, params (string Name, object Value)[] parameters)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new();

        /// <summary>
        /// Get by additional query route and id.
        /// </summary>
        /// <typeparam name="TEntityApiRoute">The type of the entity API route.</typeparam>
        /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
        /// <param name="routeSuffix">The route suffix.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        Task<List<TEntityViewModel>> QueryAsync<TEntityApiRoute, TEntityViewModel>(IQueryRoute routeSuffix, int id, IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new();

        /// <summary>
        /// Get by string representation of predicates/equality comparitors etc. Used in data grid filtering.
        /// </summary>
        /// <typeparam name="TEntityApiRoute">The type of the entity API route.</typeparam>
        /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        Task<IQueryResult<TEntityViewModel>> GridQueryAsync<TEntityApiRoute, TEntityViewModel>(IDataGridFilteringArgs args, IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new();

        /// <summary>
        /// Grids the query asynchronous. Takes an additional query argument against the base entity and
        /// then applies the filtering on the result.
        /// </summary>
        /// <typeparam name="TEntityApiRoute">The type of the entity API route.</typeparam>
        /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
        /// <param name="routeSuffix">The route suffix.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        Task<IQueryResult<TEntityViewModel>> GridQueryAsync<TEntityApiRoute, TEntityViewModel>(IQueryRoute routeSuffix, int id, IDataGridFilteringArgs args, IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new();

        /// <summary>
        /// Grids the query asynchronous. Takes additional arbitrary parameter arguments.
        /// </summary>
        /// <typeparam name="TEntityApiRoute">The type of the entity API route.</typeparam>
        /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
        /// <param name="routeSuffix">The route suffix.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="additionalParams">The additional parameters.</param>
        /// <returns></returns>
        Task<IQueryResult<TEntityViewModel>> GridQueryAsync<TEntityApiRoute, TEntityViewModel>(IQueryRoute routeSuffix, int id, IDataGridFilteringArgs args, IApiResponseIntercept handler = null, params (string Name, object Value)[] additionalParams)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new();

        /// <summary>
        /// Queries the asynchronous.
        /// </summary>
        /// <remarks>
        /// Ben 20/06/2021 - I've noticed some issues when creating predicates with integer comparison,
        /// sometimes around nullable ints, it may throw argument types do not match exceptions. I've found it seems to work
        /// if you keep the left operator a non nullable property ie. x => x.User.Id and not use x => x.UserId, and the right
        /// operator can be nullable ie, x => x.User.Id  == new int?(1). 
        /// </remarks>
        /// <typeparam name="TEntityApiRoute">The type of the entity API route.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
        /// <param name="where">The where.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        Task<IQueryResult<TEntityViewModel>> QueryAsync<TEntityApiRoute, TEntity, TEntityViewModel>(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> orderBy = null, int? skip = null, int? take = null, IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new();

        /// <summary>
        /// Update.
        /// </summary>
        /// <typeparam name="TEntityApiRoute">The type of the entity API route.</typeparam>
        /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> UpdateAsync<TEntityApiRoute, TEntityViewModel>(TEntityViewModel model, IApiResponseIntercept handler = null)
            where TEntityApiRoute : IApiRoute, new()
            where TEntityViewModel : new();

        /// <summary>
        /// Counts the total entities asynchronous.
        /// </summary>
        /// <typeparam name="TEntityApiRoute">The type of the entity API route.</typeparam>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        Task<int> CountAsync<TEntityApiRoute>(IApiResponseIntercept handler = null) where TEntityApiRoute : IApiRoute, new();
    }
}