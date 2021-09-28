// <copyright file="SwaggerConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Configuration
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Swagger configurations.
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Adds the SwaggerConfiguration Configuration used by the application.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "EPOD Api",
                        Version = "v1"
                    });
                c.CustomSchemaIds(i => i.FullName);
            });
            return services;
        }

        /// <summary>
        /// Uses the swagger configuration.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>Application builder.</returns>
        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EPOD");
            });

            return app;
        }
    }
}
