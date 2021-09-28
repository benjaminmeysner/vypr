﻿// <copyright file="RoleStrategy.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Roles.Strategy
{
    using AutoMapper;
    using VyprCore.Foundation.BaseClasses.Strategy;
    using VyprCore.Foundation.Roles.Repository;
    using VyprCore.Interfaces.Context;
    using VyprCore.Interfaces.Logging;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Interfaces.Strategy;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Tenant strategy.
    /// </summary>
    public class RoleStrategy : BaseStrategy<VyprRole, VyprRoleViewModel, VyprRoleRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleStrategy"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public RoleStrategy(
            IRepository<VyprRole> repository,
            IMapper mapper,
            IVyprLogger logger,
            IStrategy<VyprRoleClaim, VyprRoleClaimsViewModel, VyprRoleClaimsRepository> roleClaimsStrategy,
            IApplicationContext<VyprUser> context)
            : base(repository, mapper, logger, context)
        {
            RoleClaimsStrategy = roleClaimsStrategy;
        }

        private IStrategy<VyprRoleClaim, VyprRoleClaimsViewModel, VyprRoleClaimsRepository> RoleClaimsStrategy { get; }

        public async override Task Delete(int id)
        {
            var roleClaims = (await RoleClaimsStrategy.Find(rc => rc.RoleId == id)).ToList();
            if (roleClaims?.Any() ?? false)
            {
                foreach (var rc in roleClaims)
                {
                    await RoleClaimsStrategy.Delete(rc.Id.Value);
                }
            }

            await base.Delete(id);
        }
    }
}
