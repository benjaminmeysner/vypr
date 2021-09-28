// <copyright file="VyprCoreDataListBase.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    using Microsoft.AspNetCore.Components;
    using Radzen.Blazor;
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;
    using System.Collections.Generic;

    /// <summary>
    /// Vypr base datalist.
    /// </summary>
    public class VyprDataListBase<TViewModel> : VyprComponentBase
        where TViewModel : IEntityViewModel, ICloneableViewModel<TViewModel>
    {
        private IEnumerable<TViewModel> _dataSource;
        protected RadzenDataList<TViewModel> ListInternal;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDataListBase"/> class.
        /// </summary>
        public VyprDataListBase(string instanceName) : base(instanceName)
        {
        }

        [Parameter]
        public int PageSize { get; set; }

        [Parameter]
        public IEnumerable<TViewModel> DataSource
        {
            get => _dataSource;
            set => _dataSource = value;
        }
    }
}
