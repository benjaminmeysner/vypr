// <copyright file="VyprCoreDialogService" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Services
{
    using Radzen;
    using VyprCore.Interfaces.Client;
    using VyprCore.RazorComponents.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr dialog service.
    /// </summary>
    public class VyprDialogService : IDisposable, IDialogService
    {
        private readonly DialogService _radzenDialogService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDialogService"/> class.
        /// </summary>
        /// <param name="radzenDialogService">The radzen dialog service.</param>
        public VyprDialogService(DialogService radzenDialogService)
        {
            _radzenDialogService = radzenDialogService;

            _radzenDialogService.OnOpen += Open;
            _radzenDialogService.OnClose += Close;
        }

        /// <summary>
        /// Confirms the asynchronous.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<bool?> ConfirmAsync(string title, string message)
        {
            return await _radzenDialogService.Confirm(message, title, new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
        }

        /// <summary>
        /// Opens the error message asynchronous.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<dynamic> OpenErrorAsync(string title, string message)
        {
            return await _radzenDialogService.OpenAsync(title, x => VyprComponentHelpers.CreateErrorDiaglogRenderTree(message));
        }

        /// <summary>
        /// Opens the success message asynchronous.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<dynamic> OpenSuccessAsync(string title, string message)
        {
            return await _radzenDialogService.OpenAsync(title, x => VyprComponentHelpers.CreateSuccessDiaglogRenderTree(message));
        }

        /// <summary>
        /// Opens the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="type">The type.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="options">The options.</param>
        private void Open(string title, Type type, Dictionary<string, object> parameters, DialogOptions options)
        {
        }

        /// <summary>
        /// Closes the specified result.
        /// </summary>
        /// <param name="result">The result.</param>
        private void Close(dynamic result)
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // The DialogService is a singleton so it is advisable to unsubscribe.
            _radzenDialogService.OnOpen -= Open;
            _radzenDialogService.OnClose -= Close;
        }
    }
}
