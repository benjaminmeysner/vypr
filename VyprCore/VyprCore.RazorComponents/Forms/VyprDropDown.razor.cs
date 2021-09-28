// <copyright file="VyprCoreDropDown.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Forms
{
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;

    /// <summary>
    /// Vypr Core Drop Down. When setting the value property, the type of this property which this 
    /// name refers to is important to the binding process and must match the type of<see cref="TValue"/> otherwise binding will fail.
    /// For example, if you are binding to a propert called 'PersonID' of type <see cref="string"/> then TValue must also be of type <see cref="string"/>.
    /// </summary>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprComponentBase" />
    public partial class VyprDropDown<TModel, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDropDown"/> class.
        /// </summary>
        public VyprDropDown() : base(typeof(VyprDropDown<,>).Name)
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
        /// Gets or sets the function to call when loading data
        /// </summary>
        /// <value>
        /// The function that loads or updates the DataSource
        /// </value>
        [Parameter]
        public EventCallback<Radzen.LoadDataArgs> LoadData { get; set; }

        /// <summary>
        /// Gets or sets the value property.
        /// </summary>
        /// <value>
        /// The value property.
        /// </value>
        [Parameter]
        public string ValueProperty { get; set; }

        /// <summary>
        /// Gets or sets the change.
        /// </summary>
        /// <value>
        /// The change.
        /// </value>
        [Parameter]
        public EventCallback Change { get; set; }

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
        /// Gets or sets a value indicating whether [allow clear].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow clear]; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool AllowClear { get; set; } = true;

        [Parameter]
        public RenderFragment<dynamic> Template { get; set; }
    }
}