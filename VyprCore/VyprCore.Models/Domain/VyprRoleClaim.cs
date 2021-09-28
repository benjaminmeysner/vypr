// <copyright file="VyprRoleClaims.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Domain
{
    using Microsoft.AspNetCore.Identity;
    using VyprCore.Interfaces.Entity;

    /// <summary>
    /// Vypr role claims.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityRoleClaim{System.Int32}" />
    public class VyprRoleClaim : IdentityRoleClaim<int>, IEntity
    {
        public virtual VyprRole Role { get; set; }
    }
}
