// <copyright file="UserClaimsRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Identity.Repository
{
    using VyprCore.Foundation.BaseClasses.Repository;
    using VyprCore.Foundation.Context;
    using VyprCore.Models.Domain;

    /// <summary>
    /// User Claims Repository Class.
    /// </summary>
    public class VyprUserClaimsRepository : BaseRepository<VyprUserClaim, VyprDbContext>
    {
        /// <summary>Initializes a new instance of the <see cref="UserClaimsRepositoryUserClaimsRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public VyprUserClaimsRepository(VyprDbContext context) : base(context)
        {
        }
    }
}