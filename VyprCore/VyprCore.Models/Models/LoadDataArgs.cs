// <copyright file="LoadDataArgs.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Models
{
    using VyprCore.Interfaces.Client;

    /// <summary>
    /// Class model for Data grid load arguments.
    /// </summary>
    public class LoadDataArgs : IDataGridFilteringArgs
    {
        public int? Skip { get; set; }

        public int? Take { get; set; }

        public string OrderBy { get; set; }

        public string Filter { get; set; }
    }
}
