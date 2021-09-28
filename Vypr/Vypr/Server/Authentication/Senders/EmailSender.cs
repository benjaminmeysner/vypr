// <copyright file="EmailSender.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Authentication.Senders
{
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Options;
    using Vypr.Server.Settings;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    /// <summary>
    /// Email sender.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.UI.Services.IEmailSender" />
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// Gets or sets the email configuration.
        /// </summary>
        /// <value>
        /// The email configuration.
        /// </value>
        public EmailConfig EmailConfig { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public EmailSender(IOptions<EmailConfig> options)
        {
            EmailConfig = options?.Value;
        }

        /// <summary>
        /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        /// directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="htmlMessage"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(EmailConfig.Host, EmailConfig.Port)
            {
                Credentials = new NetworkCredential(EmailConfig.UserName, EmailConfig.Password),
                EnableSsl = EmailConfig.EnableSSL
            };

            await client.SendMailAsync(new MailMessage(EmailConfig.Sender, email, subject, htmlMessage) { IsBodyHtml = true });
        }
    }
}
