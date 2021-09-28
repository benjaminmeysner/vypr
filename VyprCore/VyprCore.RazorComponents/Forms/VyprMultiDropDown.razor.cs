// <copyright file="VyprCoreMultiDropDown.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Forms
{
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;

    /// <summary>
    /// Vypr Core Drop Down.
    /// </summary>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprComponentBase" />
    public partial class VyprMultiDropDown<TModel, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprMultiDropDown"/> class.
        /// </summary>
        public VyprMultiDropDown() : base(typeof(VyprMultiDropDown<,>).Name)
        {
        }

        /// <summary>
        /// Gets or sets DataSource.
        /// </summary>
        /// <value>
        /// DataSource.
        /// </value>
        [Parameter]
        public IEnumerable<TModel> DataSource { get; set; }

        /// <summary>
        /// Gets or sets the value property.
        /// </summary>
        /// <value>
        /// The value property.
        /// </value>
        [Parameter]
        public string ValueProperty { get; set; }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        [Parameter]
        public string DisplayProperty { get; set; }

        /// <summary>
        /// Gets or sets the placeholder.
        /// </summary>
        /// <value>
        /// The placeholder.
        /// </value>
        [Parameter]
        public string Placeholder { get; set; } = $"Select...";

        /// <summary>
        /// Gets or sets a value indicating whether [allow filtering].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow filtering]; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool AllowFiltering { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether [multi select].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [multi select]; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool MultiSelect { get; set; } = false;

        [Parameter]
        public RenderFragment<dynamic> Template { get; set; }
    }
}