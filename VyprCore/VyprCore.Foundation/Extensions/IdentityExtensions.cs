// <copyright file="FidoExtensions.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using VyprCore.Foundation.Identity.Fido;
    using Fido2NetLib;
    using System;
    using Microsoft.AspNetCore.Identity;
    using VyprCore.Foundation.Authentication.Stores;
    using VyprCore.Foundation.Authentication.Managers;
    using VyprCore.Foundation.Context;
    using VyprCore.Foundation.Authentication.Factory;
    using Microsoft.AspNetCore.Authorization;
    using VyprCore.Models.Domain;
    using VyprCore.Foundation.Authorisation.Policy;
    using VyprCore.Foundation.Authentication.Policy;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using VyprCore.Models;

    /// <summary>
    /// Extensions used in conjuction with FidoIdentity/WebAuthn stragies & services.
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Adds custom vypr core identity services to the application.
        /// And adds the expected, general setup to go with it. The default
        /// on password security if not provided is generally low, so it is 
        /// advised that you override these and also support them with frontend validation.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, Action<IdentityOptions> config = null, bool addSwaggerPolicy = false, bool defaultWebAuthLogin = false)
        {
            var identityConfig = new IdentityOptions();
            if (!(config is null))
            {
                config(identityConfig);
            }

            services.AddIdentity<VyprUser, VyprRole>(options =>
                {
                    options.Password.RequireDigit = config != null && identityConfig.Password.RequireDigit;
                    options.Password.RequiredLength = config != null ? 6 : identityConfig.Password.RequiredLength;
                    options.Password.RequireLowercase = config != null && identityConfig.Password.RequireLowercase;
                    options.Password.RequireNonAlphanumeric = config != null && identityConfig.Password.RequireNonAlphanumeric;
                    options.Password.RequireUppercase = config != null && identityConfig.Password.RequireUppercase;
                })
                .AddDefaultTokenProviders()
                .AddUserStore<VyprUserStore>()
                .AddRoleStore<VyprRoleStore>()
                .AddUserManager<VyprUserManager>()
                .AddRoleManager<VyprRoleManager>()
                .AddSignInManager<VyprSignInManager>()
                .AddEntityFrameworkStores<VyprDbContext>()
                .AddClaimsPrincipalFactory<VyprUserClaimsPrincipleFactory<VyprUser, VyprRole>>()
                .AddEntityFrameworkStores<VyprDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString(defaultWebAuthLogin ? "/login?U_WebAuthn=true" : "/login");
                options.AccessDeniedPath = new PathString("/error/403");
            });

            services.AddTransient<IAuthorizationHandler, VyprAuthorizationPolicy>();

            services.AddAuthorization((options, serviceProvider) =>
            {
                options.AddPolicy("TenantPolicy", policy => policy.Requirements.Add(new TenantPolicy(serviceProvider)));
                options.AddPolicy("Permission", policyBuilder => { policyBuilder.Requirements.Add(new VyprCore.Utilities.Attributes.AuthorizationRequirement()); });

                if (addSwaggerPolicy)
                {
                    options.AddPolicy("SwaggerPolicy", policy => policy.Requirements.Add(new SwaggerPolicy(serviceProvider)));
                }
            });

            return services;
        }

        /// <summary>
        /// Adds the authorization.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configure">The configure.</param>
        /// <returns></returns>
        public static IServiceCollection AddAuthorization(this IServiceCollection services, Action<AuthorizationOptions, IServiceProvider> configure)
        {
            services.AddOptions<AuthorizationOptions>().Configure<IServiceProvider>(configure);
            return services.AddAuthorization();
        }

        /// <summary>
        /// Adds web authentication protocols to the server container.
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="appConfig"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebAuthn<TDbContext>(this IServiceCollection services, IConfiguration appConfig)
            where TDbContext : DbContext
        {
            var webAuthConfig = appConfig.GetSection("WebAuthn");
            services.AddSession().Configure<WebAuthnConfiguration>(webAuthConfig);
            if (webAuthConfig is null)
            {
                throw new InvalidOperationException("Application has not been configured to use WebAuthn. Please make sure the app settings for this is present.");
            }

            return AddWebAuthn<TDbContext>(services, config =>
            {
                config.Origin = webAuthConfig["Origin"];
                config.ServerName = webAuthConfig["ServerName"];
                config.ServerDomain = webAuthConfig["ServerDomain"];
            });
        }

        /// <summary>
        /// Internally configures the web authentication protocols further.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        private static IServiceCollection AddWebAuthn<TDbContext>(this IServiceCollection services, Action<WebAuthnConfiguration> config = null)
            where TDbContext : DbContext
        {
            // Setup config for Fido2, applies defaults if the config has not been 
            // provided or a property has not been provided. Please see
            // https://github.com/abergs/fido2-net-lib for more info on implementation.

            var fidoConfig = new WebAuthnConfiguration();
            config(fidoConfig);

            var fido = new WebAuthn(new Fido2Configuration()
            {
                ServerDomain = fidoConfig.ServerDomain ?? "localhost",
                ServerName = fidoConfig.ServerName ?? "WebAuthn",
                Origin = fidoConfig.Origin ?? "https://localhost:44326",
                TimestampDriftTolerance = fidoConfig.TimeStampDriftTolerance ?? 300000
            });

            // The following lines add the internal Fido config, repo and strategy to the service container,
            // Allowing it to be injected in, anywhere within the child application.
            // The db context is a passed in type which should be resolved by DI by the using application.
            services.AddSingleton(fido);
            return services;
        }

        /// <summary>
        /// Use WebAuthentication protocols.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebAuthn(this IApplicationBuilder app)
        {
            return app.UseSession();
        }
    }
}
