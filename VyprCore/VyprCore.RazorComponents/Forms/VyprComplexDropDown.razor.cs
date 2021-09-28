// <copyright file="VyprCoreComplexDropDown.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Forms
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;
    using System.Collections.Generic;

    /// <summary>
    /// Vypr.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="VyprCore.RazorComponents.Base.VyprDataGridBase&lt;TModel&gt;" />
    public partial class VyprComplexDropDown<TModel, TValue> where TModel : ICloneableViewModel<TModel>, IEntityViewModel, new()
    {

        public VyprComplexDropDown() : base(typeof(VyprComplexDropDown<,>).Name)
        { }

        [Parameter]
        public bool UseMultiselect { get; set; } = false;

        [Parameter]
        public string DisplayProperty { get; set; }

        [Parameter]
        public string ValueProperty { get; set; }

        [Parameter]
        public IEnumerable<TValue> SelectedItems { get; set; } = new List<TValue>();

        [Parameter]
        public EventCallback<IEnumerable<TValue>> ValueChanged { get; set; }

    }
}