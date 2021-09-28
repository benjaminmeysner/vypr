// <copyright file="VyprIcon.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.PageElements
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.RazorComponents.Base;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr Icon.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprIcon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprIcon"/> class.
        /// </summary>
        public VyprIcon() : base(nameof(VyprIcon))
        {
        }

        [Parameter]
        public EventCallback Click { get; set; }

        [Parameter]
        public string Icon { get; set; }

        private async Task OnClickAsync()
        {
            await Click.InvokeAsync();
        }
    }
}