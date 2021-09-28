// <copyright file="SwaggerPolicy.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Authentication.Policy
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VyprCore.Models.Models;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Swagger Policy.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{VyprCore.Foundation.Authentication.Filters.SwaggerPolicy}" />
    /// <seealso cref="Microsoft.AspNetCore.Authorization.IAuthorizationRequirement" />
    public class SwaggerPolicy : AuthorizationHandler<SwaggerPolicy>, IAuthorizationRequirement
    {
        /// <summary>
        /// Gets or sets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        private IServiceProvider _serviceProvider { get; set; }

        public SwaggerPolicy([FromServices] IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SwaggerPolicy requirement)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.User.HasClaim(s => s.Type == "Permission" && s.Value == VyprClaims.Swagger_Administer))
            {
                await Task.Run(() => context.Succeed(requirement));
            }
        }
    }
}
