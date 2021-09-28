// <copyright file="VyprUserTokens.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Domain
{
    using Microsoft.AspNetCore.Identity;
    using VyprCore.Interfaces.Entity;

    /// <summary>
    /// Vypr user tokens.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUserToken{System.Int32}" />
    public class VyprUserToken : IdentityUserToken<int>, IEntity
    {
        public virtual VyprUser User { get; set; }

        public int Id => UserTokenId;

        public int UserTokenId { get; set; }
    }
}
