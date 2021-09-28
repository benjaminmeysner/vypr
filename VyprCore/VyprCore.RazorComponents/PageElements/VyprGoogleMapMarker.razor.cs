// <copyright file="VyprCoreGoogleMapMarker.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.PageElements
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.Models.Models;

    /// <summary>
    /// Vypr google map.
    /// </summary>
    public partial class VyprGoogleMapMarker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprGoogleMapMarker"/> class.
        /// </summary>
        public VyprGoogleMapMarker() : base(nameof(VyprGoogleMapMarker))
        {
        }

        [Parameter]
        public MapCoordinates Position { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Label { get; set; }
    }
}