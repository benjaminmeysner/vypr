// <copyright file="VyprUserClaimsPrincipleFactory.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Authentication.Factory
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Vypr.Server.Authentication.Managers;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using VyprCore.Models.Domain;
    using VyprCore.Interfaces.Context;

    /// <summary>
    /// Provides methods to create a claims principal for a given user.
    /// </summary>
    /// <typeparam name="TUser">The type used to represent a user.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IUserClaimsPrincipalFactory{TUser}" />
    public class BaseUserClaimsPrincipalFactory<TUser> : IUserClaimsPrincipalFactory<TUser>
        where TUser : VyprUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimsPrincipalFactory{TUser}" /> class.
        /// </summary>
        /// <param name="userManager">The <see cref="UserManager{TUser}" /> to retrieve user information from.</param>
        /// <param name="optionsAccessor">The configured <see cref="IdentityOptions" />.</param>
        /// <exception cref="ArgumentNullException">userManager
        /// or
        /// optionsAccessor</exception>
        public BaseUserClaimsPrincipalFactory(
            VyprUserManager userManager,
            IOptions<IdentityOptions> optionsAccessor)
        {
            if (optionsAccessor == null || optionsAccessor.Value == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            Options = optionsAccessor.Value;
        }

        /// <summary>
        /// Gets the <see cref="UserManager{TUser}" /> for this factory.
        /// </summary>
        /// <value>
        /// The current <see cref="UserManager{TUser}" /> for this factory instance.
        /// </value>
        public VyprUserManager UserManager { get; private set; }

        /// <summary>
        /// Gets the <see cref="IdentityOptions"/> for this factory.
        /// </summary>
        /// <value>
        /// The current <see cref="IdentityOptions"/> for this factory instance.
        /// </value>
        public IdentityOptions Options { get; private set; }

        /// <summary>
        /// Creates a <see cref="ClaimsPrincipal"/> from an user asynchronously.
        /// </summary>
        /// <param name="user">The user to create a <see cref="ClaimsPrincipal"/> from.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous creation operation, containing the created <see cref="ClaimsPrincipal"/>.</returns>
        public virtual async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var id = await GenerateClaimsAsync(user);
            return new ClaimsPrincipal(id);
        }

        /// <summary>
        /// Generate the claims for a user.
        /// </summary>
        /// <param name="user">The user to create a <see cref="ClaimsIdentity"/> from.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous creation operation, containing the created <see cref="ClaimsIdentity"/>.</returns>
        protected virtual async Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
        {
            var userId = await UserManager.GetUserIdAsync(user);
            var userName = await UserManager.GetUserNameAsync(user);
            var id = new ClaimsIdentity("Identity.Application", // REVIEW: Used to match Application scheme
                Options.ClaimsIdentity.UserNameClaimType,
                Options.ClaimsIdentity.RoleClaimType);
            id.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, userId));
            id.AddClaim(new Claim(Options.ClaimsIdentity.UserNameClaimType, userName));
            if (UserManager.SupportsUserSecurityStamp)
            {
                id.AddClaim(new Claim(Options.ClaimsIdentity.SecurityStampClaimType,
                    await UserManager.GetSecurityStampAsync(user)));
            }
            if (UserManager.SupportsUserClaim)
            {
                id.AddClaims(await UserManager.GetClaimsAsync(user));
            }

            return id;
        }
    }

    /// <summary>
    /// Provides methods to create a claims principal for a given user.
    /// </summary>
    /// <typeparam name="TUser">The type used to represent a user.</typeparam>
    /// <typeparam name="TRole">The type used to represent a role.</typeparam>
    /// <seealso cref="Vypr.Server.Authentication.Factory.BaseUserClaimsPrincipalFactory{TUser}" />
    public class VyprUserClaimsPrincipleFactory<TUser, TRole> : BaseUserClaimsPrincipalFactory<TUser>
        where TUser : VyprUser
        where TRole : VyprRole
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimsPrincipalFactory{TUser, TRole}" /> class.
        /// </summary>
        /// <param name="userManager">The <see cref="UserManager{TUser}" /> to retrieve user information from.</param>
        /// <param name="roleManager">The <see cref="RoleManager{TRole}" /> to retrieve a user's roles from.</param>
        /// <param name="options">The configured <see cref="IdentityOptions" />.</param>
        /// <param name="applicationContext">The application context.</param>
        /// <exception cref="ArgumentNullException">roleManager</exception>
        public VyprUserClaimsPrincipleFactory(VyprUserManager userManager, VyprRoleManager roleManager, IOptions<IdentityOptions> options, IApplicationContext<VyprUser> applicationContext)
            : base(userManager, options)
        {
            RoleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            ApplicationContext = applicationContext;
        }

        /// <summary>
        /// Gets the <see cref="RoleManager{TRole}" /> for this factory.
        /// </summary>
        /// <value>
        /// The current <see cref="RoleManager{TRole}" /> for this factory instance.
        /// </value>
        public VyprRoleManager RoleManager { get; private set; }

        /// <summary>
        /// Gets the application context.
        /// </summary>
        /// <value>
        /// The application context.
        /// </value>
        public IApplicationContext<VyprUser> ApplicationContext { get; private set; }

        /// <summary>
        /// Generate the claims for a user.
        /// </summary>
        /// <param name="user">The user to create a <see cref="ClaimsIdentity"/> from.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous creation operation, containing the created <see cref="ClaimsIdentity"/>.</returns>
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
        {
            var id = await base.GenerateClaimsAsync(user);
            if (UserManager.SupportsUserRole)
            {
                var roles = await UserManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    id.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));
                    if (RoleManager.SupportsRoleClaims)
                    {
                        var role = await RoleManager.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            id.AddClaims(await RoleManager.GetClaimsAsync(role));
                        }
                    }
                }
            }
            return id;
        }
    }
}
