// <copyright file="VyprCoreDataGridActions.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.DataGrid
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Vypr Core crud actions.
    /// </summary>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprComponentBase" />
    public partial class VyprDataGridActions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDataGridActions"/> class.
        /// </summary>
        public VyprDataGridActions()
        {
        }

        [Parameter]
        public RenderFragment Actions { get; set; }
    }
}