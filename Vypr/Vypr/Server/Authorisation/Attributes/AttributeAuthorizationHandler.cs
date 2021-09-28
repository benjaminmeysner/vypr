// <copyright file="AttributeAuthorizationHandler.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Attributes
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Attribute Authorization Handler
    /// </summary>
    /// <typeparam name="TRequirement">The type of the requirement.</typeparam>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{TRequirement}" />
    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute> : AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement where TAttribute : Attribute
    {
        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            var attributes = new List<TAttribute>();

            if (context.Resource is HttpContext httpContext)
            {
                var endPoint = httpContext.GetEndpoint();

                var action = endPoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

                if (action != null)
                {
                    attributes.AddRange(GetAttributes(action.ControllerTypeInfo.UnderlyingSystemType));
                    attributes.AddRange(GetAttributes(action.MethodInfo));
                }
            }

            return HandleRequirementAsync(context, requirement, attributes);
        }

        /// <summary>
        /// Handles the requirement asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="requirement">The requirement.</param>
        /// <param name="attributes">The attributes.</param>
        /// <returns></returns>
        protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement, IEnumerable<TAttribute> attributes);

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns></returns>
        private static IEnumerable<TAttribute> GetAttributes(MemberInfo memberInfo) => memberInfo.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
    }
}
