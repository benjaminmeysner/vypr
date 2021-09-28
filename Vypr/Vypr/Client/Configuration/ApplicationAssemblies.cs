// <copyright file="ApplicationAssemblies.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Client.Configuration
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Application Assemblies.
    /// </summary>
    public static class ApplicationAssemblies
    {
        /// <summary>
        /// Gets all the configured assemblies which are used in the application.
        /// </summary>
        /// <returns>List of all assemblies used in the application.</returns>
        public static IEnumerable<Assembly> GetReferencedAssemblies()
        {
            foreach (var assemblyName in typeof(Program).Assembly.GetReferencedAssemblies())
            {
                yield return Assembly.Load(assemblyName);
            }
        }
    }
}
