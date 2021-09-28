// <copyright file="VyprCoreDataGridIdentityColumn" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.DataGrid
{
    /// <summary>
    /// Vypr data grid identity column.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public partial class VyprDataGridIdentityColumn<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDataGridIdentityColumn{TModel}"/> class.
        /// </summary>
        public VyprDataGridIdentityColumn() : base(typeof(VyprDataGridIdentityColumn<>).Name)
        { }
    }
}