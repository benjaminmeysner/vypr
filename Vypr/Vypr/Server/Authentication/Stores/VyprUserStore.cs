// <copyright file="Startup.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Authentication.Stores
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using VyprCore.Models.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using Vypr.Server.Data;

    /// <summary>
    /// Vypr User Store
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.UserStore{Vypr.Server.Authentication.Classes.VyprUser, Vypr.Server.Authentication.Classes.VyprRole, Vypr.Server.DbContext.VyprDbContext, System.Int32, Vypr.Server.Authentication.Classes.VyprUserClaims, Vypr.Server.Authentication.Classes.VyprUserTenantRoles, Vypr.Server.Authentication.Classes.VyprUserLogins, Vypr.Server.Authentication.Classes.VyprUserTokens, Vypr.Server.Authentication.Classes.VyprRoleClaims}" />
    public class VyprUserStore : UserStore<VyprUser, VyprRole, VyprDbContext, int, VyprUserClaim, VyprUserRole, VyprUserLogin, VyprUserToken, VyprRoleClaim>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprUserStore"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public VyprUserStore(VyprDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Gets the database context for this store.
        /// </summary>
        public override VyprDbContext Context => base.Context;

        /// <summary>
        /// A navigation property for the users the store contains.
        /// </summary>
        public override IQueryable<VyprUser> Users => base.Users;

        /// <summary>
        /// Adds the <paramref name="claims" /> given to the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to add the claim to.</param>
        /// <param name="claims">The claim to add to the user.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task AddClaimsAsync(VyprUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            return base.AddClaimsAsync(user, claims, cancellationToken);
        }

        /// <summary>
        /// Adds the <paramref name="login" /> given to the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to add the login to.</param>
        /// <param name="login">The login to add to the user.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task AddLoginAsync(VyprUser user, UserLoginInfo login, CancellationToken cancellationToken = default)
        {
            return base.AddLoginAsync(user, login, cancellationToken);
        }

        /// <summary>
        /// Adds the given <paramref name="normalizedRoleName" /> to the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to add the role to.</param>
        /// <param name="normalizedRoleName">The role to add.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task AddToRoleAsync(VyprUser user, string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Please use AddToRoleAsync with tenant");
        }

        /// <summary>
        /// Converts the provided <paramref name="id" /> to a strongly typed key object.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>
        /// An instance of <typeparamref name="TKey" /> representing the provided <paramref name="id" />.
        /// </returns>
        public override int ConvertIdFromString(string id)
        {
            return base.ConvertIdFromString(id);
        }

        /// <summary>
        /// Converts the provided <paramref name="id" /> to its string representation.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>
        /// An <see cref="T:System.String" /> representation of the provided <paramref name="id" />.
        /// </returns>
        public override string ConvertIdToString(int id)
        {
            return base.ConvertIdToString(id);
        }

        /// <summary>
        /// Returns how many recovery code are still valid for a user.
        /// </summary>
        /// <param name="user">The user who owns the recovery code.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The number of valid recovery codes for the user..
        /// </returns>
        public override Task<int> CountCodesAsync(VyprUser user, CancellationToken cancellationToken)
        {
            return base.CountCodesAsync(user, cancellationToken);
        }

        /// <summary>
        /// Creates the specified <paramref name="user" /> in the user store.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the creation operation.
        /// </returns>
        public override Task<IdentityResult> CreateAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.CreateAsync(user, cancellationToken);
        }

        /// <summary>
        /// Deletes the specified <paramref name="user" /> from the user store.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the update operation.
        /// </returns>
        public override Task<IdentityResult> DeleteAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.DeleteAsync(user, cancellationToken);
        }

        /// <summary>
        /// Gets the user, if any, associated with the specified, normalized email address.
        /// </summary>
        /// <param name="normalizedEmail">The normalized email address to return the user for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous lookup operation, the user if any associated with the specified normalized email address.
        /// </returns>
        public override Task<VyprUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            return base.FindByEmailAsync(normalizedEmail, cancellationToken);
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified <paramref name="userId" />.
        /// </summary>
        /// <param name="userId">The user ID to search for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user matching the specified <paramref name="userId" /> if it exists.
        /// </returns>
        public async Task<VyprUser> FindByIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            var userIdString = base.ConvertIdToString(userId);
            return await base.FindByIdAsync(userIdString, cancellationToken);
        }

        /// <summary>
        /// Retrieves the user associated with the specified login provider and login provider key.
        /// </summary>
        /// <param name="loginProvider">The login provider who provided the <paramref name="providerKey" />.</param>
        /// <param name="providerKey">The key provided by the <paramref name="loginProvider" /> to identify a user.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> for the asynchronous operation, containing the user, if any which matched the specified login provider and key.
        /// </returns>
        public override Task<VyprUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken = default)
        {
            return base.FindByLoginAsync(loginProvider, providerKey, cancellationToken);
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified normalized user name.
        /// </summary>
        /// <param name="normalizedUserName">The normalized user name to search for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user matching the specified <paramref name="normalizedUserName" /> if it exists.
        /// </returns>
        public override Task<VyprUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            return base.FindByNameAsync(normalizedUserName, cancellationToken);
        }

        /// <summary>
        /// Retrieves the current failed access count for the specified <paramref name="user" />..
        /// </summary>
        /// <param name="user">The user whose failed access count should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the failed access count.
        /// </returns>
        public override Task<int> GetAccessFailedCountAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetAccessFailedCountAsync(user, cancellationToken);
        }

        /// <summary>
        /// Get the authenticator key for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose security stamp should be set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the security stamp for the specified <paramref name="user" />.
        /// </returns>
        public override Task<string> GetAuthenticatorKeyAsync(VyprUser user, CancellationToken cancellationToken)
        {
            return base.GetAuthenticatorKeyAsync(user, cancellationToken);
        }

        /// <summary>
        /// Get the claims associated with the specified <paramref name="user" /> as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose claims should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the claims granted to a user.
        /// </returns>
        public override Task<IList<Claim>> GetClaimsAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            // these are user claims. anything referred to in this class are user claims and not claims associated to a role
            return base.GetClaimsAsync(user, cancellationToken);
        }

        /// <summary>
        /// Gets the email address for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose email should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous operation, the email address for the specified <paramref name="user" />.
        /// </returns>
        public override Task<string> GetEmailAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetEmailAsync(user, cancellationToken);
        }

        /// <summary>
        /// Gets a flag indicating whether the email address for the specified <paramref name="user" /> has been verified, true if the email address is verified otherwise
        /// false.
        /// </summary>
        /// <param name="user">The user whose email confirmation status should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous operation, a flag indicating whether the email address for the specified <paramref name="user" />
        /// has been confirmed or not.
        /// </returns>
        public override Task<bool> GetEmailConfirmedAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetEmailConfirmedAsync(user, cancellationToken);
        }

        /// <summary>
        /// Retrieves a flag indicating whether user lockout can enabled for the specified user.
        /// </summary>
        /// <param name="user">The user whose ability to be locked out should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, true if a user can be locked out, otherwise false.
        /// </returns>
        public override Task<bool> GetLockoutEnabledAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetLockoutEnabledAsync(user, cancellationToken);
        }

        /// <summary>
        /// Gets the last <see cref="T:System.DateTimeOffset" /> a user's last lockout expired, if any.
        /// Any time in the past should be indicates a user is not locked out.
        /// </summary>
        /// <param name="user">The user whose lockout date should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the asynchronous query, a <see cref="T:System.DateTimeOffset" /> containing the last time
        /// a user's lockout expired, if any.
        /// </returns>
        public override Task<DateTimeOffset?> GetLockoutEndDateAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetLockoutEndDateAsync(user, cancellationToken);
        }

        /// <summary>
        /// Retrieves the associated logins for the specified <param ref="user" />.
        /// </summary>
        /// <param name="user">The user whose associated logins to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> for the asynchronous operation, containing a list of <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> for the specified <paramref name="user" />, if any.
        /// </returns>
        public override Task<IList<UserLoginInfo>> GetLoginsAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetLoginsAsync(user, cancellationToken);
        }

        /// <summary>
        /// Returns the normalized email for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose email address to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous lookup operation, the normalized email address if any associated with the specified user.
        /// </returns>
        public override Task<string> GetNormalizedEmailAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetNormalizedEmailAsync(user, cancellationToken);
        }

        /// <summary>
        /// Gets the normalized user name for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose normalized name should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the normalized user name for the specified <paramref name="user" />.
        /// </returns>
        public override Task<string> GetNormalizedUserNameAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetNormalizedUserNameAsync(user, cancellationToken);
        }

        /// <summary>
        /// Gets the password hash for a user.
        /// </summary>
        /// <param name="user">The user to retrieve the password hash for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the password hash for the user.
        /// </returns>
        public override Task<string> GetPasswordHashAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetPasswordHashAsync(user, cancellationToken);
        }

        /// <summary>
        /// Gets the telephone number, if any, for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose telephone number should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user's telephone number, if any.
        /// </returns>
        public override Task<string> GetPhoneNumberAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetPhoneNumberAsync(user, cancellationToken);
        }

        /// <summary>
        /// Gets a flag indicating whether the specified <paramref name="user" />'s telephone number has been confirmed.
        /// </summary>
        /// <param name="user">The user to return a flag for, indicating whether their telephone number is confirmed.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, returning true if the specified <paramref name="user" /> has a confirmed
        /// telephone number otherwise false.
        /// </returns>
        public override Task<bool> GetPhoneNumberConfirmedAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetPhoneNumberConfirmedAsync(user, cancellationToken);
        }

        /// <summary>
        /// Retrieves the roles the specified <paramref name="user" /> is a member of.
        /// </summary>
        /// <param name="user">The user whose roles should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the roles the user is a member of.
        /// </returns>
        public override Task<IList<string>> GetRolesAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetRolesAsync(user, cancellationToken);
        }

        /// <summary>
        /// Get the security stamp for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose security stamp should be set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the security stamp for the specified <paramref name="user" />.
        /// </returns>
        public override Task<string> GetSecurityStampAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetSecurityStampAsync(user, cancellationToken);
        }

        /// <summary>
        /// Returns the token value.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="loginProvider">The authentication provider for the token.</param>
        /// <param name="name">The name of the token.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task<string> GetTokenAsync(VyprUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            return base.GetTokenAsync(user, loginProvider, name, cancellationToken);
        }

        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="user" /> has two factor authentication enabled or not,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose two factor authentication enabled status should be set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing a flag indicating whether the specified
        /// <paramref name="user" /> has two factor authentication enabled or not.
        /// </returns>
        public override Task<bool> GetTwoFactorEnabledAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetTwoFactorEnabledAsync(user, cancellationToken);
        }

        /// <summary>
        /// Gets the user identifier for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose identifier should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the identifier for the specified <paramref name="user" />.
        /// </returns>
        public override Task<string> GetUserIdAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetUserIdAsync(user, cancellationToken);
        }

        /// <summary>
        /// Gets the user name for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose name should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the name for the specified <paramref name="user" />.
        /// </returns>
        public override Task<string> GetUserNameAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.GetUserNameAsync(user, cancellationToken);
        }

        /// <summary>
        /// Retrieves all users with the specified claim.
        /// </summary>
        /// <param name="claim">The claim whose users should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> contains a list of users, if any, that contain the specified claim.
        /// </returns>
        public override Task<IList<VyprUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default)
        {
            return base.GetUsersForClaimAsync(claim, cancellationToken);
        }

        /// <summary>
        /// Returns a flag indicating if the specified user has a password.
        /// </summary>
        /// <param name="user">The user to retrieve the password hash for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> containing a flag indicating if the specified user has a password. If the
        /// user has a password the returned value with be true, otherwise it will be false.
        /// </returns>
        public override Task<bool> HasPasswordAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.HasPasswordAsync(user, cancellationToken);
        }

        /// <summary>
        /// Records that a failed access has occurred, incrementing the failed access count.
        /// </summary>
        /// <param name="user">The user whose cancellation count should be incremented.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the incremented failed access count.
        /// </returns>
        public override Task<int> IncrementAccessFailedCountAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.IncrementAccessFailedCountAsync(user, cancellationToken);
        }

        /// <summary>
        /// Returns a flag indicating if the specified user is a member of the give <paramref name="normalizedRoleName" />.
        /// </summary>
        /// <param name="user">The user whose role membership should be checked.</param>
        /// <param name="normalizedRoleName">The role to check membership of</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> containing a flag indicating if the specified user is a member of the given group. If the
        /// user is a member of the group the returned value with be true, otherwise it will be false.
        /// </returns>
        /// <exception cref="NotImplementedException">Please use IsInRoleAsync with tenant</exception>
        public override Task<bool> IsInRoleAsync(VyprUser user, string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Please use IsInRoleAsync with tenant");
        }

        /// <summary>
        /// Returns whether a recovery code is valid for a user. Note: recovery codes are only valid
        /// once, and will be invalid after use.
        /// </summary>
        /// <param name="user">The user who owns the recovery code.</param>
        /// <param name="code">The recovery code to use.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// True if the recovery code was found for the user.
        /// </returns>
        public override Task<bool> RedeemCodeAsync(VyprUser user, string code, CancellationToken cancellationToken)
        {
            return base.RedeemCodeAsync(user, code, cancellationToken);
        }

        /// <summary>
        /// Removes the <paramref name="claims" /> given from the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to remove the claims from.</param>
        /// <param name="claims">The claim to remove.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task RemoveClaimsAsync(VyprUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            return base.RemoveClaimsAsync(user, claims, cancellationToken);
        }

        /// <summary>
        /// Removes the given <paramref name="normalizedRoleName" /> from the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to remove the role from.</param>
        /// <param name="normalizedRoleName">The role to remove.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        /// 
        public override Task RemoveFromRoleAsync(VyprUser user, string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Please use RemoveFromRoleAsync with tenant");
        }

        /// <summary>
        /// Removes the <paramref name="loginProvider" /> given from the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to remove the login from.</param>
        /// <param name="loginProvider">The login to remove from the user.</param>
        /// <param name="providerKey">The key provided by the <paramref name="loginProvider" /> to identify a user.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task RemoveLoginAsync(VyprUser user, string loginProvider, string providerKey, CancellationToken cancellationToken = default)
        {
            return base.RemoveLoginAsync(user, loginProvider, providerKey, cancellationToken);
        }

        /// <summary>
        /// Deletes a token for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="loginProvider">The authentication provider for the token.</param>
        /// <param name="name">The name of the token.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task RemoveTokenAsync(VyprUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            return base.RemoveTokenAsync(user, loginProvider, name, cancellationToken);
        }

        /// <summary>
        /// Replaces the <paramref name="claim" /> on the specified <paramref name="user" />, with the <paramref name="newClaim" />.
        /// </summary>
        /// <param name="user">The user to replace the claim on.</param>
        /// <param name="claim">The claim replace.</param>
        /// <param name="newClaim">The new claim replacing the <paramref name="claim" />.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task ReplaceClaimAsync(VyprUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default)
        {
            return base.ReplaceClaimAsync(user, claim, newClaim, cancellationToken);
        }

        /// <summary>
        /// Updates the recovery codes for the user while invalidating any previous recovery codes.
        /// </summary>
        /// <param name="user">The user to store new recovery codes for.</param>
        /// <param name="recoveryCodes">The new recovery codes for the user.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The new recovery codes for the user.
        /// </returns>
        public override Task ReplaceCodesAsync(VyprUser user, IEnumerable<string> recoveryCodes, CancellationToken cancellationToken)
        {
            return base.ReplaceCodesAsync(user, recoveryCodes, cancellationToken);
        }

        /// <summary>
        /// Resets a user's failed access count.
        /// </summary>
        /// <param name="user">The user whose failed access count should be reset.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        /// <remarks>
        /// This is typically called after the account is successfully accessed.
        /// </remarks>
        public override Task ResetAccessFailedCountAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.ResetAccessFailedCountAsync(user, cancellationToken);
        }

        /// <summary>
        /// Sets the authenticator key for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose authenticator key should be set.</param>
        /// <param name="key">The authenticator key to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetAuthenticatorKeyAsync(VyprUser user, string key, CancellationToken cancellationToken)
        {
            return base.SetAuthenticatorKeyAsync(user, key, cancellationToken);
        }

        /// <summary>
        /// Sets the <paramref name="email" /> address for a <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose email should be set.</param>
        /// <param name="email">The email to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public override Task SetEmailAsync(VyprUser user, string email, CancellationToken cancellationToken = default)
        {
            return base.SetEmailAsync(user, email, cancellationToken);
        }

        /// <summary>
        /// Sets the flag indicating whether the specified <paramref name="user" />'s email address has been confirmed or not.
        /// </summary>
        /// <param name="user">The user whose email confirmation status should be set.</param>
        /// <param name="confirmed">A flag indicating if the email address has been confirmed, true if the address is confirmed otherwise false.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public override Task SetEmailConfirmedAsync(VyprUser user, bool confirmed, CancellationToken cancellationToken = default)
        {
            return base.SetEmailConfirmedAsync(user, confirmed, cancellationToken);
        }

        /// <summary>
        /// Set the flag indicating if the specified <paramref name="user" /> can be locked out..
        /// </summary>
        /// <param name="user">The user whose ability to be locked out should be set.</param>
        /// <param name="enabled">A flag indicating if lock out can be enabled for the specified <paramref name="user" />.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetLockoutEnabledAsync(VyprUser user, bool enabled, CancellationToken cancellationToken = default)
        {
            return base.SetLockoutEnabledAsync(user, enabled, cancellationToken);
        }

        /// <summary>
        /// Locks out a user until the specified end date has passed. Setting a end date in the past immediately unlocks a user.
        /// </summary>
        /// <param name="user">The user whose lockout date should be set.</param>
        /// <param name="lockoutEnd">The <see cref="T:System.DateTimeOffset" /> after which the <paramref name="user" />'s lockout should end.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetLockoutEndDateAsync(VyprUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken = default)
        {
            return base.SetLockoutEndDateAsync(user, lockoutEnd, cancellationToken);
        }

        /// <summary>
        /// Sets the normalized email for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose email address to set.</param>
        /// <param name="normalizedEmail">The normalized email to set for the specified <paramref name="user" />.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public override Task SetNormalizedEmailAsync(VyprUser user, string normalizedEmail, CancellationToken cancellationToken = default)
        {
            return base.SetNormalizedEmailAsync(user, normalizedEmail, cancellationToken);
        }

        /// <summary>
        /// Sets the given normalized name for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose name should be set.</param>
        /// <param name="normalizedName">The normalized name to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetNormalizedUserNameAsync(VyprUser user, string normalizedName, CancellationToken cancellationToken = default)
        {
            return base.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }

        /// <summary>
        /// Sets the password hash for a user.
        /// </summary>
        /// <param name="user">The user to set the password hash for.</param>
        /// <param name="passwordHash">The password hash to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetPasswordHashAsync(VyprUser user, string passwordHash, CancellationToken cancellationToken = default)
        {
            return base.SetPasswordHashAsync(user, passwordHash, cancellationToken);
        }

        /// <summary>
        /// Sets the telephone number for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose telephone number should be set.</param>
        /// <param name="phoneNumber">The telephone number to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetPhoneNumberAsync(VyprUser user, string phoneNumber, CancellationToken cancellationToken = default)
        {
            return base.SetPhoneNumberAsync(user, phoneNumber, cancellationToken);
        }

        /// <summary>
        /// Sets a flag indicating if the specified <paramref name="user" />'s phone number has been confirmed..
        /// </summary>
        /// <param name="user">The user whose telephone number confirmation status should be set.</param>
        /// <param name="confirmed">A flag indicating whether the user's telephone number has been confirmed.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetPhoneNumberConfirmedAsync(VyprUser user, bool confirmed, CancellationToken cancellationToken = default)
        {
            return base.SetPhoneNumberConfirmedAsync(user, confirmed, cancellationToken);
        }

        /// <summary>
        /// Sets the provided security <paramref name="stamp" /> for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose security stamp should be set.</param>
        /// <param name="stamp">The security stamp to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetSecurityStampAsync(VyprUser user, string stamp, CancellationToken cancellationToken = default)
        {
            return base.SetSecurityStampAsync(user, stamp, cancellationToken);
        }

        /// <summary>
        /// Sets the token value for a particular user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="loginProvider">The authentication provider for the token.</param>
        /// <param name="name">The name of the token.</param>
        /// <param name="value">The value of the token.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetTokenAsync(VyprUser user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            return base.SetTokenAsync(user, loginProvider, name, value, cancellationToken);
        }

        /// <summary>
        /// Sets a flag indicating whether the specified <paramref name="user" /> has two factor authentication enabled or not,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose two factor authentication enabled status should be set.</param>
        /// <param name="enabled">A flag indicating whether the specified <paramref name="user" /> has two factor authentication enabled.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetTwoFactorEnabledAsync(VyprUser user, bool enabled, CancellationToken cancellationToken = default)
        {
            return base.SetTwoFactorEnabledAsync(user, enabled, cancellationToken);
        }

        /// <summary>
        /// Sets the given <paramref name="userName" /> for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose name should be set.</param>
        /// <param name="userName">The user name to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetUserNameAsync(VyprUser user, string userName, CancellationToken cancellationToken = default)
        {
            return base.SetUserNameAsync(user, userName, cancellationToken);
        }

        /// <summary>
        /// Updates the specified <paramref name="user" /> in the user store.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the update operation.
        /// </returns>
        public override Task<IdentityResult> UpdateAsync(VyprUser user, CancellationToken cancellationToken = default)
        {
            return base.UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// Add a new user token.
        /// </summary>
        /// <param name="token">The token to be added.</param>
        /// <returns></returns>
        protected override Task AddUserTokenAsync(VyprUserToken token)
        {
            return base.AddUserTokenAsync(token);
        }

        /// <summary>
        /// Called to create a new instance of a <see cref="T:Microsoft.AspNetCore.Identity.IdentityUserClaim`1" />.
        /// </summary>
        /// <param name="user">The associated user.</param>
        /// <param name="claim">The associated claim.</param>
        /// <returns></returns>
        protected override VyprUserClaim CreateUserClaim(VyprUser user, Claim claim)
        {
            return base.CreateUserClaim(user, claim);
        }

        /// <summary>
        /// Called to create a new instance of a <see cref="T:Microsoft.AspNetCore.Identity.IdentityUserLogin`1" />.
        /// </summary>
        /// <param name="user">The associated user.</param>
        /// <param name="login">The sasociated login.</param>
        /// <returns></returns>
        protected override VyprUserLogin CreateUserLogin(VyprUser user, UserLoginInfo login)
        {
            return base.CreateUserLogin(user, login);
        }

        /// <summary>
        /// Called to create a new instance of a <see cref="T:Microsoft.AspNetCore.Identity.IdentityUserToken`1" />.
        /// </summary>
        /// <param name="user">The associated user.</param>
        /// <param name="loginProvider">The associated login provider.</param>
        /// <param name="name">The name of the user token.</param>
        /// <param name="value">The value of the user token.</param>
        /// <returns></returns>
        protected override VyprUserToken CreateUserToken(VyprUser user, string loginProvider, string name, string value)
        {
            return base.CreateUserToken(user, loginProvider, name, value);
        }

        /// <summary>
        /// Return a role with the normalized name if it exists.
        /// </summary>
        /// <param name="normalizedRoleName">The normalized role name.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The role if it exists.
        /// </returns>
        protected override Task<VyprRole> FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return base.FindRoleAsync(normalizedRoleName, cancellationToken);
        }

        /// <summary>
        /// Find a user token if it exists.
        /// </summary>
        /// <param name="user">The token owner.</param>
        /// <param name="loginProvider">The login provider for the token.</param>
        /// <param name="name">The name of the token.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The user token if it exists.
        /// </returns>
        protected override Task<VyprUserToken> FindTokenAsync(VyprUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            return base.FindTokenAsync(user, loginProvider, name, cancellationToken);
        }

        /// <summary>
        /// Return a user with the matching userId if it exists.
        /// </summary>
        /// <param name="userId">The user's id.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The user if it exists.
        /// </returns>
        protected override Task<VyprUser> FindUserAsync(int userId, CancellationToken cancellationToken)
        {
            return base.FindUserAsync(userId, cancellationToken);
        }

        /// <summary>
        /// Return a user login with the matching userId, provider, providerKey if it exists.
        /// </summary>
        /// <param name="userId">The user's id.</param>
        /// <param name="loginProvider">The login provider name.</param>
        /// <param name="providerKey">The key provided by the <paramref name="loginProvider" /> to identify a user.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The user login if it exists.
        /// </returns>
        protected override Task<VyprUserLogin> FindUserLoginAsync(int userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return base.FindUserLoginAsync(userId, loginProvider, providerKey, cancellationToken);
        }

        /// <summary>
        /// Return a user login with  provider, providerKey if it exists.
        /// </summary>
        /// <param name="loginProvider">The login provider name.</param>
        /// <param name="providerKey">The key provided by the <paramref name="loginProvider" /> to identify a user.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The user login if it exists.
        /// </returns>
        protected override Task<VyprUserLogin> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return base.FindUserLoginAsync(loginProvider, providerKey, cancellationToken);
        }

        /// <summary>
        /// Remove a new user token.
        /// </summary>
        /// <param name="token">The token to be removed.</param>
        /// <returns></returns>
        protected override Task RemoveUserTokenAsync(VyprUserToken token)
        {
            return base.RemoveUserTokenAsync(token);
        }
    }
}
