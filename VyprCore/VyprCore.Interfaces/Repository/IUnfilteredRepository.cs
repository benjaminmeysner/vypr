// <copyright file="INonFilteredRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Repository
{
    using VyprCore.Interfaces.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Repository exposing the non filtered db sets / without query filtering applied.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IUnfilteredRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets all with query filtering.
        /// </summary>
        /// <returns></returns>
        public Task<IQueryable<TEntity>> GetAllWithoutQueryFiltering();

        /// <summary>
        /// Finds the with query filtering.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public Task<IQueryable<TEntity>> FindWithoutQueryFiltering(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Finds the with query filtering.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public Task<TEntity> FindSingleWithoutQueryFiltering(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Queries the with query filtering.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public IQueryable<TEntity> QueryWithoutQueryFiltering(string filter, string orderBy, int? skip, int? top);

        /// <summary>
        /// Queries the with query filtering.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public IQueryable<TEntity> QueryWithoutQueryFiltering(IQueryable<TEntity> query, string filter, string orderBy, int? skip, int? top);

        /// <summary>
        /// Queries the with query filtering.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns></returns>
        public IQueryable<TEntity> QueryWithoutQueryFiltering(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, int? skip, int? take);

        /// <summary>
        /// Counts the with query filtering.
        /// </summary>
        /// <returns></returns>
        public Task<int> CountWithoutQueryFiltering();
    }
}
