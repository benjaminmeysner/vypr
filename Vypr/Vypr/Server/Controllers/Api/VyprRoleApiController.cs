// <copyright file="RolesApiController.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Interfaces.Strategy;
    using VyprCore.Models.Domain;
    using VyprCore.Models.Models;
    using VyprCore.Models.ViewModels;
    using VyprCore.Utilities.Attributes;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Vypr.Server.Data.Repository;
    using Vypr.Server.Data.Strategy;

    /// <summary>
    /// Roles api controller.
    /// </summary>
    /// <seealso cref="Vypr.Server.BaseClasses.Controller.VyprBaseApiController{VyprCore.Models.Domain.VyprRole, VyprCore.Models.ViewModels.VyprRoleViewModel, Vypr.Server.Roles.Repository.VyprRoleRepository, Vypr.Server.Roles.Strategy.RoleStrategy}" />
    [Route("api/roles")]
    public class VyprRoleApiController : VyprBaseApiController<VyprRole, VyprRoleViewModel, VyprRoleRepository, VyprRoleStrategy>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantController"/> class.
        /// </summary>
        /// <param name="strategy">The strategy.</param>
        public VyprRoleApiController(IStrategy<VyprRole, VyprRoleViewModel, VyprRoleRepository> strategy) : base(strategy)
        {
        }

        /// <summary>
        /// Deletes the specified entity with identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Authorize(VyprClaims.Role_Delete)]
        public override Task<ActionResult<VyprRole>> Delete(int id)
        {
            return base.Delete(id);
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns></returns>
        [Authorize(VyprClaims.Role_Read)]
        public override Task<ActionResult<IEnumerable<VyprRoleViewModel>>> Get()
        {
            return base.Get();
        }

        /// <summary>
        /// Gets the specified entity with identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Authorize(VyprClaims.Role_Read)]
        public override Task<ActionResult<VyprRoleViewModel>> Get(int id)
        {
            return base.Get(id);
        }

        /// <summary>
        /// Posts the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [Authorize(VyprClaims.Role_Create)]
        public override Task<ActionResult<VyprRole>> Post(VyprRoleViewModel viewModel)
        {
            return base.Post(viewModel);
        }

        /// <summary>
        /// Puts the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [Authorize(VyprClaims.Role_Update)]
        public override Task<IActionResult> Put(VyprRoleViewModel viewModel)
        {
            return base.Put(viewModel);
        }

        /// <summary>
        /// Queries using the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        [Authorize(VyprClaims.Role_Read)]
        public override Task<ActionResult<IQueryResult<VyprRoleViewModel>>> Query(string filter = null, string orderBy = null, int? skip = null, int? top = null)
        {
            return base.Query(filter, orderBy, skip, top);
        }

        /// <summary>
        /// Queries using the specified filter.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(VyprClaims.Role_Read)]
        public override Task<ActionResult<IQueryResult<VyprRoleViewModel>>> Query([FromBody] QueryViewModel<VyprRole> model)
        {
            return base.Query(model);
        }
    }
}
