// <copyright file="IEntityViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Entity
{
    /// <summary>
    /// Entity ViewModel Interface.
    /// </summary>
    public interface IEntityViewModel
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int? Id { get; }
    }
}
