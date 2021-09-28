// <copyright file="VyprCoreTextBox.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Forms
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Vypr text box.
    /// </summary>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprComponentBase" />
    public partial class VyprTextBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprTextBox"/> class.
        /// </summary>
        public VyprTextBox() : base(typeof(VyprTextBox).Name)
        {
        }

        /// <summary>
        /// Gets or sets the changed.
        /// </summary>
        /// <value>
        /// The changed.
        /// </value>
        [Parameter]
        public EventCallback<string> Changed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use [password mask].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [password mask]; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool UsePasswordMask { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is text area.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is text area; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool IsTextArea { get; set; }

        /// <summary>
        /// Gets or sets the place holder.
        /// </summary>
        /// <value>
        /// The place holder.
        /// </value>
        [Parameter]
        public string Placeholder { get; set; }

        /// <summary>
        /// Called when [text box changed].
        /// </summary>
        /// <param name="value">The value.</param>
        public void OnTextBoxChanged(string value)
        {
            Changed.InvokeAsync(value);
        }
    }
}