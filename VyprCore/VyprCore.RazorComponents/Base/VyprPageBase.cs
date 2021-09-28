// <copyright file="VyprCorePageBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    using System;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.Forms;
    using VyprCore.RazorComponents.Helpers;
    using VyprCore.Client.Api;
    using VyprCore.Interfaces.Client;

    /// <summary>
    /// Vypr Core Component base class.
    /// Logically bundles common functionality between Vypr Core Components.
    /// This class inherits <see cref="Microsoft.AspNetCore.Components.ComponentBase"/>.
    /// </summary>
    public class VyprPageBase : ApiComponent
    {
        private readonly string _routePath;
        private readonly string _componentId;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprPageBase"/> class.
        /// </summary>
        public VyprPageBase(string routePath, string instanceName)
        {
            _routePath = routePath;
            _componentId = $"{instanceName}_{Guid.NewGuid().ToString()}";
        }

        /// <summary>
        /// Gets or sets the navigation manager.
        /// </summary>
        /// <value>
        /// The navigation manager.
        /// </value>
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
    }
}
