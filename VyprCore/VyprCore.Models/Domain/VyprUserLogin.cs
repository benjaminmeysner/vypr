// <copyright file="Startup.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Domain
{
    using Microsoft.AspNetCore.Identity;
    using VyprCore.Interfaces.Entity;

    /// <summary>
    /// Vypr user logins.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUserLogin{System.Int32}" />
    public class VyprUserLogin : IdentityUserLogin<int>, IEntity
    {
        public virtual VyprUser User { get; set; }

        public int Id => UserLoginId;

        public int UserLoginId { get; set; }
    }
}
