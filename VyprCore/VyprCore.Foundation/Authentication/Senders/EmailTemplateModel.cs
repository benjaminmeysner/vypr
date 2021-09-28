// <copyright file="EmailTemplateModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Authentication.Senders
{
    using VyprCore.Interfaces.Email;
    using System.Collections.Generic;

    /// <summary>
    /// Template model for an email.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.Dictionary{System.String, System.String}" />
    public class EmailTemplateModel : Dictionary<string, string>, IEmailTemplateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailTemplateModel"/> class.
        /// </summary>
        public EmailTemplateModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailTemplateModel"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="image">The image.</param>
        /// <param name="message">The message.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="sysName">Name of the system.</param>
        /// <param name="footer1">The footer1.</param>
        /// <param name="footer2">The footer2.</param>
        /// <param name="footer3">The footer3.</param>
        /// <param name="footer4">The footer4.</param>
        /// <param name="footer5">The footer5.</param>
        /// <param name="footer6">The footer6.</param>
        private EmailTemplateModel(
            string title, string image, string message, string fullName, string sysName, string footer1, string footer2,
            string footer3, string footer4, string footer5, string footer6)
        {
            Add("Title", title);
            Add("Image", image);
            Add("Message", message);
            Add("FullName", fullName);
            Add("System Name", sysName);
            Add("FooterLine1", footer1);
            Add("FooterLine2", footer2);
            Add("FooterLine3", footer3);
            Add("FooterLine4", footer4);
            Add("FooterLine5", footer5);
            Add("FooterLine6", footer6);
        }

        /// <summary>
        /// Creates a new instance of the email template model.
        /// Any parameters that are not supplied are replaced by empty strings.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="image">The image.</param>
        /// <param name="message">The message.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="sysName">Name of the system.</param>
        /// <param name="footer1">The footer1.</param>
        /// <param name="footer2">The footer2.</param>
        /// <param name="footer3">The footer3.</param>
        /// <param name="footer4">The footer4.</param>
        /// <param name="footer5">The footer5.</param>
        /// <param name="footer6">The footer6.</param>
        /// <returns></returns>
        public static IEmailTemplateModel Create(
            string title = null, string image = null, string message = null, string fullName = null, string sysName = null,
            string footer1 = null, string footer2 = null, string footer3 = null, string footer4 = null, string footer5 = null, string footer6 = null)
        {
            return new EmailTemplateModel(ValueOrEmpty(title), ValueOrEmpty(image), ValueOrEmpty(message), ValueOrEmpty(fullName),
                ValueOrEmpty(sysName), ValueOrEmpty(footer1), ValueOrEmpty(footer2), ValueOrEmpty(footer3), ValueOrEmpty(footer4),
                ValueOrEmpty(footer5), ValueOrEmpty(footer6));
        }

        /// <summary>
        /// Values the or empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static string ValueOrEmpty(string value) => value ?? string.Empty;
    }
}
