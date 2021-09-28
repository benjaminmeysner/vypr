// <copyright file="EmailTemplater.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Authentication.Senders
{
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using Vypr.Server.Settings;
    using VyprCore.Interfaces.Email;

    /// <summary>
    /// Email Templater
    /// </summary>
    /// <seealso cref="Vypr.Server.Interfaces.IEmailTemplater" />
    public class EmailTemplater : IEmailTemplater
    {
        private readonly IWebHostEnvironment _env;
        private readonly IEmailSender _emailSender;
        private readonly string _defaultTemplateName = "email_template.html";

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailTemplater" /> class.
        /// </summary>
        /// <param name="emailSender">The email sender.</param>
        /// <param name="env">The env.</param>
        /// <param name="options">The options.</param>
        public EmailTemplater(IEmailSender emailSender, IWebHostEnvironment env, IOptions<EmailConfig> options)
        {
            _env = env;
            _emailSender = emailSender;
            EmailConfig = options.Value;
        }

        /// <summary>
        /// Gets or sets the email configuration.
        /// </summary>
        /// <value>
        /// The email configuration.
        /// </value>
        public EmailConfig EmailConfig { get; set; }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="toEmailAddress">To email address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="data">The data to be added to the html template</param>
        /// <param name="templateName">Name of the template, otherwise will use email_template.html</param>
        public async Task TemplateAndSendEmail(string toEmailAddress, string subject, IEmailTemplateModel data, string templateName = null)
        {
            // Get the email template, either default or tenant derived.
            var templateToUse = GetAvailableEmailTemplate(templateName);

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = File.OpenText(templateToUse))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            string htmlMessage = Regex.Replace(builder.HtmlBody, @"\|(.+?)\|", m => data[m.Groups[1].Value]);

            await _emailSender.SendEmailAsync(toEmailAddress, subject, htmlMessage);
        }

        /// <summary>
        /// Gets the available email template to send.
        /// Either default or tenant derived.
        /// </summary>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="tenant">The tenant.</param>
        /// <returns></returns>
        private string GetAvailableEmailTemplate(string templateName)
        {
            var defaultEmailTemplateFile = Path.Combine(
                _env.WebRootPath,
                @"Templates\Default\EmailTemplate\",
                !string.IsNullOrEmpty(templateName) ? templateName : _defaultTemplateName);

            var tenantEmailTemplateFile = Path.Combine(
                _env.WebRootPath,
                @$"Templates\EmailTemplate\",
                !string.IsNullOrEmpty(templateName) ? templateName : _defaultTemplateName);

            return File.Exists(tenantEmailTemplateFile) ? tenantEmailTemplateFile : defaultEmailTemplateFile;
        }
    }
}
