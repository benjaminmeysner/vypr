// <copyright file="VyprCoreSignaturePad.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.PageElements
{
    using Microsoft.AspNetCore.Components;
    using Mobsites.Blazor;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr Signature Pad.
    /// </summary>
    public partial class VyprSignaturePad
    {
        private SignaturePad _signaturePad;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprSignaturePad"/> class.
        /// </summary>
        public VyprSignaturePad() : base(nameof(VyprSignaturePad))
        {
        }

        public SignaturePad Internal => _signaturePad;

        [Parameter]
        public bool ShowSignHereImage { get; set; } = false;

        [Parameter]
        public EventCallback<ChangeEventArgs> Change { get; set; }

        /// <summary>
        /// Converts to dataurl. Hardcoded to PNG format.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ToDataURL()
        {
            return await _signaturePad.ToDataURL(SignaturePad.SupportedSaveAsTypes.png);
        }
    }
}