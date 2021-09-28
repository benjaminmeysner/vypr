// <copyright file="VyprCoreDataGridEditColumn" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.DataGrid
{
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;

    public partial class VyprDataGridEditColumn<TModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDataGridEditColumn{TModel}"/> class.
        /// TODO: Make sure any property implementing this column must provide a EditTemplate as ChildContent.
        /// </summary>
        public VyprDataGridEditColumn() : base(typeof(VyprDataGridEditColumn<>).Name)
        { }

        [Parameter]
        public EventCallback<TModel> OnRowEdit { get; set; }

        [Parameter]
        public EventCallback<TModel> OnRowSaveEdit { get; set; }

        [Parameter]
        public EventCallback<TModel> OnRowCancelEdit { get; set; }

        /// <summary>
        /// Internals the on row edit asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        protected async Task InternalOnRowEditAsync(TModel model)
        {
            await OnRowEdit.InvokeAsync(model);
        }

        /// <summary>
        /// Internals the on row edit asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        protected async Task InternalOnRowSaveEditAsync(TModel model)
        {
            await OnRowSaveEdit.InvokeAsync(model);
        }

        /// <summary>
        /// Internals the on row edit asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        protected async Task InternalOnRowCancelEditAsync(TModel model)
        {
            await OnRowCancelEdit.InvokeAsync(model);
        }
    }
}