// <copyright file="VyprAuthorizationPolicy.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Authorisation.Policy
{
    using Microsoft.AspNetCore.Authorization;
    using VyprCore.Foundation.Authentication.Managers;
    using VyprCore.Interfaces.Authorization;
    using VyprCore.Interfaces.Context;
    using VyprCore.Models.Domain;
    using VyprCore.Models.Models;
    using VyprCore.Utilities.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// Role Authorization Policy
    /// </summary>
    /// <seealso cref="VyprCore.Foundation.Authorisation.Attributes.AttributeAuthorizationHandler{VyprCore.Foundation.Authorisation.Policy.VyprAuthorizationRequirement, VyprCore.Foundation.Authorisation.Attributes.MustBeRoleTypeOrAbove}" />
    public class VyprAuthorizationPolicy : AttributeAuthorizationHandler<AuthorizationRequirement, Utilities.Attributes.AuthorizeAttribute>
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly VyprUserManager _userManager;

        /// <summary>
        /// The application context
        /// </summary>
        private readonly IApplicationContext<VyprUser> _applicationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprAuthorizationPolicy"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="applicationContext">The application context.</param>
        public VyprAuthorizationPolicy(VyprUserManager userManager, IApplicationContext<VyprUser> applicationContext)
        {
            _userManager = userManager;
            _applicationContext = applicationContext;
        }

        /// <summary>
        /// Handles the requirement asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="requirement">The requirement.</param>
        /// <param name="attributes">The attributes.</param>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement, IEnumerable<Utilities.Attributes.AuthorizeAttribute> attributes)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                foreach (var permissionAttribute in attributes)
                {
                    if (permissionAttribute.RoleType != null)
                    {
                        var roleType = Activator.CreateInstance(permissionAttribute.RoleType) as IRoleType;
                        if (!Authorize(context.User, roleType))
                        {
                            return Task.CompletedTask;
                        }
                    }

                    if (permissionAttribute.Claims != null && permissionAttribute.Claims.Any())
                    {
                        if (!Authorize(context.User, permissionAttribute.Claims))
                        {
                            return Task.CompletedTask;
                        }
                    }
                }

                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Authorizes the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleType">Type of the role.</param>
        /// <returns></returns>
        private bool Authorize(ClaimsPrincipal user, IRoleType roleType)
        {
            // TODO!
            return true;
        }

        /// <summary>
        /// Authorizes the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="claims">The claims.</param>
        /// <returns></returns>
        private bool Authorize(ClaimsPrincipal user, string[] claims)
        {
            // TODO!
            return true;
        }
    }
}
