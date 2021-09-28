// <copyright file="ClientExtensions.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Extensions
{
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using VyprCore.Client.Api;
    using VyprCore.Client.Context;
    using VyprCore.Client.Navigation;
    using VyprCore.Interfaces.Client;
    using VyprCore.Interfaces.Context;
    using VyprCore.Models.Models;
    using VyprCore.Utilities.Converters;
    using System;

    /// <summary>
    /// Extensions for client projects.
    /// </summary>
    public static class ClientExtensions
    {
        /// <summary>
        /// Adds core application context to the project. Configurable with options.
        /// </summary>
        /// <param name="builder">The WebAssemblyHostBuilder.</param>
        /// <returns></returns>
        public static void AddClientFrameworkConfiguration(this WebAssemblyHostBuilder builder)
        {
            // Logging all user pages won't happen since this service is registered on the client machine.
            // The assumption here is that this is Blazor WASM.
            builder.Services.AddSingleton<IPageState, PageState>();
            builder.Services.AddHttpClient<AccountManageApi>(x => x.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<AccountApi>(x => x.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddHttpClient<WebAuthnApi>(x => x.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddHttpClient<ImageUtilitiesApi>(x => x.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<AdminManageApi>(x => x.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<IEntityApi, EntityApi>(x => x.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddScoped<IClientApplicationContext, ClientApplicationContext>();
            builder.Services.AddBlazoredLocalStorage(config =>
            {
                config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                config.JsonSerializerOptions.Converters.Add(new WebAuthnByteArrayConverter());
            });
        }

        /// <summary>
        /// Adds the permission claims as policies.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void AddPermissionClaimsAsPolicies(this AuthorizationOptions config)
        {
            var claims = VyprClaims.GetAll();
            foreach (var claim in claims)
            {
                var claimName = (string)claim.GetRawConstantValue();
                config.AddPolicy(claimName, policy => policy.RequireClaim("Permission", claimName));
            }
        }
    }
}
