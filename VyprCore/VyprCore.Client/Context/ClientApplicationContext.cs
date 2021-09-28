// <copyright file="ClientApplicationContext.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Context
{
    using Microsoft.AspNetCore.Components.Authorization;
    using VyprCore.Interfaces.Context;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// Client Application Context
    /// </summary>
    /// <seealso cref="VyprCore.Interfaces.Context.IClientApplicationContext" />
    public class ClientApplicationContext : IClientApplicationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientApplicationContext"/> class.
        /// </summary>
        /// <param name="authStateProvider">The authentication state provider.</param>
        public ClientApplicationContext(AuthenticationStateProvider authStateProvider)
        {
            AuthenticationStateProvider = authStateProvider;
        }

        /// <summary>
        /// Gets the authentication state provider.
        /// </summary>
        /// <value>
        /// The authentication state provider.
        /// </value>
        private AuthenticationStateProvider AuthenticationStateProvider { get; }

        /// <summary>
        /// Determines whether the specified user has the specified claims.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="allRequired">if set to <c>true</c> [all required].</param>
        /// <param name="claimType">Type of the claim.</param>
        /// <param name="claims">The claims.</param>
        /// <returns>
        ///   <c>true</c> if the user has all the claims (if allRequired is <c>true</c>) or one of the claims (if allRequired is <c>false</c>)
        /// </returns>
        public async Task<bool> HasClaims(ClaimsPrincipal user = null, bool allRequired = true, string claimType = "Permission", params string[] claims)
        {
            if (user == null)
            {
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                user = authState.User;
            }

            if (!claims?.Any() ?? true)
            {
                return true;
            }

            var found = true;
            foreach (var claim in claims)
            {
                found = user.HasClaim(claimType, claim);

                if (!found && allRequired)
                    return false;

                if (found && !allRequired)
                    break;
            }

            return found;
        }

        /// <summary>
        /// Determines whether the specified user has roles.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="allRequired">if set to <c>true</c> [all required].</param>
        /// <param name="roles">The roles.</param>
        /// <returns>
        ///   <c>true</c> if the user has all the roles (if allRequired is <c>true</c>) or one of the roles (if allRequired is <c>false</c>)
        /// </returns>
        public async Task<bool> HasRoles(ClaimsPrincipal user = null, bool allRequired = true, params string[] roles)
        {
            if (user == null)
            {
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                user = authState.User;
            }

            if (!roles?.Any() ?? true)
            {
                return true;
            }

            var found = true;
            foreach (var roleValue in roles)
            {
                found = user.HasClaim(x => x.Type == "role" && x.Value == roleValue);

                if (!found && allRequired)
                    return false;

                if (found && !allRequired)
                    break;
            }

            return found;
        }
    }
}
