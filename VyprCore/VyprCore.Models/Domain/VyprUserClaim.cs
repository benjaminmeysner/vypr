// <copyright file="VyprUserClaims.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Domain
{
    using Microsoft.AspNetCore.Identity;
    using VyprCore.Interfaces.Entity;

    /// <summary>
    /// Vypr user claims.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUserClaim{System.Int32}" />
    public class VyprUserClaim : IdentityUserClaim<int>, IEntity
    {
        public virtual VyprUser User { get; set; }
    }
}
