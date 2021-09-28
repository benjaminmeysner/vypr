// <copyright file="VyprCoreTabsItem.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Containers
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Vypr Core tab panel.
    /// </summary>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprComponentBase" />
    public partial class VyprTabsItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprTabsItem"/> class.
        /// </summary>
        public VyprTabsItem() : base(typeof(VyprTabsItem).Name)
        {
        }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Icon { get; set; }
    }
}