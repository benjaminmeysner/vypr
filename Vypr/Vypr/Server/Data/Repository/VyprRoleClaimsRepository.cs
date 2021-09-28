// <copyright file="RoleClaimsRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Data.Repository
{
    using Vypr.Server.Data;
    using VyprCore.Models.Domain;

    /// <summary>
    /// Role claims repository.
    /// </summary>
    /// <seealso cref="Vypr.Server.BaseClasses.Repository.BaseRepository&lt;VyprCore.Models.Domain.VyprRoleClaim, Vypr.Server.DbContext.VyprDbContext&gt;" />
    public class VyprRoleClaimsRepository : BaseRepository<VyprRoleClaim, VyprDbContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprRoleClaimsRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public VyprRoleClaimsRepository(VyprDbContext dbContext) : base(dbContext)
        {
        }
    }
}
