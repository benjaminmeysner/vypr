// <copyright file="IComponentFilterable.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Client
{
    /// <summary>
    /// IComonentFilterable interface. Defines a class which has an implementation
    /// for filtering it against other entities in a component.
    /// </summary>
    public interface IComponentFilterable
    {
        /// <summary>
        /// Converts to lower.
        /// </summary>
        /// <returns></returns>
        public string ToLower();
    }
}
