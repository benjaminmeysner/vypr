// <copyright file="VyprCoreCheckBox.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Forms
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Vypr Core Drop Down.
    /// </summary>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprComponentBase" />
    public partial class VyprCheckBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprCheckBox"/> class.
        /// </summary>
        public VyprCheckBox() : base(typeof(VyprCheckBox).Name)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the checkbox will be used [in grid].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [in grid]; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool InGrid { get; set; } = false;

        [Parameter]
        public EventCallback<bool> Change { get; set; }
    }
}