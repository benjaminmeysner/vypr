// <copyright file="VyprCoreLoaderBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    /// <summary>
    /// Vypr Core Component base class.
    /// Logically bundles common functionality between Vypr Core Components.
    /// This class inherits <see cref="Microsoft.AspNetCore.Components.ComponentBase"/>.
    /// </summary>
    public abstract class VyprLoaderBase : VyprComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprLoaderBase"/> class.
        /// </summary>
        public VyprLoaderBase(string instanceName) : base(instanceName)
        {
        }

        /// <summary>
        /// Somes the base method.
        /// </summary>
        /// <returns></returns>
        public string ComponentId => _componentId;
    }
}
