// <copyright file="AutomapperExtensions.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Extensions
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Automapper Extension methods.
    /// </summary>
    public static class AutomapperExtensions
    {
        /// <summary>Gets the automatic mapper profiles from all assemblies.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IEnumerable<Type> GetAutoMapperProfilesFromAllAssemblies()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(Profile)))
                    {
                        yield return type;
                    }
                }
            }
        }
    }
}
