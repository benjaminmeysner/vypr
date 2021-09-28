// <copyright file="ClientConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Client.Configuration
{
    using System;
    using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Vypr.Client.Api;

    /// <summary>
    /// Client configuration.
    /// </summary>
    public static class ClientConfiguration
    {
        /// <summary>
        /// Adds client application config to the project.
        /// </summary>
        /// <param name="builder">The WebAssemblyHostBuilder.</param>
        public static void AddClientConfiguration(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddHttpClient<UserApi>(x => x.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<ConfigurationApi>(x => x.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
        }
    }
}
