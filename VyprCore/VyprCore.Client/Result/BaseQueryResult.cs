// <copyright file="BaseQueryResult.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Result
{
    using VyprCore.Interfaces.Repository;
    using System.Collections.Generic;

    /// <summary>
    /// Base Query result.
    /// </summary>
    /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
    /// <seealso cref="VyprCore.Client.Interfaces.IQueryResult{TEntityViewModel}" />
    public class BaseQueryResult<TEntityViewModel> : IQueryResult<TEntityViewModel>
    {
        public int Count { get; set; }

        public List<TEntityViewModel> Data { get; set; }

        public bool IsFiltered { get; set; }
    }
}
