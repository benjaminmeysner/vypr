// <copyright file="TenantRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Data.Repository
{
    using Vypr.Server.Data;
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