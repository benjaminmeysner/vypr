// <copyright file="EndpointConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Configuration
{
    using Microsoft.AspNetCore.Builder;
    using Vypr.Server.Authentication.Policy;
    using Vypr.Server.Extensions;

    /// <summary>
    /// Endpoint configurations.
    /// </summary>
    public static class EndpointConfiguration
    {
        /// <summary>
        /// Uses the swagger configuration.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>Application builder.</returns>
        public static IApplicationBuilder UseEndpointConfiguration(this IApplicationBuilder app)
        {
            // very important to tag .RequireAuthorization("TenantPolicy") on any routes
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{Id?}");

                // Tie down swagger access to system admins / tenant admins only.
                var pipeline = endpoints.CreateApplicationBuilder().Build();
                endpoints.Map("/swagger", pipeline).RequireAuthorization(nameof(SwaggerPolicy));
                endpoints.Map("/swagger/index.html", pipeline).RequireAuthorization(nameof(SwaggerPolicy));
                endpoints.Map("/swagger/v1/swagger.json", pipeline).RequireAuthorization(nameof(SwaggerPolicy));
                endpoints.Map("/swagger/{documentName}/swagger.json", pipeline).RequireAuthorization(nameof(SwaggerPolicy));

                endpoints.AddFoundationRoutes();
                endpoints.MapFallbackToFile("index.html");
            });

            return app;
        }
    }
}
