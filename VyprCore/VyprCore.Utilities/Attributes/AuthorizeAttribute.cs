// <copyright file="VyprAuthorizeAttribute.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>


namespace VyprCore.Utilities.Attributes
{
    using System;

    /// <summary>
    /// Vypr Authorize Attribute
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true)]
    public class AuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        /// <summary>
        /// Gets the type of the role.
        /// </summary>
        /// <value>
        /// The type of the role.
        /// </value>
        public Type RoleType { get; }

        /// <summary>
        /// Gets the claims.
        /// </summary>
        /// <value>
        /// The claims.
        /// </value>
        public string[] Claims { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprAuthorize" /> class.
        /// </summary>
        /// <param name="roleType">Type of the role.</param>
        /// <param name="roleTypeFullyQualifiedName">Name of the role type fully qualified.</param>
        public AuthorizeAttribute(Type roleType = null, string roleTypeFullyQualifiedName = null) : base("Permission")
        {
            if (roleType == null && string.IsNullOrEmpty(roleTypeFullyQualifiedName))
            {
                throw new ArgumentNullException("either roletype or roleTypeFullyQualifiedName is required");
            }

            RoleType = roleType;

            if (!string.IsNullOrEmpty(roleTypeFullyQualifiedName))
            {
                RoleType = Type.GetType(roleTypeFullyQualifiedName);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HasClaimsAttribute"/> class.
        /// </summary>
        /// <param name="claims">The claims.</param>
        public AuthorizeAttribute(params string[] claims) : base("Permission")
        {
            Claims = claims;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HasClaimsAttribute"/> class.
        /// </summary>
        /// <param name="claim">The claim.</param>
        public AuthorizeAttribute(string claim) : base("Permission")
        {
            Claims = new[] { claim };
        }
    }
}
