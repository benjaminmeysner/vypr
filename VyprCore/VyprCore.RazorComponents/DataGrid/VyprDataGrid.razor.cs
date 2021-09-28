// <copyright file="VyprCoreDataGrid" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.DataGrid
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;

    /// <summary>
    /// Vypr datagrid.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprDataGridBase{TModel}" />
    public partial class VyprDataGrid<TModel> where TModel : ICloneableViewModel<TModel>, IEntityViewModel, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDataGrid{TModel}"/> class.
        /// </summary>
        public VyprDataGrid() : base(typeof(VyprDataGrid<>).Name)
        {
        }

        [Parameter]
        public bool UsePageSizeFooter { get; set; } = true;
    }
}
