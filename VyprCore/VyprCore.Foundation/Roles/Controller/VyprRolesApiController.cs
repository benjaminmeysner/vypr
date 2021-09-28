// <copyright file="RolesApiController.cs" company="Vypr Systems">
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
    using VyprCore.Models.ViewModels;
    using VyprCore.Utilities.Attributes;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Roles api controller.
    /// </summary>
    /// <seealso cref="VyprCore.Foundation.BaseClasses.Controller.BaseApiController{VyprCore.Models.Domain.VyprRole, VyprCore.Models.ViewModels.VyprRoleViewModel, VyprCore.Foundation.Roles.Repository.VyprRoleRepository, VyprCore.Foundation.Roles.Strategy.RoleStrategy}" />
    [Route("api/roles")]
    public class VyprRolesApiController : BaseApiController<VyprRole, VyprRoleViewModel, VyprRoleRepository, RoleStrategy>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantController"/> class.
        /// </summary>
        /// <param name="strategy">The strategy.</param>
        public VyprRolesApiController(IStrategy<VyprRole, VyprRoleViewModel, VyprRoleRepository> strategy) : base(strategy)
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
