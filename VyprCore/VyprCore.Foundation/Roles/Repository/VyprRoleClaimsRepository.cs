// <copyright file="RoleClaimsRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Roles.Repository
{
    using VyprCore.Foundation.BaseClasses.Repository;
    using VyprCore.Foundation.Context;
    using VyprCore.Models.Domain;

    /// <summary>
    /// Role claims repository.
    /// </summary>
    /// <seealso cref="VyprCore.Foundation.BaseClasses.Repository.BaseRepository&lt;VyprCore.Models.Domain.VyprRoleClaim, VyprCore.Foundation.DbContext.VyprDbContext&gt;" />
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
