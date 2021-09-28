// <copyright file="IToolTipService" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Client
{
    /// <summary>
    /// Tool tip service interface.
    /// </summary>
    public interface IToolTipPosition
    {
        /// <summary>
        /// Position
        /// </summary>
        /// 0 - bottom, 1 - left, 2 - top, 3 - right
        public int Position { get; set; }
    }
}
