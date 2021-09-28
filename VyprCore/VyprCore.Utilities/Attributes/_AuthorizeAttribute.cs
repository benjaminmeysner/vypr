// <copyright file="VyprClientAuthorize.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Attributes
{
    using Microsoft.AspNetCore.Authorization;
    using System;

    /// <summary>
    /// Client AuthorizeAttribute wrapper
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true)]
    public class _AuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        /// <summary>
        /// Gets or sets the permission.
        /// </summary>
        /// <value>
        /// The permission.
        /// </value>
        public string Permission 
        { 
            get
            {
                return Policy;
            }
            set
            {
                Policy = value;
            }
        }
    }
}
