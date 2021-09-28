// <copyright file="VyprCoreSelectBarItem.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Forms
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Vypr select bar item.
    /// </summary>
    public partial class VyprSelectBarItem<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprSelectBarItem"/> class.
        /// </summary>
        public VyprSelectBarItem() : base(nameof(VyprSelectBarItem<TValue>))
        {
        }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public TValue Value { get; set; }
    }
}