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
    public class VyprUserRole : IdentityUserRole<int>, IEntity
    {
        /// <summary>
        /// Gets or sets the role claims.
        /// </summary>
        /// <value>
        /// The role claims.
        /// </value>
        public virtual ICollection<VyprRoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual VyprUser User { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int UserRoleId { get; set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id => UserRoleId;
    }
}
