// <copyright file="ToolTipPosition" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Models
{
    using VyprCore.Interfaces.Client;

    /// <summary>
    /// Tooltip pos.
    /// </summary>
    /// <seealso cref="VyprCore.Interfaces.Client.IToolTipPosition" />
    public class ToolTipPosition : IToolTipPosition
    {
        public int Position { get; set; }
    }
}
