// <copyright file="VyprCoreComponentBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using VyprCore.RazorComponents.Helpers;

    /// <summary>
    /// Vypr Core Component base class.
    /// Logically bundles common functionality between Vypr Core Components.
    /// This class inherits <see cref="Microsoft.AspNetCore.Components.ComponentBase"/>.
    /// </summary>
    public abstract class VyprContainerBase : VyprComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprContainerBase"/> class.
        /// </summary>
        public VyprContainerBase(string instanceName) : base(instanceName)
        {
        }

        /// <summary>
        /// Somes the base method.
        /// </summary>
        /// <returns></returns>
        public string ComponentId => _componentId;

        /// <summary>
        /// Gets or sets the content of the child.
        /// </summary>
        /// <value>
        /// The content of the child.
        /// </value>
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
