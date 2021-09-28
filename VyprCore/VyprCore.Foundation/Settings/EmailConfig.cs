// <copyright file="EmailConfig.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Settings
{
    /// <summary>
    /// Email config.
    /// </summary>
    public class EmailConfig
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSSL { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Sender { get; set; }

        public string RegistrationEmailTitle { get; set; }

        public string RegistrationEmailText { get; set; }
    }
}
