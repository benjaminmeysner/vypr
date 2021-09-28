// <copyright file="VyprToast" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.PageElements
{
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;

    /// <summary>
    /// VyprMiniCard
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprMiniCard<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprMiniCard"/> class.
        /// </summary>
        public VyprMiniCard()
        {
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public async Task SetValueAsync(TValue value)
        {
            Value = value;
            await InvokeAsync(StateHasChanged);
        }

        [Parameter]
        public TValue Value { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Icon { get; set; }
    }
}