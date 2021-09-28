// <copyright file="ApplicationUserExtensions.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Vypr.Server.Extensions;
    using VyprCore.Interfaces.Authorization;
    using VyprCore.Models.Domain;

    /// <summary>
    /// Application User Extensions.
    /// </summary>
    public static class ApplicationUserExtensions
    {
        /// <summary>
        /// Gets the inherent role types.
        /// </summary>
        /// <param name="epodUser">The user.</param>
        /// <returns>List of role types specific to the user.</returns>
        public static List<IRoleType> GetInherentRoleTypes(this VyprUser vyprUser)
        {
            List<IRoleType> roleTypes = new();

            if (vyprUser.IsSystemAdministrator())
            {
                roleTypes.Add(vyprUser.SystemAdministrator);
            }

            return roleTypes.OrderBy(s => s.Rank).ToList();
        }
    }
}
