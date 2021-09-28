// <copyright file="VyprCoreDataGridColumnBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using VyprCore.RazorComponents.Helpers;

    /// <summary>
    /// Vypr Core Column base class.
    /// Logically bundles common functionality between Vypr Core Components.
    /// This class inherits <see cref="Microsoft.AspNetCore.Components.ComponentBase"/>.
    /// </summary>
    public abstract class VyprDataGridColumnBase<TModel> : VyprComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDataGridColumnBase"/> class.
        /// </summary>
        public VyprDataGridColumnBase(string instanceName) : base(instanceName)
        {
        }

        /// <summary>
        /// Somes the base method.
        /// </summary>
        /// <returns></returns>
        public string ComponentId => _componentId;

        [Parameter]
        public string Property { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public bool Sortable { get; set; } = true;

        [Parameter]
        public bool Filterable { get; set; } = true;

        [Parameter]
        public string SortProperty { get; set; }

        [Parameter]
        public string FilterProperty { get; set; }

        [Parameter]
        public string Width { get; set; }

        [Parameter]
        public RenderFragment FooterTemplate { get; set; }

        [Parameter]
        public RenderFragment FilterTemplate { get; set; }

        [Parameter]
        public RenderFragment<TModel> EditTemplate { get; set; }

        [Parameter]
        public RenderFragment<TModel> Template { get; set; }

        [Parameter]
        public bool Monospaced { get; set; } = false;
    }
}
