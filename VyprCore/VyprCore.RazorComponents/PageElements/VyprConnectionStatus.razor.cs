// <copyright file="VyprCoreConnectionStatus.razor" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.PageElements
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;
    using VyprCore.RazorComponents.Helpers;

    /// <summary>
    /// Vypr connection status. Assumed it being used in
    /// Blazor web assembly.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprConnectionStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprConnectionStatus"/> class.
        /// </summary>
        public VyprConnectionStatus()
        {
        }

        /// <summary>
        /// Called when [connection status changed].
        /// </summary>
        /// <param name="isOnline">if set to <c>true</c> [is online].</param>
        [JSInvokable("Connection.StatusChanged")]
        public void OnConnectionStatusChanged(bool isOnline)
        {
            if (IsOnline != isOnline)
            {
                IsOnline = isOnline;
            }

            StateHasChanged();
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            JSRuntime.InvokeVoidAsync(VyprJSFunctions.ConnectionInit, DotNetObjectReference.Create(this));
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            JSRuntime.InvokeVoidAsync(VyprJSFunctions.ConnectionDispose);
        }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public RenderFragment Online { get; set; }

        [Parameter]
        public RenderFragment Offline { get; set; }

        public bool IsOnline { get; set; }
    }
}