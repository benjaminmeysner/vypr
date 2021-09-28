// <copyright file="ILoadDataArgs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Client
{
    /// <summary>
    /// Data loading arguments.
    /// </summary>
    public interface IDataGridFilteringArgs
    {
        public int? Skip { get; set; }

        public int? Take { get; set; }

        public string OrderBy { get; set; }

        public string Filter { get; set; }
    }
}
