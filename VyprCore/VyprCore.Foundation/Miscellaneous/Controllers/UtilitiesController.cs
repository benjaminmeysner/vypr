// <copyright file="ImageUtilitiesController.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Miscellaneous.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using VyprCore.Models.Resources;
    using VyprCore.Models.ViewModels;
    using VyprCore.Utilities.Helpers;

    /// <summary>
    /// Utilities -> image.
    /// </summary>
    [Route("utilities")]
    public class UtilitiesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="UtilitiesController"/> class.
        /// </summary>
        public UtilitiesController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// Image mutation.
        /// </summary>
        /// <param name="viewModel">The viewmodel.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost("image/mutate")]
        public async Task<ActionResult<string>> ImageMutate([FromBody] ImageMutateViewModel viewModel)
        {
            try
            {
                var data = await ImageHelpers.CompressAndResizeImageAsync(viewModel.Image, viewModel.Quality, viewModel.Width, viewModel.Height);
                return Ok(Convert.ToBase64String(data));
            }
            catch
            {
                return BadRequest(StandardText.SomethingWentWrong);
            }
        }
    }
}
