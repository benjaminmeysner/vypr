// <copyright file="LinqHelpers.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Helpers
{
    using System.Linq;
    using System.Linq.Dynamic.Core;

    /// <summary>
    /// Linq helpers.
    /// </summary>
    public class LinqHelpers
    {
        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Query<TEntity>(IQueryable<TEntity> query, string filter = null, string orderBy = null, int? skip = null, int? top = null)
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
    }
}
