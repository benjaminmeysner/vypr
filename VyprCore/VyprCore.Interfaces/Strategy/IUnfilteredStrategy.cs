// <copyright file="IUnfilteredStrategy.cs" company="Vypr Systems">
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
    /// IUnfilteredStrategy interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    public interface IUnfilteredStrategy<TEntity, TEntityViewModel, TRepository>
        where TEntity : class, IEntity
        where TEntityViewModel : class, IEntityViewModel
        where TRepository : IUnfilteredRepository<TEntity>
    {
        /// <summary>
        /// Gets all without filtering.
        /// </summary>
        /// <remarks>
        /// For a single lookup, use FindWithoutQueryFiltering.
        /// </remarks>
        /// <returns></returns>
        public Task<IEnumerable<TEntityViewModel>> GetAllWithoutQueryFiltering();

        /// <summary>
        /// Finds the without filtering.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public Task<IEnumerable<TEntityViewModel>> FindWithoutQueryFiltering(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Finds the without filtering.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public Task<TEntityViewModel> FindSingleWithoutQueryFiltering(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Queries the without filtering.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public Task<IQueryResult<TEntityViewModel>> QueryWithoutQueryFiltering(string filter, string orderBy, int? skip, int? top);

        /// <summary>
        /// Queries the without filtering.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public Task<IQueryResult<TEntityViewModel>> QueryWithoutQueryFiltering(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, int? skip, int? top);
    }
}