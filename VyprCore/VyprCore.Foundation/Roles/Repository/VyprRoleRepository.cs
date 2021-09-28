// <copyright file="TenantRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Roles.Repository
{
    using VyprCore.Foundation.BaseClasses.Repository;
    using VyprCore.Foundation.Context;
    using VyprCore.Models.Domain;

    /// <summary>
    /// Tenant Repository Class.
    /// </summary>
    public class VyprRoleRepository : BaseRepository<VyprRole, VyprDbContext>
    {
        /// <summary>Initializes a new instance of the <see cref="VyprRoleRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public VyprRoleRepository(VyprDbContext context) : base(context)
        {
        }
    }
}