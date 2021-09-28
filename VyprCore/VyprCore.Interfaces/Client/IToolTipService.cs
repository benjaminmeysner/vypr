// <copyright file="IToolTipService" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Components;

namespace VyprCore.Interfaces.Client
{
    /// <summary>
    /// Tool tip service interface.
    /// </summary>
    public interface IToolTipService
    {
        /// <summary>
        /// Shows the information.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void Open(ElementReference element, string message, IToolTipPosition position);
    }
}
