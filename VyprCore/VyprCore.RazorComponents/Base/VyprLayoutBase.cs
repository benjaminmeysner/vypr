// <copyright file="VyprCoreLayoutBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Authorization;

    /// <summary>
    /// Vypr core layout base.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.LayoutComponentBase" />
    public abstract class VyprLayoutBase : LayoutComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprLayoutBase"/> class.
        /// </summary>
        public VyprLayoutBase()
        {
        }

        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthState { get; set; }
    }
}
