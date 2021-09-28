// <copyright file="VyprCoreValidationResult.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Containers
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Vypr core validation results.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprValidationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprValidationResult"/> class.
        /// </summary>
        public VyprValidationResult() : base(nameof(VyprValidationResult))
        {
        }

        [Parameter]
        public bool Success { get; set; }
    }
}