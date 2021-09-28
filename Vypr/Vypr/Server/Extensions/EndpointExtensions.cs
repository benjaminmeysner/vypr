// <copyright file="EndpointExtensions.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Extensions
{
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Vypr.Server.Controllers;
    using Vypr.Server.Controllers.Api;

    /// <summary>
    /// Extensions for endpoints.
    /// </summary>
    public static class EndpointExtensions
    {
        /// <summary>
        /// Adds the foundation routes.
        /// </summary>
        /// <param name="IEndpointRouteBuilder">The i endpoint route builder.</param>
        /// <returns></returns>
        public static void AddFoundationRoutes(this IEndpointRouteBuilder endpoint)
        {
            // very important to tag .RequireAuthorization("TenantPolicy") on any routes

            endpoint.MapControllerRoute(
               name: "accountmanagement",
               pattern: "account/manage/{action}/{Id?}",
               // This presumes the app has a controller called AccountManageController 
               // which inherits BaseAccountManageController. It doesn't seem to pickup
               // routes on the inheritted controllers.
               defaults: new { controller = "AccountManage" })
               .RequireAuthorization("TenantPolicy");

            endpoint.MapControllerRoute(
               name: "api",
               pattern: "api/{controller}/{action}/{Id?}")
               .RequireAuthorization("TenantPolicy");
        }

        /// <summary>
        /// Adds the foundation controllers.
        /// </summary>
        /// <param name="service">The service.</param>
        public static void AddFoundationControllers(this IServiceCollection service)
        {
            service.AddControllers()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest)
                .AddApplicationPart(typeof(AccountController).Assembly)
                .AddApplicationPart(typeof(AccountManageController).Assembly)
                .AddApplicationPart(typeof(AdminManageController).Assembly)
                .AddApplicationPart(typeof(VyprConfigurationApiController).Assembly)
                .AddApplicationPart(typeof(VyprRoleApiController).Assembly)
                .AddApplicationPart(typeof(VyprRoleClaimApiController).Assembly)
                .AddApplicationPart(typeof(UtilitiesController).Assembly)
                .AddApplicationPart(typeof(WebAuthnApiController).Assembly);
        }
    }
}
