// <copyright file="VyprCoreCrudGridBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Client
{
    using System.Collections.Generic;

    /// <summary>
    /// Data grid action interface.
    /// </summary>
    public interface IDataGridActions<TViewModel>
    {
        public IToastService ToastService { get; set; }

        public IList<TViewModel> SelectedModels { get; set; }
    }
}
