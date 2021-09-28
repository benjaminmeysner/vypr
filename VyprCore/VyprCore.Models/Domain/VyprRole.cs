// <copyright file="Startup.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Domain
{
    using Microsoft.AspNetCore.Identity;
    using VyprCore.Interfaces.Entity;
    using System.Collections.Generic;

    /// <summary>
    /// Vypr role.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityRole{System.Int32}" />
    public class VyprRole : IdentityRole<int>, IEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VyprRole"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the type of the role.
        /// </summary>
        /// <value>
        /// The type of the role.
        /// </value>
        public virtual VyprRoleType RoleType { get; set; }

        /// <summary>
        /// Gets or sets the role claims.
        /// </summary>
        /// <value>
        /// The role claims.
        /// </value>
        public virtual ICollection<VyprRoleClaim> RoleClaims { get; set; }
    }
}
