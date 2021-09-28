// <copyright file="VyprCoreStatusCodeResult.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Pages.Error
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.WebUtilities;

    /// <summary>
    /// Vypr status code result.
    /// </summary>
    public partial class VyprStatusCodeResult
    {
        private int _statusCode;
        private string _statusDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprStatusCodeResult"/> class.
        /// </summary>
        public VyprStatusCodeResult()
        {
        }

        [Parameter]
        public int StatusCode
        {
            get => _statusCode;
            set
            {
                _statusCode = value;
                _statusDescription = ReasonPhrases.GetReasonPhrase(_statusCode);
            }
        }
    }
}