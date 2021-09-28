// <copyright file="VyprGoogleMap.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.PageElements
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.Models.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr google map.
    /// </summary>
    public partial class VyprGoogleMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprGoogleMap"/> class.
        /// </summary>
        public VyprGoogleMap() : base(nameof(VyprGoogleMap))
        {
        }

        [Parameter]
        public EventCallback<MapCoordinates> MapClick { get; set; }

        [Parameter]
        public EventCallback<VyprGoogleMapMarker> MarkerClick { get; set; }

        [Parameter]
        public int Zoom { get; set; } = 3;

        [Parameter]
        public MapCoordinates Center { get; set; }

        [Parameter]
        public string ApiKey { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected async Task OnMapClick(Radzen.GoogleMapClickEventArgs args)
        {
            await MapClick.InvokeAsync(new MapCoordinates { Lat = args.Position.Lat, Lng = args.Position.Lng });
        }

        protected async Task OnMarkerClick(Radzen.Blazor.RadzenGoogleMapMarker marker)
        {
            await MarkerClick.InvokeAsync();
        }
    }
}