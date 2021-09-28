// <copyright file="RolesApiController.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Interfaces.Strategy;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;
    using System.Threading.Tasks;
    using Vypr.Server.Data.Repository;
    using Vypr.Server.Data.Strategy;
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// Roles api controller.
    /// </summary>
    /// <seealso cref="Vypr.Server.BaseClasses.Controller.VyprBaseApiController{VyprCore.Models.Domain.VyprRole, VyprCore.Models.ViewModels.VyprRoleViewModel, Vypr.Server.Roles.Repository.VyprRoleRepository, Vypr.Server.Roles.Strategy.RoleStrategy}" />
    [Authorize]
    [Route("api/users")]
    public class VyprUserApiController : VyprBaseApiController<VyprUser, VyprUserViewModel, VyprUserRepository, VyprUserStrategy>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantController"/> class.
        /// </summary>
        /// <param name="strategy">The strategy.</param>
        public VyprUserApiController(IStrategy<VyprUser, VyprUserViewModel, VyprUserRepository> strategy) : base(strategy)
        {
        }

        /// <summary>
        /// Queries using the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns>Query result.</returns>
        public override Task<ActionResult<IQueryResult<VyprUserViewModel>>> Query(string filter = null, string orderBy = null, int? skip = null, int? top = null)
        {
            return base.Query(filter, orderBy, skip, top);
        }
    }
}
