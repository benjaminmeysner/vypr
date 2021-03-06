// <copyright file="RoleClaimsApiController.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Roles.Controller
{
    using Microsoft.AspNetCore.Mvc;
    using VyprCore.Foundation.BaseClasses.Controller;
    using VyprCore.Foundation.Roles.Repository;
    using VyprCore.Foundation.Roles.Strategy;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Interfaces.Strategy;
    using VyprCore.Models.Domain;
    using VyprCore.Models.Models;
    using VyprCore.Models.Resources;
    using VyprCore.Models.ViewModels;
    using VyprCore.Utilities.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Roles Claims Api controller.
    /// </summary>
    /// <seealso cref="VyprCore.Foundation.BaseClasses.Controller.BaseApiController&lt;VyprCore.Models.Domain.VyprRoleClaim, VyprCore.Models.ViewModels.VyprRoleClaimsViewModel, VyprCore.Foundation.Roles.Repository.VyprRoleClaimsRepository, VyprCore.Foundation.Roles.Strategy.RoleClaimsStrategy&gt;" />
    [Route("api/roleclaims")]
    public class VyprRoleClaimsApiController : BaseApiController<VyprRoleClaim, VyprRoleClaimsViewModel, VyprRoleClaimsRepository, RoleClaimsStrategy>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprRoleClaimsApiController"/> class.
        /// </summary>
        /// <param name="strategy">The strategy.</param>
        public VyprRoleClaimsApiController(IStrategy<VyprRoleClaim, VyprRoleClaimsViewModel, VyprRoleClaimsRepository> strategy) : base(strategy)
        {
        }

        /// <summary>
        /// Deletes the specified entity with identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Authorize(VyprClaims.RoleClaims_Administer)]
        public override Task<ActionResult<VyprRoleClaim>> Delete(int id)
        {
            return base.Delete(id);
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns></returns>
        [Authorize(VyprClaims.RoleClaims_Read)]
        public override Task<ActionResult<IEnumerable<VyprRoleClaimsViewModel>>> Get()
        {
            return base.Get();
        }

        /// <summary>
        /// Gets the specified entity with identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Authorize(VyprClaims.RoleClaims_Read)]
        public override Task<ActionResult<VyprRoleClaimsViewModel>> Get(int id)
        {
            return base.Get(id);
        }

        /// <summary>
        /// Gets the by role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("byrole/{id}/query")]
        [Authorize(VyprClaims.RoleClaims_Read)]
        public async Task<ActionResult<IQueryResult<VyprRoleClaimsViewModel>>> GetByRoleGridQuery(int id, string filter = null, string orderBy = null, int? skip = null, int? top = null)
        {
            try
            {
                var result = await _strategy.ByRole(id, filter, orderBy, skip, top);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest($"{StandardText.SomethingWentWrong}");
            }
        }

        /// <summary>
        /// Posts the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [Authorize(VyprClaims.RoleClaims_Administer)]
        public override Task<ActionResult<VyprRoleClaim>> Post(VyprRoleClaimsViewModel viewModel)
        {
            return base.Post(viewModel);
        }

        /// <summary>
        /// Puts the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [Authorize(VyprClaims.RoleClaims_Administer)]
        public override Task<IActionResult> Put(VyprRoleClaimsViewModel viewModel)
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
        [Authorize(VyprClaims.RoleClaims_Read)]
        public override Task<ActionResult<IQueryResult<VyprRoleClaimsViewModel>>> Query(string filter = null, string orderBy = null, int? skip = null, int? top = null)
        {
            return base.Query(filter, orderBy, skip, top);
        }

        /// <summary>
        /// Queries using the specified filter.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(VyprClaims.RoleClaims_Read)]
        public override Task<ActionResult<IQueryResult<VyprRoleClaimsViewModel>>> Query([FromBody] QueryViewModel<VyprRoleClaim> model)
        {
            return base.Query(model);
        }
    }
}
