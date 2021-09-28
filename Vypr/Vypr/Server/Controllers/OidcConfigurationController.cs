// <copyright file="OidcConfigurationController.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Controllers
{
    using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Open Id Controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public sealed class OidcConfigurationController : Controller
    {
        /// <summary>Initializes a new instance of the <see cref="OidcConfigurationController" /> class.</summary>
        /// <param name="clientRequestParametersProvider">The client request parameters provider.</param>
        public OidcConfigurationController(IClientRequestParametersProvider clientRequestParametersProvider)
        {
            ClientRequestParametersProvider = clientRequestParametersProvider;
        }

        /// <summary>
        /// Gets the client request parameters provider.
        /// </summary>
        /// <value>
        /// The client request parameters provider.
        /// </value>
        public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

        /// <summary>
        /// Gets the client request parameters.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("_configuration/{clientId}")]
        //// [AllowAnonymous]
        public IActionResult GetClientRequestParameters([FromRoute] string clientId)
        {
            var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
            return Ok(parameters);
        }
    }
}
