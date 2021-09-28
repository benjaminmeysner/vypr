// <copyright file="IStrategy.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Strategy
{
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// IStrategy interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    public interface IStrategy<TEntity, TEntityViewModel, TRepository> : IUnfilteredStrategy<TEntity, TEntityViewModel, TRepository>
        where TEntity : class, IEntity
        where TEntityViewModel : class, IEntityViewModel
        where TRepository : IRepository<TEntity>
    {
        /// <summary>
        /// Adds the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public Task<TEntityViewModel> Add(TEntityViewModel viewModel);

        /// <summary>
        /// Updates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public Task<TEntityViewModel> Update(TEntityViewModel viewModel);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task Delete(int id);

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="viewModels">The view models.</param>
        /// <returns></returns>
        public Task AddRange(IEnumerable<TEntityViewModel> viewModels);

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="viewModels">The view models.</param>
        /// <returns></returns>
        public Task RemoveRange(IEnumerable<TEntityViewModel> viewModels);

        /// <summary>
        /// Updates the range.
        /// </summary>
        /// <param name="viewModels">The view models.</param>
        /// <returns></returns>
        public Task UpdateRange(IEnumerable<TEntityViewModel> viewModels);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<TEntityViewModel> Get(int id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<TEntityViewModel>> GetAll();

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public Task<int> Count();

        /// <summary>
        /// Finds the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public Task<IEnumerable<TEntityViewModel>> Find(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Queries the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public Task<IQueryResult<TEntityViewModel>> Query(string filter, string orderBy, int? skip, int? top);

        /// <summary>
        /// Queries the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public Task<IQueryResult<TEntityViewModel>> Query(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, int? skip, int? top);
    }
}