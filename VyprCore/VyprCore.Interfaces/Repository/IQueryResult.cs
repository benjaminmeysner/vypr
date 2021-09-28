// <copyright file="IQueryResult.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Repository
{
    using System.Collections.Generic;

    /// <summary>
    /// I Query result.
    /// </summary>
    /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
    public interface IQueryResult<TEntityViewModel>
    {
        int Count { get; set; }

        List<TEntityViewModel> Data { get; set; }

        bool IsFiltered { get; set; }
    }
}
