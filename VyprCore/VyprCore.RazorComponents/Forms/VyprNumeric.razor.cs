// <copyright file="VyprNumeric.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Forms
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Vypr Numeric.
    /// </summary>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprComponentBase" />
    public partial class VyprNumeric<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprNumeric"/> class.
        /// </summary>
        public VyprNumeric() : base(typeof(VyprNumeric<>).Name)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show up down].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show up down]; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool ShowUpDown { get; set; } = true;
    }
}