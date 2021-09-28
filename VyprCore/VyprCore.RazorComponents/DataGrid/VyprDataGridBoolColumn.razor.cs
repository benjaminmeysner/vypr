// <copyright file="VyprCoreDataGridBoolColumn" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.DataGrid
{
    using System;
    using System.Reflection;

    /// <summary>
    /// data bool column for grid.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprDataGridBase{TModel}" />
    public partial class VyprDataGridBoolColumn<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDataGridColumn{TModel}"/> class.
        /// </summary>
        public VyprDataGridBoolColumn() : base(typeof(VyprDataGridBoolColumn<>).Name)
        { }

        private bool GetBoolValueOfProperty(TModel model)
        {
            PropertyInfo prop = typeof(TModel).GetProperty(Property);

            if (prop is null)
            {
                throw new ArgumentException($"Cannot bind to property {model}");
            }

            return (bool)prop.GetValue(model);
        }
    }
}