// <copyright file="StrategyHelpers.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Extensions
{
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Interfaces.Strategy;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Strategy Helper methods.
    /// </summary>
    public class StrategyHelpers
    {
        /// <summary>
        /// Updates the against selection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
        /// <typeparam name="TRepository">The type of the repository.</typeparam>
        /// <param name="strategy">The strategy.</param>
        /// <param name="addable">The entities.</param>
        /// <param name="removable">The selection.</param>
        /// <param name="viewModelCallback">The view model callback.</param>
        public static async Task UpdateAgainstSelection<TEntity, TEntityViewModel, TRepository>(
            IStrategy<TEntity, TEntityViewModel, TRepository> strategy,
            IEnumerable<int?> addable,
            IEnumerable<int?> removable,
            Func<int?, TEntityViewModel> viewModelCallback)
            where TEntity : class, IEntity
            where TEntityViewModel : class, IEntityViewModel, new()
            where TRepository : IRepository<TEntity>
        {
            if (!(removable is null) && removable.Any())
            {
                foreach (var entity in removable)
                {
                    await strategy.Delete(entity.Value);
                }
            }

            if (!(addable is null) && addable.Any())
            {
                foreach (var entity in addable)
                {
                    await strategy.Add(viewModelCallback(entity));
                }
            }
        }
    }
}
