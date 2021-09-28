// <copyright file="VyprCoreDatePicker.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Forms
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Renders a Date Picker.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprDatePicker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDatePicker"/> class.
        /// </summary>
        public VyprDatePicker() : base(typeof(VyprDatePicker).Name)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating the date and time format.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [time only]; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public VyprDatePickerFormat Format { get; set; } = VyprDatePickerFormat.DateAndTime24H;
    }
}