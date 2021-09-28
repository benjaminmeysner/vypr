// <copyright file="VyprCoreComponentBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    using Microsoft.AspNetCore.Components;
    using System;

    /// <summary>
    /// Fundamental component base class.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public class VyprComponentBase : ComponentBase
    {
        protected readonly string _componentId;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprComponentBase"/> class.
        /// </summary>
        public VyprComponentBase(string instanceName)
        {
            _componentId = $"{instanceName}_{Guid.NewGuid().ToString()}";
        }

        [Parameter]
        public string Class { get; set; } = string.Empty!;

        [Parameter]
        public string Style { get; set; } = string.Empty!;
    }
}
