// <copyright file="VyprCoreComponentBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Vypr Core Component base class.
    /// Logically bundles common functionality between Vypr Core Components.
    /// This class inherits <see cref="Microsoft.AspNetCore.Components.ComponentBase"/>.
    /// </summary>
    public abstract class VyprButtonBase : VyprComponentBase
    {
        protected const string DropShadowStyle = "box-shadow: 0 0 10px #535353c4;";

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprButtonBase"/> class.
        /// </summary>
        public VyprButtonBase(string instanceName) : base(instanceName)
        {
        }

        /// <summary>
        /// Somes the base method.
        /// </summary>
        /// <returns></returns>
        public string ComponentId => _componentId;

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public EventCallback OnClick { get; set; }

        [Parameter]
        public bool WaitingOnResponse { get; set; }
    }
}
