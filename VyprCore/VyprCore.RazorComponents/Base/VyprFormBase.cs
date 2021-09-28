// <copyright file="VyprCoreComponentBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;

    /// <summary>
    /// Vypr Core Component base class.
    /// Logically bundles common functionality between Vypr Core Components.
    /// This class inherits <see cref="Microsoft.AspNetCore.Components.ComponentBase"/>.
    /// </summary>
    public abstract class VyprFormBase : VyprComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprFormBase"/> class.
        /// </summary>
        public VyprFormBase(string instanceName) : base(instanceName)
        {
        }

        /// <summary>
        /// Gets or sets the cascading edit context.
        /// </summary>
        /// <value>
        /// The edit context.
        /// </value>
        [CascadingParameter]
        public EditContext EditContext { get; set; } = default!;

        /// <summary>
        /// Somes the base method.
        /// </summary>
        /// <returns></returns>
        public string ComponentId => _componentId;
    }
}
