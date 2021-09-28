// <copyright file="ContextExtensions.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Vypr.Server.Classes.Context;
    using System;
    using VyprCore.Interfaces.Context;
    using VyprCore.Models.Domain;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Extensions for project contexts.
    /// </summary>
    public static class ContextExtensions
    {
        /// <summary>
        /// Adds core application context to the project. Configurable with options.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationContext(this IServiceCollection services)
        {
            return services.AddScoped<IApplicationContext<VyprUser>, ApplicationContext>();
        }

        /// <summary>
        /// Seeds the system administrator.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static ModelBuilder SeedSystemAdministrator(this ModelBuilder builder)
        {
            builder.Entity<VyprSystemAdministrator>().HasData(
                new VyprSystemAdministrator
                {
                    UserId = 1
                });

            builder.Entity<VyprUser>().HasData(
                new VyprUser()
                {
                    Id = 1,
                    Email = "admin@vyprsystems.com",
                    FirstName = "Vypr",
                    LastName = "Administrator",
                    EmailConfirmed = true,
                    RegistrationToken = "ABCDEFG_SEEDED",
                    RegistrationTokenExpiry = DateTime.Now,
                    UserName = "admin@vyprsystems.com",
                    NormalizedEmail = "ADMIN@VYPRSYSTEMS.COM",
                    NormalizedUserName = "ADMIN@VYPRSYSTEMS.COM",
                    PhoneNumberConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEBZJdEJrVf8TCzaSyi6JBcohyrbf+dlfSgJJ/5RxgkqVhoKO7Qaoi2VgJyPEdrOPhw==",
                    SecurityStamp = "VQVNOJJQ775HRO5JUUNFXQWVSFMYGD5C",
                    ConcurrencyStamp = "3845376e-a5bc-4fd8-b00e-06e7013ba295",
                    LockoutEnabled = false,
                    Active = true
                });
            return builder;
        }
    }
}
