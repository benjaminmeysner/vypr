// <copyright file="ConfigurationApiController.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Miscellaneous.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using VyprCore.Foundation.Settings;
    using VyprCore.Models.Resources;
    using VyprCore.Utilities.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Controller that handles Configuration.</summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/configuration")]
    public class ConfigurationApiController : ControllerBase
    {
        /// <summary>
        /// Gets or sets the application configuration.
        /// </summary>
        /// <value>
        /// The application configuration.
        /// </value>
        private ApplicationConfig ApplicationConfig { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TripApiController"/> class.
        /// </summary>
        /// <param name="counterActService">The _counterActService.</param>
        public ConfigurationApiController(IOptions<ApplicationConfig> options)
        {
            ApplicationConfig = options?.Value;
        }


        /// <summary>
        /// Gets the environment name.
        /// </summary>
        /// <returns>The environment name as a <c>string</c></returns>
        [HttpGet("environment")]
        public ActionResult<string> GetEnvironmentName()
        {
            try
            {
                if (ApplicationConfig == null)
                {
                    return Ok(string.Empty);
                }

                return Ok(ApplicationConfig.Environment);
            }
            catch (Exception ex)
            {
                return BadRequest($"{StandardText.SomethingWentWrong}. {ex.Message}.");
            }
        }

        /// <summary>
        /// Gets whether to display the development banner.
        /// </summary>
        /// <returns><c>true</c> when Environment is not 'Live'.</returns>
        [HttpGet("banner")]
        public ActionResult<string> DisplayBanner()
        {
            try
            {
                if (ApplicationConfig == null)
                {
                    return Ok(false);
                }

                return Ok(ApplicationConfig.Banner);
            }
            catch (Exception ex)
            {
                return BadRequest($"{StandardText.SomethingWentWrong}. {ex.Message}.");
            }
        }
    }
}
