// <copyright file="TenantPolicy.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Authentication.Policy
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using VyprCore.Foundation.Authentication.Managers;
    using VyprCore.Interfaces.Context;
    using VyprCore.Models.Domain;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Tenant Policy
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{VyprCore.Foundation.Authentication.Filters.TenantPolicy}" />
    /// <seealso cref="Microsoft.AspNetCore.Authorization.IAuthorizationRequirement" />
    public class TenantPolicy : AuthorizationHandler<TenantPolicy>, IAuthorizationRequirement
    {
        /// <summary>
        /// Gets or sets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        private IServiceProvider _serviceProvider { get; set; }

        public TenantPolicy([FromServices] IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TenantPolicy requirement)
        {
            using var scope = _serviceProvider.CreateScope();
            var signInManager = scope.ServiceProvider.GetRequiredService<VyprSignInManager>();
            var applicationContext = scope.ServiceProvider.GetRequiredService<IApplicationContext<VyprUser>>();

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (await signInManager.CanSignInAsync(await applicationContext.UserAsync()))
            {
                context.Succeed(requirement);
            }

            return;
        }
    }
}
