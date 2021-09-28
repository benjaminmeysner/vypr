// <copyright file="VyprClientClaimsPrincipalFactory.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Authorization
{
    using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
    using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
    using VyprCore.Client.Api;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// Client Claims Principal Factory
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.WebAssembly.Authentication.AccountClaimsPrincipalFactory{Microsoft.AspNetCore.Components.WebAssembly.Authentication.RemoteUserAccount}" />
    public class VyprClientClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprClientClaimsPrincipalFactory"/> class.
        /// </summary>
        /// <param name="accountClient">The account client.</param>
        /// <param name="tokenProvider">The token provider.</param>
        public VyprClientClaimsPrincipalFactory(AccountApi accountClient, IAccessTokenProviderAccessor tokenProvider) : base(tokenProvider)
        {
            AccountClient = accountClient;
        }

        /// <summary>
        /// Gets the account client.
        /// </summary>
        /// <value>
        /// The account client.
        /// </value>
        private AccountApi AccountClient { get; }

        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="userAccount">The user account.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount userAccount, RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(userAccount, options);

            var claims = await AccountClient.GetUserClaimsAsync();

            ((ClaimsIdentity)user.Identity).AddClaims(claims.Select(c => new Claim(c.Key, c.Value)));

            return user;
        }
    }
}
