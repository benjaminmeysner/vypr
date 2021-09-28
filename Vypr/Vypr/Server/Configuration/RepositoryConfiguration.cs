// <copyright file="RepositoryConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Configuration
{
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Vypr.Server.Authentication.Senders;
    using Vypr.Server.Authorisation.Strategy;
    using Vypr.Server.Data.Repository;
    using Vypr.Server.Data.Strategy;
    using VyprCore.Interfaces.Email;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Interfaces.Strategy;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Repository configurations.
    /// </summary>
    public static class RepositoryConfiguration
    {
        /// <summary>
        /// Adds the repositories used by the application.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddConfiguredRepositories(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IEmailTemplater, EmailTemplater>();

            services.AddScoped<IRepository<VyprWebAuthnCredential>, WebAuthnRepository>();
            services.AddScoped<IStrategy<VyprWebAuthnCredential, WebAuthnCredentialViewModel, WebAuthnRepository>, WebAuthnStrategy>();

            services.AddScoped<IRepository<VyprRole>, VyprRoleRepository>();
            services.AddScoped<IStrategy<VyprRole, VyprRoleViewModel, VyprRoleRepository>, VyprRoleStrategy>();

            services.AddScoped<IRepository<VyprRoleClaim>, VyprRoleClaimsRepository>();
            services.AddScoped<IStrategy<VyprRoleClaim, VyprRoleClaimsViewModel, VyprRoleClaimsRepository>, VyprRoleClaimsStrategy>();

            services.AddScoped<IRepository<VyprUserToken>, VyprUserTokenRepository>();
            services.AddScoped<IStrategy<VyprUserToken, VyprUserTokenViewModel, VyprUserTokenRepository>, VyprUserTokenStrategy>();

            services.AddScoped<IRepository<VyprUserLogin>, VyprUserLoginRepository>();
            services.AddScoped<IStrategy<VyprUserLogin, VyprUserLoginViewModel, VyprUserLoginRepository>, VyprUserLoginStrategy>();

            services.AddScoped<IRepository<VyprUserClaim>, VyprUserClaimsRepository>();
            services.AddScoped<IStrategy<VyprUserClaim, VyprUserClaimsViewModel, VyprUserClaimsRepository>, VyprUserClaimsStrategy>();

            services.AddScoped<IRepository<VyprSystemAdministrator>, VyprSystemAdministratorRepository>();
            services.AddScoped<IStrategy<VyprSystemAdministrator, VyprSystemAdministratorViewModel, VyprSystemAdministratorRepository>, VyprSystemAdministratorStrategy>();

            services.AddScoped<IRepository<VyprUser>, VyprUserRepository>();
            services.AddScoped<IStrategy<VyprUser, VyprUserViewModel, VyprUserRepository>, VyprUserStrategy>();

            return services;
        }
    }
}
