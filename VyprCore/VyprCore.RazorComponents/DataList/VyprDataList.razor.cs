// <copyright file="VyprCoreDataList.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.DataList
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;

    /// <summary>
    /// Vypr datalist.
    /// </summary>
    public partial class VyprDataList<TModel> where TModel : ICloneableViewModel<TModel>, IEntityViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDataList"/> class.
        /// </summary>
        public VyprDataList() : base(typeof(VyprDataList<>).Name)
        {
        }

        [Parameter]
        public RenderFragment<TModel> Template { get; set; }
    }
}