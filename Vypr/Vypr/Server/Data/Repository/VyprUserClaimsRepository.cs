// <copyright file="UserClaimsRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Data.Repository
{
    using Vypr.Server.Data;
    using VyprCore.Models.Domain;

    /// <summary>
    /// User Claims Repository Class.
    /// </summary>
    public class VyprUserClaimsRepository : BaseRepository<VyprUserClaim, VyprDbContext>
    {
        /// <summary>Initializes a new instance of the <see cref="VyprUserClaimsRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public VyprUserClaimsRepository(VyprDbContext context) : base(context)
        {
        }
    }
}