// <copyright file="IRepository.cs" company="Vypr Systems">
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
    /// IRepository interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> : IUnfilteredRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public Task<IQueryable<TEntity>> GetAll();

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<TEntity> Get(int id);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task<TEntity> Add(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<TEntity> Delete(int id);

        /// <summary>
        /// Deletes the specified t entity.
        /// </summary>
        /// <param name="TEntity">The t entity.</param>
        /// <returns></returns>
        public TEntity Delete(TEntity entity);

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public Task AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public Task UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public Task RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        public Task SaveChanges();

        /// <summary>
        /// Finds the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public Task<IQueryable<TEntity>> Find(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Queries the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(string filter, string orderBy, int? skip, int? top);

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(IQueryable<TEntity> query, string filter, string orderBy, int? skip, int? top);

        /// <summary>
        /// Queries the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, int? skip, int? take);

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public Task<int> Count();
    }
}