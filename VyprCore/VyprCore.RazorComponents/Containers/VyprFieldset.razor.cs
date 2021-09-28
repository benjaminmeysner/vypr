// <copyright file="VyprCoreFieldset.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Containers
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Vypr Core field set.
    /// </summary>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprComponentBase" />
    public partial class VyprFieldset
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprFieldset"/> class.
        /// </summary>
        public VyprFieldset() : base(typeof(VyprFieldset).Name)
        {
        }

        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        /// <value>
        /// The heading.
        /// </value>
        [Parameter]
        public string Heading { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        [Parameter]
        public string Icon { get; set; } = "input";

        /// <summary>
        /// Gets or sets a value indicating whether [allow collapse].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow collapse]; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool AllowCollapse { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether or not[open default].
        /// This only works in conjunction with the AllowCollapse property.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [open default]; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool OpenDefault { get; set; } = true;
    }
}