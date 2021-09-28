// <copyright file="VyprCoreDataGridCheckBoxColumn" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.DataGrid
{
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr datagrid checkbox column.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprDataGridBase{TModel}" />
    public partial class VyprDataGridCheckBoxColumn<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDataGridCheckBoxColumn{TModel}"/> class.
        /// </summary>
        public VyprDataGridCheckBoxColumn() : base(typeof(VyprDataGridCheckBoxColumn<>).Name)
        { }

        [Parameter]
        public EventCallback<(bool, TModel)> ValueChanged { get; set; }

        [Parameter]
        public Func<TModel, bool> IsReadOnly { get; set; } = null;

        private async Task OnValueChangedAsync((bool Value, TModel Model) changeContext)
        {
            if (!(changeContext.Model is null))
            {
                await ValueChanged.InvokeAsync(changeContext);
            }
        }

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