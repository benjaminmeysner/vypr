// <copyright file="UserTokenRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Identity.Repository
{
    using VyprCore.Foundation.BaseClasses.Repository;
    using VyprCore.Foundation.Context;
    using VyprCore.Models.Domain;

    /// <summary>
    /// User Tokens Repository Class.
    /// </summary>
    public class VyprUserTokenRepository : BaseRepository<VyprUserToken, VyprDbContext>
    {
        /// <summary>Initializes a new instance of the <see cref="VyprUserTokenRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public VyprUserTokenRepository(VyprDbContext context) : base(context)
        {
        }
    }
}