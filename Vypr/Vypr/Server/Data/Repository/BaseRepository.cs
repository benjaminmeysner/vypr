// <copyright file="BaseRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Repository;

    /// <summary>
    /// Base repository class in which to inherit.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <seealso cref="Vypr.Server.Interfaces.IRepository{TEntity}" />
    /// <seealso cref="System.IDisposable" />
    public abstract class BaseRepository<TEntity, TDbContext> : IRepository<TEntity>, IDisposable
           where TEntity : class, IEntity, new()
           where TDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity, TDbContext}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BaseRepository(TDbContext context)
        {
            DbContext = context;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
            return await Task.FromResult(entity);
        }

        public virtual async Task AddRange(IEnumerable<TEntity> entities)
        {
            await DbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public virtual async Task<int> Count()
        {
            return await DbContext.Set<TEntity>().CountAsync();
        }

        public virtual async Task<TEntity> Delete(int id)
        {
            var entity = await DbContext.Set<TEntity>().FindAsync(id);
            return entity is null ? entity : DbContext.Set<TEntity>().Remove(entity).Entity;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public virtual async Task<IQueryable<TEntity>> Find(Expression<Func<TEntity, bool>> expression)
        {
            return await Task.FromResult(DbContext.Set<TEntity>().Where(expression));
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IQueryable<TEntity>> GetAll()
        {
            return await Task.FromResult(DbContext.Set<TEntity>());
        }

        public virtual IQueryable<TEntity> Query(string filter = null, string orderBy = null, int? skip = null, int? top = null)
        {
            var query = DbContext.Set<TEntity>();
            return QueryImplementation(query, filter, orderBy, skip, top);
        }

        public virtual IQueryable<TEntity> Query(IQueryable<TEntity> query, string filter = null, string orderBy = null, int? skip = null, int? top = null)
        {
            return QueryImplementation(query, filter, orderBy, skip, top);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, int? skip, int? take)
        {
            return QueryExpressionImplementation(filter, orderBy, skip, take, false);
        }

        public virtual async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => DbContext.Set<TEntity>().RemoveRange(entities));
        }

        public virtual async Task SaveChanges()
        {
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return await Task.FromResult(entity);
        }

        public virtual async Task UpdateRange(IEnumerable<TEntity> entities)
        {
            await Task.FromResult(entities.Select(e => DbContext.Entry(e).State = EntityState.Modified));
        }

        public virtual async Task<IQueryable<TEntity>> GetAllWithoutQueryFiltering()
        {
            return await Task.FromResult(DbContext.Set<TEntity>().IgnoreQueryFilters());
        }

        public virtual async Task<IQueryable<TEntity>> FindWithoutQueryFiltering(Expression<Func<TEntity, bool>> expression)
        {
            return await Task.FromResult(DbContext.Set<TEntity>().IgnoreQueryFilters().Where(expression));
        }


        public virtual async Task<TEntity> FindSingleWithoutQueryFiltering(Expression<Func<TEntity, bool>> expression)
        {
            return await DbContext.Set<TEntity>().IgnoreQueryFilters().SingleOrDefaultAsync(expression);
        }

        public IQueryable<TEntity> QueryWithoutQueryFiltering(string filter, string orderBy, int? skip, int? top)
        {
            var query = DbContext.Set<TEntity>().IgnoreQueryFilters();
            return QueryImplementation(query, filter, orderBy, skip, top);
        }

        public IQueryable<TEntity> QueryWithoutQueryFiltering(IQueryable<TEntity> query, string filter, string orderBy, int? skip, int? top)
        {
            query = query.IgnoreQueryFilters();
            return QueryImplementation(query, filter, orderBy, skip, top);
        }

        public IQueryable<TEntity> QueryWithoutQueryFiltering(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, int? skip, int? take)
        {
            return QueryExpressionImplementation(filter, orderBy, skip, take, true);
        }

        public virtual async Task<int> CountWithoutQueryFiltering()
        {
            return await DbContext.Set<TEntity>().IgnoreQueryFilters().CountAsync();

        }

        /// <summary>
        /// Queries the implementation.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        private IQueryable<TEntity> QueryImplementation(IQueryable<TEntity> query, string filter, string orderBy, int? skip, int? top)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            if (skip != null)
            {
                query = query.Skip(skip.Value);
            }

            if (top != null)
            {
                query = query.Take(top.Value);
            }

            return query;
        }

        /// <summary>
        /// Queries the expression implementation.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="ignoreFilters">if set to <c>true</c> [ignore filters].</param>
        /// <returns></returns>
        public IQueryable<TEntity> QueryExpressionImplementation(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, int? skip, int? take, bool ignoreFilters = false)
        {
            var query = ignoreFilters ? DbContext.Set<TEntity>().IgnoreQueryFilters() : DbContext.Set<TEntity>().AsQueryable();
            if (!(filter is null))
            {
                query = query.Where(filter);
            }
            if (!(orderBy is null))
            {
                query = query.OrderBy(orderBy);
            }
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            return query;
        }

        public TEntity Delete(TEntity entity)
        {
            var entry = DbContext.Entry(entity);
            entry.State = EntityState.Deleted;
            DbContext.SaveChanges();
            return entry.Entity;
        }

        /// <summary>
        /// The database context.
        /// </summary>
        protected TDbContext DbContext { get; set; }
    }
}