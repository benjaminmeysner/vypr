// <copyright file="VyprUser.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Domain
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using VyprCore.Interfaces.Entity;

    /// <summary>
    /// Vypr user.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser{System.Int32}" />
    public class VyprUser : IdentityUser<int>, IEntity
    {
        /// <summary>
        /// Gets or sets the registration token.
        /// </summary>
        /// <value>
        /// The registration token.
        /// </value>
        public string RegistrationToken { get; set; }

        /// <summary>
        /// Gets or sets the registration token expiry.
        /// </summary>
        /// <value>
        /// The registration token expiry.
        /// </value>
        public DateTime? RegistrationTokenExpiry { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [invitation sent].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [invitation sent]; otherwise, <c>false</c>.
        /// </value>
        public bool InvitationSent { get; set; }

        /// <summary>
        /// Gets or sets the user claims.
        /// </summary>
        /// <value>
        /// The user claims.
        /// </value>
        public virtual ICollection<VyprUserClaim> UserClaims { get; set; }

        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        /// <value>
        /// The user claims.
        /// </value>
        public virtual ICollection<VyprUserRole> UserRoles { get; set; }


        /// <summary>
        /// Gets or sets the user tokens.
        /// </summary>
        /// <value>
        /// The user tokens.
        /// </value>
        public virtual ICollection<VyprUserToken> UserTokens { get; set; }

        /// <summary>
        /// Gets or sets the user logins.
        /// </summary>
        /// <value>
        /// The user logins.
        /// </value>
        public virtual ICollection<VyprUserLogin> UserLogins { get; set; }

        /// <summary>
        /// Gets or sets the system administrator.
        /// </summary>
        /// <value>
        /// The system administrator.
        /// </value>
        public virtual VyprSystemAdministrator SystemAdministrator { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VyprUser"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }
    }
}
