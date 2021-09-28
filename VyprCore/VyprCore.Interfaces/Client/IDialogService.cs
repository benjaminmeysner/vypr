// <copyright file="IDialogService" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Client
{
    using System.Threading.Tasks;

    /// <summary>
    /// Data grid action interface.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Confirms the asynchronous.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Task<bool?> ConfirmAsync(string title, string message);

        /// <summary>
        /// Opens the error message asynchronous.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Task<dynamic> OpenErrorAsync(string title, string message);

        /// <summary>
        /// Opens the success message asynchronous.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Task<dynamic> OpenSuccessAsync(string title, string message);
    }
}
