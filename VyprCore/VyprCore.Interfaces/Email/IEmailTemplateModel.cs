// <copyright file="EmailTemplateModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Email
{
    using System.Collections.Generic;

    /// <summary>
    /// Email template model interface.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IDictionary&lt;System.String, System.String&gt;" />
    public interface IEmailTemplateModel : IDictionary<string, string>
    {
    }
}