// <copyright file="VyprCoreTabs.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Containers
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Vypr Core tab item.
    /// </summary>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprComponentBase" />
    public partial class VyprTabs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vyprabs"/> class.
        /// </summary>
        public VyprTabs() : base(typeof(VyprTabs).Name)
        {
        }

        [Parameter]
        public bool Stretch { get; set; } = false;

        [Parameter]
        public bool Visible { get; set; } = true;

        [Parameter]
        public int SelectedIndex { get; set; } = 0;

        [Parameter]
        public EventCallback SelectedIndexChanged { get; set; }

        /// <summary>
        /// Sets the index of the selected.
        /// </summary>
        /// <param name="index">The index.</param>
        public void SetSelectedIndex(int index)
        {
            SelectedIndex = index;
        }
    }
}