// <copyright file="IToastService" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Client
{
    /// <summary>
    /// Data grid action interface.
    /// </summary>
    public interface IToastService
    {
        /// <summary>
        /// Shows the information.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void ShowInfo(string title, string message);

        /// <summary>
        /// Shows the information.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void ShowWarning(string title, string message);

        /// <summary>
        /// Shows the success.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void ShowSuccess(string title, string message);

        /// <summary>
        /// Shows the error.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void ShowError(string title, string message);
    }
}
