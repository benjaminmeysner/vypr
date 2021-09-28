// <copyright file="VyprCoreXOverYProgressBar.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.PageElements
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// X over Y progress bar
    /// </summary>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprComponentBase" />
    public partial class VyprXOverYProgressBar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprXOverYProgressBar"/> class.
        /// </summary>
        public VyprXOverYProgressBar() : base(nameof(VyprXOverYProgressBar))
        {
        }

        [Parameter]
        public double Value { get; set; }

        [Parameter]
        public double Max { get; set; }
    }
}