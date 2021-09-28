// <copyright file="Program.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Client
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using VyprCore.Client.Authorization;
    using VyprCore.Client.Extensions;
    using Vypr.Client.Configuration;
    using VyprCore.RazorComponents.Helpers;

    /// <summary>
    /// Program Test.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A New Task.</returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#vypr_app");

            builder.AddClientConfiguration();
            builder.AddClientFrameworkConfiguration();
            builder.AddFrameworkRazorComponentServices();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
