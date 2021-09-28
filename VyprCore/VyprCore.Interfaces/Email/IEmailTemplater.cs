// <copyright file="IEmailTemplater.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Email
{
    using System.Threading.Tasks;

    /// <summary>
    /// IEmailTemplater.
    /// </summary>
    public interface IEmailTemplater
    {
        /// <summary>
        /// Applies a template and sends the email.
        /// </summary>
        /// <param name="toEmailAddress">To email address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="data">The data.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <returns></returns>
        public Task TemplateAndSendEmail(string toEmailAddress, string subject, IEmailTemplateModel data, string templateName = null);
    }
}