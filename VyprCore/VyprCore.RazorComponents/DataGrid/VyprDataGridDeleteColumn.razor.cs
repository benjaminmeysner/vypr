// <copyright file="VyprCoreDataGridDeleteColumn" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.DataGrid
{
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;

    public partial class VyprDataGridDeleteColumn<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDataGridDeleteColumn{TModel}"/> class.
        /// TODO: Make sure any property implementing this column must provide a EditTemplate as ChildContent.
        /// </summary>
        public VyprDataGridDeleteColumn() : base(typeof(VyprDataGridDeleteColumn<>).Name)
        { }

        [Parameter]
        public EventCallback<TModel> OnRowDelete { get; set; }

        /// <summary>
        /// Internals the on row delete asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        private async Task InternalOnRowDeleteAsync(TModel model)
        {
            await OnRowDelete.InvokeAsync(model);
        }
    }
}