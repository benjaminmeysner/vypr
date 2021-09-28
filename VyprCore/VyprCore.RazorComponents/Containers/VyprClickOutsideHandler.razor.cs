// <copyright file="VyprCoreClickOutsideHandler.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Containers
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;
    using VyprCore.RazorComponents.Base;
    using VyprCore.RazorComponents.Helpers;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates a container where you can handle clicks which happen outside of this container.
    /// </summary>
    public partial class VyprClickOutsideHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprClickOutsideHandler"/> class.
        /// </summary>
        public VyprClickOutsideHandler() : base(nameof(VyprClickOutsideHandler))
        {
        }

        [Parameter]
        public Action OnClickOutside { get; set; }

        [Inject]
        public IJSRuntime JSInterop { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.
        /// </returns>
        protected override Task OnInitializedAsync()
        {
            JSInterop.InvokeAsync<object>(VyprJSFunctions.OutsideClickHandler, ComponentId, DotNetObjectReference.Create(this));
            return base.OnInitializedAsync();
        }

        /// <summary>
        /// Invokes the click outside.
        /// </summary>
        [JSInvokable]
        public void InvokeClickOutside()
        {
            OnClickOutside?.Invoke();
        }
    }
}