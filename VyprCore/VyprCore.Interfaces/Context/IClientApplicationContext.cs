// <copyright file="IClientApplicationContext.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Context
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// Client Application Context
    /// </summary>
    public interface IClientApplicationContext
    {
        /// <summary>
        /// Determines whether the specified user has the specified claims.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="allRequired">if set to <c>true</c> [all required].</param>
        /// <param name="claimType">Type of the claim.</param>
        /// <param name="claims">The claims.</param>
        /// <returns><c>true</c> if the user has all the claims (if allRequired is <c>true</c>) or one of the claims (if allRequired is <c>false</c>)</returns>
        public Task<bool> HasClaims(ClaimsPrincipal user = null, bool allRequired = true, string claimType = "Permission", params string[] claims);

        /// <summary>
        /// Determines whether the specified user has roles.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="allRequired">if set to <c>true</c> [all required].</param>
        /// <param name="roles">The roles.</param>
        /// <returns><c>true</c> if the user has all the roles (if allRequired is <c>true</c>) or one of the roles (if allRequired is <c>false</c>)</returns>
        public Task<bool> HasRoles(ClaimsPrincipal user = null, bool allRequired = true, params string[] roles);
    }
}
