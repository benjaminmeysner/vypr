// <copyright file="VyprCorePageSpinner" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Loaders
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Vypr core spinner.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprPageSpinner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprPageSpinner"/> class.
        /// </summary>
        public VyprPageSpinner() : base(typeof(VyprSpinner).Name)
        {
        }

        [Parameter]
        public bool ShowSpinner { get; set; } = true;

        [Parameter]
        public bool Overlay { get; set; } = false;

        [Parameter]
        public EventCallback<bool> ShowSpinnerChanged { get; set; }
    }
}