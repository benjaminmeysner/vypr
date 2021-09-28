// <copyright file="VyprCoreDataGridColumn" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.DataGrid
{
    /// <summary>
    /// Vypr data grid column.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprDataGridBase{TModel}" />
    public partial class VyprDataGridColumn<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDataGridColumn{TModel}"/> class.
        /// </summary>
        public VyprDataGridColumn() : base(typeof(VyprDataGridColumn<>).Name)
        { }
    }
}