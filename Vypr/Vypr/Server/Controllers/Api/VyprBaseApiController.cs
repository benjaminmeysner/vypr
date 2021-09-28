// <copyright file="BaseApiController.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VyprCore.Interfaces.Strategy;
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Repository;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using System;
    using VyprCore.Utilities.Exceptions;
    using VyprCore.Models.Resources;
    using Microsoft.AspNetCore.Authorization;
    using VyprCore.Models.ViewModels;
    using Remote.Linq;

    /// <summary>
    /// Base API Controller.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    /// <typeparam name="TStrategy">The type of the strategy.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Authorize]
    public abstract class VyprBaseApiController<TEntity, TEntityViewModel, TRepository, TStrategy> : ControllerBase
        where TStrategy : IStrategy<TEntity, TEntityViewModel, TRepository>
        where TEntity : class, IEntity, new()
        where TEntityViewModel : class, IEntityViewModel, new()
        where TRepository : IRepository<TEntity>
    {
        /// <summary>
        /// The strategy.
        /// </summary>
        protected readonly TStrategy _strategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprBaseApiController{TEntity, TEntityViewModel, TRepository, TStrategy}"/> class.
        /// </summary>
        /// <param name="strategy">The strategy.</param>
        public VyprBaseApiController(IStrategy<TEntity, TEntityViewModel, TRepository> strategy)
        {
            _strategy = (TStrategy)strategy;
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public virtual async Task<ActionResult<IEnumerable<TEntityViewModel>>> Get()
        {
            try
            {
                var viewModels = await _strategy.GetAll();
                if (viewModels == null)
                {
                    return NotFound();
                }
                return Ok(viewModels);

            }
            catch
            {
                return BadRequest($"{StandardText.SomethingWentWrong}");
            }
        }

        /// <summary>
        /// Gets the specified entity with identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntityViewModel>> Get(int id)
        {
            try
            {
                var viewModel = await _strategy.Get(id);
                if (viewModel == null)
                {
                    return NotFound();
                }

                return viewModel;
            }
            catch
            {
                return BadRequest($"{StandardText.SomethingWentWrong}");
            }
        }

        /// <summary>
        /// Puts the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPut()]
        public virtual async Task<IActionResult> Put(TEntityViewModel viewModel)
        {
            ThrowIfNull(viewModel);

            try
            {
                return Ok(await _strategy.Update(viewModel));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Posts the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Post(TEntityViewModel viewModel)
        {
            ThrowIfNull(viewModel);

            try
            {
                var response = await _strategy.Add(viewModel);
                return Ok(response);
            }
            catch (ResourceConflictException)
            {
                return new StatusCodeResult(409);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the specified entity with identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TEntity>> Delete(int id)
        {
            try
            {
                await _strategy.Delete(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Queries using the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        [HttpGet("query")]
        public virtual async Task<ActionResult<IQueryResult<TEntityViewModel>>> Query(string filter = null, string orderBy = null, int? skip = null, int? top = null)
        {
            try
            {
                var result = await _strategy.Query(filter, orderBy, skip, top);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Queries using the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        [HttpPost("query")]
        public virtual async Task<ActionResult<IQueryResult<TEntityViewModel>>> Query([FromBody] QueryViewModel<TEntity> model)
        {
            try
            {
                var result = await _strategy.Query(
                    model.Filter?.ToLinqExpression<TEntity, bool>(),
                    model.OrderBy?.ToLinqExpression<TEntity, object>(), model.Skip, model.Take);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Counts all entities, -1 if error.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        [HttpGet("count")]
        public virtual async Task<ActionResult<int>> Count()
        {
            try
            {
                return Ok(await _strategy.Count());
            }
            catch
            {
                return Ok(-1);
            }
        }

        /// <summary>
        /// Throws if null.
        /// </summary>
        /// <param name="objects">The objects.</param>
        /// <exception cref="Utilities.Exceptions.NullModelException">objects</exception>
        protected static void ThrowIfNull(params object[] objects)
        {
            if (objects.Any(x => x is null))
            {
                throw new NullModelException(nameof(objects));
            }
        }

        /// <summary>
        /// Yeses the no identity response.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected IActionResult YesNoIdentityResponse(IdentityResult result, object model)
        {
            if (result.Succeeded)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest(model);
            }
        }
    }
}
