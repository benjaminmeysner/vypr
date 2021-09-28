// <copyright file="RoleClaimsStrategy.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Data.Strategy
{
    using AutoMapper;
    using VyprCore.Client.Result;
    using VyprCore.Interfaces.Authorization;
    using VyprCore.Interfaces.Context;
    using VyprCore.Interfaces.Logging;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Models.Domain;
    using VyprCore.Models.Models;
    using VyprCore.Models.ViewModels;
    using VyprCore.Utilities.Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Reflection;
    using System.Threading.Tasks;
    using Vypr.Server.Data.Repository;

    /// <summary>
    /// Role claims strategy.
    /// </summary>
    /// <seealso cref="Vypr.Server.BaseClasses.Strategy.BaseStrategy&lt;VyprCore.Models.Domain.VyprRoleClaim, VyprCore.Models.ViewModels.VyprRoleClaimsViewModel, Vypr.Server.Roles.Repository.VyprRoleClaimsRepository&gt;" />
    public class VyprRoleClaimsStrategy : BaseStrategy<VyprRoleClaim, VyprRoleClaimsViewModel, VyprRoleClaimsRepository>
    {
        protected IRepository<VyprRole> RoleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprRoleClaimsStrategy"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        /// <param name="roleRepository">The role repository.</param>
        public VyprRoleClaimsStrategy(
            IRepository<VyprRoleClaim> repository,
            IMapper mapper,
            IVyprLogger logger,
            IApplicationContext<VyprUser> context,
            IRepository<VyprRole> roleRepository) : base(repository, mapper, logger, context)
        {
            RoleRepository = roleRepository;
        }

        /// <summary>
        /// Bies the role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        public async Task<IQueryResult<VyprRoleClaimsViewModel>> ByRole(int roleId, string filter, string orderBy, int? skip, int? top)
        {
            var role = await RoleRepository.Get(roleId);
            var roleClaims = role.RoleClaims;

            var allClaims = VyprClaims.GetAll().ToList();
            var claimAttributePairs = allClaims.Select(c => (Claim: c, Authorisation: (AuthorizeAttribute)c.GetCustomAttribute(typeof(AuthorizeAttribute), false), Display: (DisplayAttribute)c.GetCustomAttribute(typeof(DisplayAttribute), false)));

            claimAttributePairs = claimAttributePairs.Where(cap => ((IRoleType)Activator.CreateInstance(cap.Authorisation.RoleType)).Rank >= role.RoleType.Rank);

            var filteredClaims = claimAttributePairs.Select(cap => (Claim: (string)cap.Claim.GetRawConstantValue(), Display: cap.Display)).ToList();

            var entities = filteredClaims.Select(c => new VyprRoleClaimsViewModel { ClaimType = "Permission", ClaimValue = c.Claim, ClaimDescription = c.Display.GetDescription() }).ToList();

            foreach (var entity in entities)
            {
                var roleClaim = roleClaims.FirstOrDefault(rc => rc.ClaimType == entity.ClaimType && rc.ClaimValue == entity.ClaimValue);
                entity.RoleHasClaim = roleClaim != null;
                if (entity.RoleHasClaim)
                {
                    entity.RoleId = roleClaim.RoleId;
                    entity.Id = roleClaim.Id;
                }
            }

            var query = entities.AsQueryable();

            var (Data, Count) = QueryAsViewModel(query, filter, orderBy, skip, top);

            return new BaseQueryResult<VyprRoleClaimsViewModel>
            {
                Data = Data,
                Count = Count,
                IsFiltered = !string.IsNullOrEmpty(filter)
            };
        }
    }
}
