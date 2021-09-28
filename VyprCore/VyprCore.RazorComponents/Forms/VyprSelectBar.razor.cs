// <copyright file="VyprCoreSelectBar.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Forms
{
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr select bar.
    /// </summary>
    public partial class VyprSelectBar<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprSelectBar"/> class.
        /// </summary>
        public VyprSelectBar() : base(nameof(VyprSelectBar<TValue>))
        {
        }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Multiple { get; set; } = false;

        [Parameter]
        public TValue Value { get; set; }

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public bool Stretch { get; set; } = false;

        protected async Task OnChange(TValue value)
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
        }

        private string GetClass()
        {
            var stretch = Stretch ? "vypr-stretch-selectbar" : string.Empty;
            return $"{stretch} {Class}";
        }
    }
}