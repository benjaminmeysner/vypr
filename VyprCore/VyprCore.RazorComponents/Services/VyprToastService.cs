// <copyright file="VyprCoreToastService" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Services
{
    using Radzen;
    using VyprCore.Interfaces.Client;

    /// <summary>
    /// Vypr Toast Service.
    /// </summary>
    public class VyprToastService : IToastService
    {
        private readonly NotificationService _radzenNotificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprToastService"/> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        public VyprToastService(NotificationService notificationService)
        {
            _radzenNotificationService = notificationService;
        }

        public void ShowInfo(string title, string message)
        {
            Show(title, message, NotificationSeverity.Info);
        }

        public void ShowWarning(string title, string message)
        {
            Show(title, message, NotificationSeverity.Warning);
        }

        public void ShowSuccess(string title, string message)
        {
            Show(title, message, NotificationSeverity.Success);
        }

        public void ShowError(string title, string message)
        {
            Show(title, message, NotificationSeverity.Error);
        }

        /// <summary>
        /// Shows the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="severity">The severity.</param>
        private void Show(string title, string message, NotificationSeverity severity)
        {
            _radzenNotificationService.Notify(severity, title, message, 3000);
        }
    }
}
