// <copyright file="VyprUserManager.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Authentication.Managers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using VyprCore.Interfaces.Email;
    using VyprCore.Models.Domain;
    using VyprCore.Utilities.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Vypr.Server.Authentication.Stores;
    using Vypr.Server.Authentication.Senders;

    /// <summary>
    /// Vypr User Manager
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.UserManager{Vypr.Server.Authentication.Classes.VyprUser}" />
    public class VyprUserManager : UserManager<VyprUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprUserManager" /> class.
        /// </summary>
        /// <param name="userStore">The user store.</param>
        /// <param name="options">The options.</param>
        /// <param name="passwordHasher">The password hasher.</param>
        /// <param name="userValidators">The user validators.</param>
        /// <param name="passwordValidators">The password validators.</param>
        /// <param name="lookupNormalizer">The lookup normalizer.</param>
        /// <param name="identityErrorDescriber">The identity error describer.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="url">The link generator.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="emailTemplater">The email templater.</param>
        public VyprUserManager(IUserStore<VyprUser> userStore, IOptions<IdentityOptions> options, IPasswordHasher<VyprUser> passwordHasher, IEnumerable<IUserValidator<VyprUser>> userValidators, IEnumerable<IPasswordValidator<VyprUser>> passwordValidators, ILookupNormalizer lookupNormalizer, IdentityErrorDescriber identityErrorDescriber, IServiceProvider serviceProvider, ILogger<UserManager<VyprUser>> logger, LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor, IEmailTemplater emailTemplater) : base(userStore, options, passwordHasher, userValidators, passwordValidators, lookupNormalizer, identityErrorDescriber, serviceProvider, logger)
        {
            _linkGenerator = linkGenerator;
            _emailTemplater = emailTemplater;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// The link generator
        /// </summary>
        private readonly LinkGenerator _linkGenerator;

        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// The email templater
        /// </summary>
        private readonly IEmailTemplater _emailTemplater;

        /// <summary>
        /// The cancellation token used to cancel operations.
        /// </summary>
        protected override CancellationToken CancellationToken => base.CancellationToken;

        /// <summary>
        /// The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> used to log messages from the manager.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> used to log messages from the manager.
        /// </value>
        public override ILogger Logger { get => base.Logger; set => base.Logger = value; }

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports returning
        /// <see cref="T:System.Linq.IQueryable" /> collections of information.
        /// </summary>
        /// <value>
        /// true if the backing user store supports returning <see cref="T:System.Linq.IQueryable" /> collections of
        /// information, otherwise false.
        /// </value>
        public override bool SupportsQueryableUsers => base.SupportsQueryableUsers;

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports authentication tokens.
        /// </summary>
        /// <value>
        /// true if the backing user store supports authentication tokens, otherwise false.
        /// </value>
        public override bool SupportsUserAuthenticationTokens => base.SupportsUserAuthenticationTokens;

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports a user authenticator.
        /// </summary>
        /// <value>
        /// true if the backing user store supports a user authenticator, otherwise false.
        /// </value>
        public override bool SupportsUserAuthenticatorKey => base.SupportsUserAuthenticatorKey;

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports user claims.
        /// </summary>
        /// <value>
        /// true if the backing user store supports user claims, otherwise false.
        /// </value>
        public override bool SupportsUserClaim => base.SupportsUserClaim;

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports user emails.
        /// </summary>
        /// <value>
        /// true if the backing user store supports user emails, otherwise false.
        /// </value>
        public override bool SupportsUserEmail => base.SupportsUserEmail;

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports user lock-outs.
        /// </summary>
        /// <value>
        /// true if the backing user store supports user lock-outs, otherwise false.
        /// </value>
        public override bool SupportsUserLockout => base.SupportsUserLockout;

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports external logins.
        /// </summary>
        /// <value>
        /// true if the backing user store supports external logins, otherwise false.
        /// </value>
        public override bool SupportsUserLogin => base.SupportsUserLogin;

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports user passwords.
        /// </summary>
        /// <value>
        /// true if the backing user store supports user passwords, otherwise false.
        /// </value>
        public override bool SupportsUserPassword => base.SupportsUserPassword;

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports user telephone numbers.
        /// </summary>
        /// <value>
        /// true if the backing user store supports user telephone numbers, otherwise false.
        /// </value>
        public override bool SupportsUserPhoneNumber => base.SupportsUserPhoneNumber;

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports user roles.
        /// </summary>
        /// <value>
        /// true if the backing user store supports user roles, otherwise false.
        /// </value>
        public override bool SupportsUserRole => base.SupportsUserRole;

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports security stamps.
        /// </summary>
        /// <value>
        /// true if the backing user store supports user security stamps, otherwise false.
        /// </value>
        public override bool SupportsUserSecurityStamp => base.SupportsUserSecurityStamp;

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports two factor authentication.
        /// </summary>
        /// <value>
        /// true if the backing user store supports user two factor authentication, otherwise false.
        /// </value>
        public override bool SupportsUserTwoFactor => base.SupportsUserTwoFactor;

        /// <summary>
        /// Gets a flag indicating whether the backing user store supports recovery codes.
        /// </summary>
        /// <value>
        /// true if the backing user store supports a user authenticator, otherwise false.
        /// </value>
        public override bool SupportsUserTwoFactorRecoveryCodes => base.SupportsUserTwoFactorRecoveryCodes;
        /// <summary>
        /// Returns an IQueryable of users if the store is an IQueryableUserStore
        /// </summary>
        public override IQueryable<VyprUser> Users => base.Users;
        /// <summary>
        /// Increments the access failed count for the user as an asynchronous operation.
        /// If the failed access account is greater than or equal to the configured maximum number of attempts,
        /// the user will be locked out for the configured lockout time span.
        /// </summary>
        /// <param name="user">The user whose failed access count to increment.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the operation.
        /// </returns>
        public override Task<IdentityResult> AccessFailedAsync(VyprUser user)
        {
            return base.AccessFailedAsync(user);
        }

        /// <summary>
        /// Adds the specified <paramref name="claim" /> to the <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to add the claim to.</param>
        /// <param name="claim">The claim to add.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> AddClaimAsync(VyprUser user, Claim claim)
        {
            return base.AddClaimAsync(user, claim);
        }

        /// <summary>
        /// Adds the specified <paramref name="claims" /> to the <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to add the claim to.</param>
        /// <param name="claims">The claims to add.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> AddClaimsAsync(VyprUser user, IEnumerable<Claim> claims)
        {
            return base.AddClaimsAsync(user, claims);
        }

        /// <summary>
        /// Adds an external <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> to the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to add the login to.</param>
        /// <param name="login">The external <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> to add to the specified <paramref name="user" />.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> AddLoginAsync(VyprUser user, UserLoginInfo login)
        {
            return base.AddLoginAsync(user, login);
        }

        /// <summary>
        /// Adds the <paramref name="password" /> to the specified <paramref name="user" /> only if the user
        /// does not already have a password.
        /// </summary>
        /// <param name="user">The user whose password should be set.</param>
        /// <param name="password">The password to set.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> AddPasswordAsync(VyprUser user, string password)
        {
            return base.AddPasswordAsync(user, password);
        }

        /// <summary>
        /// Add the specified <paramref name="user" /> to the named role.
        /// </summary>
        /// <param name="user">The user to add to the named role.</param>
        /// <param name="role">The name of the role to add the user to.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        /// <exception cref="NotImplementedException">Please use AddToRoleAsync with tenant</exception>
        public override Task<IdentityResult> AddToRoleAsync(VyprUser user, string role)
        {
            throw new NotImplementedException("Please use AddToRoleAsync with tenant");
        }

        /// <summary>
        /// Add the specified <paramref name="user" /> to the named roles.
        /// </summary>
        /// <param name="user">The user to add to the named roles.</param>
        /// <param name="roles">The name of the roles to add the user to.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        /// <exception cref="NotImplementedException">Please use AddToRolesAsync with tenant</exception>
        public override Task<IdentityResult> AddToRolesAsync(VyprUser user, IEnumerable<string> roles)
        {
            throw new NotImplementedException("Please use AddToRolesAsync with tenant");
        }

        /// <summary>
        /// Updates a users emails if the specified email change <paramref name="token" /> is valid for the user.
        /// </summary>
        /// <param name="user">The user whose email should be updated.</param>
        /// <param name="newEmail">The new email address.</param>
        /// <param name="token">The change email token to be verified.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> ChangeEmailAsync(VyprUser user, string newEmail, string token)
        {
            return base.ChangeEmailAsync(user, newEmail, token);
        }

        /// <summary>
        /// Changes a user's password after confirming the specified <paramref name="currentPassword" /> is correct,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose password should be set.</param>
        /// <param name="currentPassword">The current password to validate before changing.</param>
        /// <param name="newPassword">The new password to set for the specified <paramref name="user" />.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> ChangePasswordAsync(VyprUser user, string currentPassword, string newPassword)
        {
            return base.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        /// <summary>
        /// Sets the phone number for the specified <paramref name="user" /> if the specified
        /// change <paramref name="token" /> is valid.
        /// </summary>
        /// <param name="user">The user whose phone number to set.</param>
        /// <param name="phoneNumber">The phone number to set.</param>
        /// <param name="token">The phone number confirmation token to validate.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> ChangePhoneNumberAsync(VyprUser user, string phoneNumber, string token)
        {
            return base.ChangePhoneNumberAsync(user, phoneNumber, token);
        }

        /// <summary>
        /// Returns a flag indicating whether the given <paramref name="password" /> is valid for the
        /// specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose password should be validated.</param>
        /// <param name="password">The password to validate</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing true if
        /// the specified <paramref name="password" /> matches the one store for the <paramref name="user" />,
        /// otherwise false.
        /// </returns>
        public override Task<bool> CheckPasswordAsync(VyprUser user, string password)
        {
            return base.CheckPasswordAsync(user, password);
        }

        /// <summary>
        /// Validates that an email confirmation token matches the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to validate the token against.</param>
        /// <param name="token">The email confirmation token to validate.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> ConfirmEmailAsync(VyprUser user, string token)
        {
            return base.ConfirmEmailAsync(user, token);
        }

        /// <summary>
        /// Returns how many recovery code are still valid for a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// How many recovery code are still valid for a user.
        /// </returns>
        public override Task<int> CountRecoveryCodesAsync(VyprUser user)
        {
            return base.CountRecoveryCodesAsync(user);
        }

        /// <summary>
        /// Creates the specified <paramref name="user" /> in the backing store with no password,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> CreateAsync(VyprUser user)
        {
            return base.CreateAsync(user);
        }

        /// <summary>
        /// Creates the specified <paramref name="user" /> in the backing store with given password,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <param name="password">The password for the user to hash and store.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> CreateAsync(VyprUser user, string password)
        {
            return base.CreateAsync(user, password);
        }

        /// <summary>
        /// Creates bytes to use as a security token from the user's security stamp.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The security token bytes.
        /// </returns>
        public override Task<byte[]> CreateSecurityTokenAsync(VyprUser user)
        {
            return base.CreateSecurityTokenAsync(user);
        }

        /// <summary>
        /// Deletes the specified <paramref name="user" /> from the backing store.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> DeleteAsync(VyprUser user)
        {
            return base.DeleteAsync(user);
        }

        /// <summary>
        /// Gets the user, if any, associated with the normalized value of the specified email address.
        /// Note: Its recommended that identityOptions.User.RequireUniqueEmail be set to true when using this method, otherwise
        /// the store may throw if there are users with duplicate emails.
        /// </summary>
        /// <param name="email">The email address to return the user for.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous lookup operation, the user, if any, associated with a normalized value of the specified email address.
        /// </returns>
        public override Task<VyprUser> FindByEmailAsync(string email)
        {
            return base.FindByEmailAsync(email);
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified <paramref name="userId" />.
        /// </summary>
        /// <param name="userId">The user ID to search for.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user matching the specified <paramref name="userId" /> if it exists.
        /// </returns>
        public override Task<VyprUser> FindByIdAsync(string userId)
        {
            return base.FindByIdAsync(userId);
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified <paramref name="userId" />.
        /// </summary>
        /// <param name="userId">The user ID to search for.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user matching the specified <paramref name="userId" /> if it exists.
        /// </returns>
        public async Task<VyprUser> FindByIdAsync(int userId)
        {
            ThrowIfDisposed();
            return await GetUserStore().FindByIdAsync(userId, CancellationToken);
        }

        /// <summary>
        /// Retrieves the user associated with the specified external login provider and login provider key.
        /// </summary>
        /// <param name="loginProvider">The login provider who provided the <paramref name="providerKey" />.</param>
        /// <param name="providerKey">The key provided by the <paramref name="loginProvider" /> to identify a user.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> for the asynchronous operation, containing the user, if any which matched the specified login provider and key.
        /// </returns>
        public override Task<VyprUser> FindByLoginAsync(string loginProvider, string providerKey)
        {
            return base.FindByLoginAsync(loginProvider, providerKey);
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified user name.
        /// </summary>
        /// <param name="userName">The user name to search for.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user matching the specified <paramref name="userName" /> if it exists.
        /// </returns>
        public override Task<VyprUser> FindByNameAsync(string userName)
        {
            return base.FindByNameAsync(userName);
        }

        /// <summary>
        /// Generates an email change token for the specified user.
        /// </summary>
        /// <param name="user">The user to generate an email change token for.</param>
        /// <param name="newEmail">The new email address.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, an email change token.
        /// </returns>
        public override Task<string> GenerateChangeEmailTokenAsync(VyprUser user, string newEmail)
        {
            return base.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        /// <summary>
        /// Generates a telephone number change token for the specified user.
        /// </summary>
        /// <param name="user">The user to generate a telephone number token for.</param>
        /// <param name="phoneNumber">The new phone number the validation token should be sent to.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the telephone change number token.
        /// </returns>
        public override Task<string> GenerateChangePhoneNumberTokenAsync(VyprUser user, string phoneNumber)
        {
            return base.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
        }

        /// <summary>
        /// Generates a value suitable for use in concurrency tracking.
        /// </summary>
        /// <param name="user">The user to generate the stamp for.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the security
        /// stamp for the specified <paramref name="user" />.
        /// </returns>
        public override Task<string> GenerateConcurrencyStampAsync(VyprUser user)
        {
            return base.GenerateConcurrencyStampAsync(user);
        }

        /// <summary>
        /// Generates an email confirmation token for the specified user.
        /// </summary>
        /// <param name="user">The user to generate an email confirmation token for.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, an email confirmation token.
        /// </returns>
        public override Task<string> GenerateEmailConfirmationTokenAsync(VyprUser user)
        {
            return base.GenerateEmailConfirmationTokenAsync(user);
        }

        /// <summary>
        /// Generates a new base32 encoded 160-bit security secret (size of SHA1 hash).
        /// </summary>
        /// <returns>
        /// The new security secret.
        /// </returns>
        public override string GenerateNewAuthenticatorKey()
        {
            return base.GenerateNewAuthenticatorKey();
        }

        /// <summary>
        /// Generates recovery codes for the user, this invalidates any previous recovery codes for the user.
        /// </summary>
        /// <param name="user">The user to generate recovery codes for.</param>
        /// <param name="number">The number of codes to generate.</param>
        /// <returns>
        /// The new recovery codes for the user.  Note: there may be less than number returned, as duplicates will be removed.
        /// </returns>
        public override Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(VyprUser user, int number)
        {
            return base.GenerateNewTwoFactorRecoveryCodesAsync(user, number);
        }

        /// <summary>
        /// Generates a password reset token for the specified <paramref name="user" />, using
        /// the configured password reset token provider.
        /// </summary>
        /// <param name="user">The user to generate a password reset token for.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation,
        /// containing a password reset token for the specified <paramref name="user" />.
        /// </returns>
        public override Task<string> GeneratePasswordResetTokenAsync(VyprUser user)
        {
            return base.GeneratePasswordResetTokenAsync(user);
        }

        /// <summary>
        /// Gets a two factor authentication token for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user the token is for.</param>
        /// <param name="tokenProvider">The provider which will generate the token.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents result of the asynchronous operation, a two factor authentication token
        /// for the user.
        /// </returns>
        public override Task<string> GenerateTwoFactorTokenAsync(VyprUser user, string tokenProvider)
        {
            return base.GenerateTwoFactorTokenAsync(user, tokenProvider);
        }

        /// <summary>
        /// Generates a token for the given <paramref name="user" /> and <paramref name="purpose" />.
        /// </summary>
        /// <param name="user">The user the token will be for.</param>
        /// <param name="tokenProvider">The provider which will generate the token.</param>
        /// <param name="purpose">The purpose the token will be for.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents result of the asynchronous operation, a token for
        /// the given user and purpose.
        /// </returns>
        public override Task<string> GenerateUserTokenAsync(VyprUser user, string tokenProvider, string purpose)
        {
            return base.GenerateUserTokenAsync(user, tokenProvider, purpose);
        }

        /// <summary>
        /// Retrieves the current number of failed accesses for the given <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose access failed count should be retrieved for.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that contains the result the asynchronous operation, the current failed access count
        /// for the user.
        /// </returns>
        public override Task<int> GetAccessFailedCountAsync(VyprUser user)
        {
            return base.GetAccessFailedCountAsync(user);
        }

        /// <summary>
        /// Returns an authentication token for a user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loginProvider">The authentication scheme for the provider the token is associated with.</param>
        /// <param name="tokenName">The name of the token.</param>
        /// <returns>
        /// The authentication token for a user
        /// </returns>
        public override Task<string> GetAuthenticationTokenAsync(VyprUser user, string loginProvider, string tokenName)
        {
            return base.GetAuthenticationTokenAsync(user, loginProvider, tokenName);
        }

        /// <summary>
        /// Returns the authenticator key for the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The authenticator key
        /// </returns>
        public override Task<string> GetAuthenticatorKeyAsync(VyprUser user)
        {
            return base.GetAuthenticatorKeyAsync(user);
        }

        /// <summary>
        /// Gets a list of <see cref="T:System.Security.Claims.Claim" />s to be belonging to the specified <paramref name="user" /> as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose claims to retrieve.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the asynchronous query, a list of <see cref="T:System.Security.Claims.Claim" />s.
        /// </returns>
        public override Task<IList<Claim>> GetClaimsAsync(VyprUser user)
        {
            return base.GetClaimsAsync(user);
        }

        /// <summary>
        /// Gets the email address for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose email should be returned.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous operation, the email address for the specified <paramref name="user" />.
        /// </returns>
        public override Task<string> GetEmailAsync(VyprUser user)
        {
            return base.GetEmailAsync(user);
        }

        /// <summary>
        /// Retrieves a flag indicating whether user lockout can be enabled for the specified user.
        /// </summary>
        /// <param name="user">The user whose ability to be locked out should be returned.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, true if a user can be locked out, otherwise false.
        /// </returns>
        public override Task<bool> GetLockoutEnabledAsync(VyprUser user)
        {
            return base.GetLockoutEnabledAsync(user);
        }

        /// <summary>
        /// Gets the last <see cref="T:System.DateTimeOffset" /> a user's last lockout expired, if any.
        /// A time value in the past indicates a user is not currently locked out.
        /// </summary>
        /// <param name="user">The user whose lockout date should be retrieved.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the lookup, a <see cref="T:System.DateTimeOffset" /> containing the last time a user's lockout expired, if any.
        /// </returns>
        public override Task<DateTimeOffset?> GetLockoutEndDateAsync(VyprUser user)
        {
            return base.GetLockoutEndDateAsync(user);
        }

        /// <summary>
        /// Retrieves the associated logins for the specified <param ref="user" />.
        /// </summary>
        /// <param name="user">The user whose associated logins to retrieve.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> for the asynchronous operation, containing a list of <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> for the specified <paramref name="user" />, if any.
        /// </returns>
        public override Task<IList<UserLoginInfo>> GetLoginsAsync(VyprUser user)
        {
            return base.GetLoginsAsync(user);
        }

        /// <summary>
        /// Gets the telephone number, if any, for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose telephone number should be retrieved.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user's telephone number, if any.
        /// </returns>
        public override Task<string> GetPhoneNumberAsync(VyprUser user)
        {
            return base.GetPhoneNumberAsync(user);
        }

        /// <summary>
        /// Gets a list of role names the specified <paramref name="user" /> belongs to.
        /// </summary>
        /// <param name="user">The user whose role names to retrieve.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing a list of role names.
        /// </returns>
        public override Task<IList<string>> GetRolesAsync(VyprUser user)
        {
            return base.GetRolesAsync(user);
        }

        /// <summary>
        /// Get the security stamp for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose security stamp should be set.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the security stamp for the specified <paramref name="user" />.
        /// </returns>
        public override Task<string> GetSecurityStampAsync(VyprUser user)
        {
            return base.GetSecurityStampAsync(user);
        }

        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="user" /> has two factor authentication enabled or not,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose two factor authentication enabled status should be retrieved.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, true if the specified <paramref name="user " />
        /// has two factor authentication enabled, otherwise false.
        /// </returns>
        public override Task<bool> GetTwoFactorEnabledAsync(VyprUser user)
        {
            return base.GetTwoFactorEnabledAsync(user);
        }

        /// <summary>
        /// Returns the user corresponding to the IdentityOptions.ClaimsIdentity.UserIdClaimType claim in
        /// the principal or null.
        /// </summary>
        /// <param name="principal">The principal which contains the user id claim.</param>
        /// <returns>
        /// The user corresponding to the IdentityOptions.ClaimsIdentity.UserIdClaimType claim in
        /// the principal or null
        /// </returns>
        public override Task<VyprUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return base.GetUserAsync(principal);
        }

        /// <summary>
        /// Returns the User ID claim value if present otherwise returns null.
        /// </summary>
        /// <param name="principal">The <see cref="T:System.Security.Claims.ClaimsPrincipal" /> instance.</param>
        /// <returns>
        /// The User ID claim value, or null if the claim is not present.
        /// </returns>
        /// <remarks>
        /// The User ID claim is identified by <see cref="F:System.Security.Claims.ClaimTypes.NameIdentifier" />.
        /// </remarks>
        public override string GetUserId(ClaimsPrincipal principal)
        {
            return base.GetUserId(principal);
        }

        /// <summary>
        /// Gets the user identifier for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose identifier should be retrieved.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the identifier for the specified <paramref name="user" />.
        /// </returns>
        public override Task<string> GetUserIdAsync(VyprUser user)
        {
            return base.GetUserIdAsync(user);
        }

        /// <summary>
        /// Returns the Name claim value if present otherwise returns null.
        /// </summary>
        /// <param name="principal">The <see cref="T:System.Security.Claims.ClaimsPrincipal" /> instance.</param>
        /// <returns>
        /// The Name claim value, or null if the claim is not present.
        /// </returns>
        /// <remarks>
        /// The Name claim is identified by <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultNameClaimType" />.
        /// </remarks>
        public override string GetUserName(ClaimsPrincipal principal)
        {
            return base.GetUserName(principal);
        }

        /// <summary>
        /// Gets the user name for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose name should be retrieved.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the name for the specified <paramref name="user" />.
        /// </returns>
        public override Task<string> GetUserNameAsync(VyprUser user)
        {
            return base.GetUserNameAsync(user);
        }

        /// <summary>
        /// Returns a list of users from the user store who have the specified <paramref name="claim" />.
        /// </summary>
        /// <param name="claim">The claim to look for.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the asynchronous query, a list of <typeparamref name="TUser" />s who
        /// have the specified claim.
        /// </returns>
        public override Task<IList<VyprUser>> GetUsersForClaimAsync(Claim claim)
        {
            return base.GetUsersForClaimAsync(claim);
        }

        public override Task<IList<VyprUser>> GetUsersInRoleAsync(string roleName)
        {
            throw new NotImplementedException("Please use GetUsersInRoleAsync with tenant");
        }

        /// <summary>
        /// Gets a list of valid two factor token providers for the specified <paramref name="user" />,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user the whose two factor authentication providers will be returned.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents result of the asynchronous operation, a list of two
        /// factor authentication providers for the specified user.
        /// </returns>
        public override Task<IList<string>> GetValidTwoFactorProvidersAsync(VyprUser user)
        {
            return base.GetValidTwoFactorProvidersAsync(user);
        }

        /// <summary>
        /// Gets a flag indicating whether the specified <paramref name="user" /> has a password.
        /// </summary>
        /// <param name="user">The user to return a flag for, indicating whether they have a password or not.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, returning true if the specified <paramref name="user" /> has a password
        /// otherwise false.
        /// </returns>
        public override Task<bool> HasPasswordAsync(VyprUser user)
        {
            return base.HasPasswordAsync(user);
        }

        /// <summary>
        /// Gets a flag indicating whether the email address for the specified <paramref name="user" /> has been verified, true if the email address is verified otherwise
        /// false.
        /// </summary>
        /// <param name="user">The user whose email confirmation status should be returned.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous operation, a flag indicating whether the email address for the specified <paramref name="user" />
        /// has been confirmed or not.
        /// </returns>
        public override Task<bool> IsEmailConfirmedAsync(VyprUser user)
        {
            return base.IsEmailConfirmedAsync(user);
        }

        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="user" /> is a member of the given named role.
        /// </summary>
        /// <param name="user">The user whose role membership should be checked.</param>
        /// <param name="role">The name of the role to be checked.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing a flag indicating whether the specified <paramref name="user" /> is
        /// a member of the named role.
        /// </returns>
        /// <exception cref="NotImplementedException">Please use IsInRoleAsync with tenant</exception>
        public override Task<bool> IsInRoleAsync(VyprUser user, string role)
        {
            throw new NotImplementedException("Please use IsInRoleAsync with tenant");
        }

        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="user" /> is locked out,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose locked out status should be retrieved.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, true if the specified <paramref name="user " />
        /// is locked out, otherwise false.
        /// </returns>
        public override Task<bool> IsLockedOutAsync(VyprUser user)
        {
            return base.IsLockedOutAsync(user);
        }

        /// <summary>
        /// Gets a flag indicating whether the specified <paramref name="user" />'s telephone number has been confirmed.
        /// </summary>
        /// <param name="user">The user to return a flag for, indicating whether their telephone number is confirmed.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, returning true if the specified <paramref name="user" /> has a confirmed
        /// telephone number otherwise false.
        /// </returns>
        public override Task<bool> IsPhoneNumberConfirmedAsync(VyprUser user)
        {
            return base.IsPhoneNumberConfirmedAsync(user);
        }

        /// <summary>
        /// Normalize email for consistent comparisons.
        /// </summary>
        /// <param name="email">The email to normalize.</param>
        /// <returns>
        /// A normalized value representing the specified <paramref name="email" />.
        /// </returns>
        public override string NormalizeEmail(string email)
        {
            return base.NormalizeEmail(email);
        }

        /// <summary>
        /// Normalize user or role name for consistent comparisons.
        /// </summary>
        /// <param name="name">The name to normalize.</param>
        /// <returns>
        /// A normalized value representing the specified <paramref name="name" />.
        /// </returns>
        public override string NormalizeName(string name)
        {
            return base.NormalizeName(name);
        }

        /// <summary>
        /// Returns whether a recovery code is valid for a user. Note: recovery codes are only valid
        /// once, and will be invalid after use.
        /// </summary>
        /// <param name="user">The user who owns the recovery code.</param>
        /// <param name="code">The recovery code to use.</param>
        /// <returns>
        /// True if the recovery code was found for the user.
        /// </returns>
        public override Task<IdentityResult> RedeemTwoFactorRecoveryCodeAsync(VyprUser user, string code)
        {
            return base.RedeemTwoFactorRecoveryCodeAsync(user, code);
        }

        /// <summary>
        /// Registers a token provider.
        /// </summary>
        /// <param name="providerName">The name of the provider to register.</param>
        /// <param name="provider">The provider to register.</param>
        public override void RegisterTokenProvider(string providerName, IUserTwoFactorTokenProvider<VyprUser> provider)
        {
            base.RegisterTokenProvider(providerName, provider);
        }

        /// <summary>
        /// Remove an authentication token for a user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loginProvider">The authentication scheme for the provider the token is associated with.</param>
        /// <param name="tokenName">The name of the token.</param>
        /// <returns>
        /// Whether a token was removed.
        /// </returns>
        public override Task<IdentityResult> RemoveAuthenticationTokenAsync(VyprUser user, string loginProvider, string tokenName)
        {
            return base.RemoveAuthenticationTokenAsync(user, loginProvider, tokenName);
        }

        /// <summary>
        /// Removes the specified <paramref name="claim" /> from the given <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to remove the specified <paramref name="claim" /> from.</param>
        /// <param name="claim">The <see cref="T:System.Security.Claims.Claim" /> to remove.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> RemoveClaimAsync(VyprUser user, Claim claim)
        {
            return base.RemoveClaimAsync(user, claim);
        }

        /// <summary>
        /// Removes the specified <paramref name="claims" /> from the given <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to remove the specified <paramref name="claims" /> from.</param>
        /// <param name="claims">A collection of <see cref="T:System.Security.Claims.Claim" />s to remove.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> RemoveClaimsAsync(VyprUser user, IEnumerable<Claim> claims)
        {
            return base.RemoveClaimsAsync(user, claims);
        }

        /// <summary>
        /// Removes the specified <paramref name="user" /> from the named role.
        /// </summary>
        /// <param name="user">The user to remove from the named role.</param>
        /// <param name="role">The name of the role to remove the user from.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> RemoveFromRoleAsync(VyprUser user, string role)
        {
            throw new NotImplementedException("Please use RemoveFromRoleAsync with tenant");
        }

        /// <summary>
        /// Removes the specified <paramref name="user" /> from the named roles.
        /// </summary>
        /// <param name="user">The user to remove from the named roles.</param>
        /// <param name="roles">The name of the roles to remove the user from.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> RemoveFromRolesAsync(VyprUser user, IEnumerable<string> roles)
        {
            throw new NotImplementedException("Please use RemoveFromRolesAsync with tenant");
        }

        /// <summary>
        /// Attempts to remove the provided external login information from the specified <paramref name="user" />.
        /// and returns a flag indicating whether the removal succeed or not.
        /// </summary>
        /// <param name="user">The user to remove the login information from.</param>
        /// <param name="loginProvider">The login provide whose information should be removed.</param>
        /// <param name="providerKey">The key given by the external login provider for the specified user.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> RemoveLoginAsync(VyprUser user, string loginProvider, string providerKey)
        {
            return base.RemoveLoginAsync(user, loginProvider, providerKey);
        }

        /// <summary>
        /// Removes a user's password.
        /// </summary>
        /// <param name="user">The user whose password should be removed.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> RemovePasswordAsync(VyprUser user)
        {
            return base.RemovePasswordAsync(user);
        }

        /// <summary>
        /// Replaces the given <paramref name="claim" /> on the specified <paramref name="user" /> with the <paramref name="newClaim" />
        /// </summary>
        /// <param name="user">The user to replace the claim on.</param>
        /// <param name="claim">The claim to replace.</param>
        /// <param name="newClaim">The new claim to replace the existing <paramref name="claim" /> with.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> ReplaceClaimAsync(VyprUser user, Claim claim, Claim newClaim)
        {
            return base.ReplaceClaimAsync(user, claim, newClaim);
        }

        /// <summary>
        /// Resets the access failed count for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose failed access count should be reset.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the operation.
        /// </returns>
        public override Task<IdentityResult> ResetAccessFailedCountAsync(VyprUser user)
        {
            return base.ResetAccessFailedCountAsync(user);
        }

        /// <summary>
        /// Resets the authenticator key for the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Whether the user was successfully updated.
        /// </returns>
        public override Task<IdentityResult> ResetAuthenticatorKeyAsync(VyprUser user)
        {
            return base.ResetAuthenticatorKeyAsync(user);
        }

        /// <summary>
        /// Resets the <paramref name="user" />'s password to the specified <paramref name="newPassword" /> after
        /// validating the given password reset <paramref name="token" />.
        /// </summary>
        /// <param name="user">The user whose password should be reset.</param>
        /// <param name="token">The password reset token to verify.</param>
        /// <param name="newPassword">The new password to set if reset token verification succeeds.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> ResetPasswordAsync(VyprUser user, string token, string newPassword)
        {
            return base.ResetPasswordAsync(user, token, newPassword);
        }

        /// <summary>
        /// Sets an authentication token for a user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loginProvider">The authentication scheme for the provider the token is associated with.</param>
        /// <param name="tokenName">The name of the token.</param>
        /// <param name="tokenValue">The value of the token.</param>
        /// <returns>
        /// Whether the user was successfully updated.
        /// </returns>
        public override Task<IdentityResult> SetAuthenticationTokenAsync(VyprUser user, string loginProvider, string tokenName, string tokenValue)
        {
            return base.SetAuthenticationTokenAsync(user, loginProvider, tokenName, tokenValue);
        }

        /// <summary>
        /// Sets the <paramref name="email" /> address for a <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose email should be set.</param>
        /// <param name="email">The email to set.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> SetEmailAsync(VyprUser user, string email)
        {
            return base.SetEmailAsync(user, email);
        }

        /// <summary>
        /// Sets a flag indicating whether the specified <paramref name="user" /> is locked out,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose locked out status should be set.</param>
        /// <param name="enabled">Flag indicating whether the user is locked out or not.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the operation
        /// </returns>
        public override Task<IdentityResult> SetLockoutEnabledAsync(VyprUser user, bool enabled)
        {
            return base.SetLockoutEnabledAsync(user, enabled);
        }

        /// <summary>
        /// Locks out a user until the specified end date has passed. Setting a end date in the past immediately unlocks a user.
        /// </summary>
        /// <param name="user">The user whose lockout date should be set.</param>
        /// <param name="lockoutEnd">The <see cref="T:System.DateTimeOffset" /> after which the <paramref name="user" />'s lockout should end.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the operation.
        /// </returns>
        public override Task<IdentityResult> SetLockoutEndDateAsync(VyprUser user, DateTimeOffset? lockoutEnd)
        {
            return base.SetLockoutEndDateAsync(user, lockoutEnd);
        }

        /// <summary>
        /// Sets the phone number for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose phone number to set.</param>
        /// <param name="phoneNumber">The phone number to set.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> SetPhoneNumberAsync(VyprUser user, string phoneNumber)
        {
            return base.SetPhoneNumberAsync(user, phoneNumber);
        }

        /// <summary>
        /// Sets a flag indicating whether the specified <paramref name="user" /> has two factor authentication enabled or not,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose two factor authentication enabled status should be set.</param>
        /// <param name="enabled">A flag indicating whether the specified <paramref name="user" /> has two factor authentication enabled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the operation
        /// </returns>
        public override Task<IdentityResult> SetTwoFactorEnabledAsync(VyprUser user, bool enabled)
        {
            return base.SetTwoFactorEnabledAsync(user, enabled);
        }

        /// <summary>
        /// Sets the given <paramref name="userName" /> for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose name should be set.</param>
        /// <param name="userName">The user name to set.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task<IdentityResult> SetUserNameAsync(VyprUser user, string userName)
        {
            return base.SetUserNameAsync(user, userName);
        }

        /// <summary>
        /// Updates the specified <paramref name="user" /> in the backing store.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> UpdateAsync(VyprUser user)
        {
            return base.UpdateAsync(user);
        }

        /// <summary>
        /// Updates the normalized email for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose email address should be normalized and updated.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public override Task UpdateNormalizedEmailAsync(VyprUser user)
        {
            return base.UpdateNormalizedEmailAsync(user);
        }

        /// <summary>
        /// Updates the normalized user name for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose user name should be normalized and updated.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task UpdateNormalizedUserNameAsync(VyprUser user)
        {
            return base.UpdateNormalizedUserNameAsync(user);
        }

        /// <summary>
        /// Regenerates the security stamp for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose security stamp should be regenerated.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        /// <remarks>
        /// Regenerating a security stamp will sign out any saved login for the user.
        /// </remarks>
        public override Task<IdentityResult> UpdateSecurityStampAsync(VyprUser user)
        {
            return base.UpdateSecurityStampAsync(user);
        }

        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="user" />'s phone number change verification
        /// token is valid for the given <paramref name="phoneNumber" />.
        /// </summary>
        /// <param name="user">The user to validate the token against.</param>
        /// <param name="token">The telephone number change token to validate.</param>
        /// <param name="phoneNumber">The telephone number the token was generated for.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, returning true if the <paramref name="token" />
        /// is valid, otherwise false.
        /// </returns>
        public override Task<bool> VerifyChangePhoneNumberTokenAsync(VyprUser user, string token, string phoneNumber)
        {
            return base.VerifyChangePhoneNumberTokenAsync(user, token, phoneNumber);
        }

        /// <summary>
        /// Verifies the specified two factor authentication <paramref name="token" /> against the <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user the token is supposed to be for.</param>
        /// <param name="tokenProvider">The provider which will verify the token.</param>
        /// <param name="token">The token to verify.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents result of the asynchronous operation, true if the token is valid,
        /// otherwise false.
        /// </returns>
        public override Task<bool> VerifyTwoFactorTokenAsync(VyprUser user, string tokenProvider, string token)
        {
            return base.VerifyTwoFactorTokenAsync(user, tokenProvider, token);
        }

        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="token" /> is valid for
        /// the given <paramref name="user" /> and <paramref name="purpose" />.
        /// </summary>
        /// <param name="user">The user to validate the token against.</param>
        /// <param name="tokenProvider">The token provider used to generate the token.</param>
        /// <param name="purpose">The purpose the token should be generated for.</param>
        /// <param name="token">The token to validate</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, returning true if the <paramref name="token" />
        /// is valid, otherwise false.
        /// </returns>
        public override Task<bool> VerifyUserTokenAsync(VyprUser user, string tokenProvider, string purpose, string token)
        {
            return base.VerifyUserTokenAsync(user, tokenProvider, purpose, token);
        }

        /// <summary>
        /// Generate a new recovery code.
        /// </summary>
        /// <returns></returns>
        protected override string CreateTwoFactorRecoveryCode()
        {
            return base.CreateTwoFactorRecoveryCode();
        }

        /// <summary>
        /// Releases the unmanaged resources used by the role manager and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// Updates a user's password hash.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="validatePassword">Whether to validate the password.</param>
        /// <returns>
        /// Whether the password has was successfully updated.
        /// </returns>
        protected override Task<IdentityResult> UpdatePasswordHash(VyprUser user, string newPassword, bool validatePassword)
        {
            return base.UpdatePasswordHash(user, newPassword, validatePassword);
        }

        /// <summary>
        /// Called to update the user after validating and updating the normalized email/user name.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Whether the operation was successful.
        /// </returns>
        protected override Task<IdentityResult> UpdateUserAsync(VyprUser user)
        {
            return base.UpdateUserAsync(user);
        }

        /// <summary>
        /// Returns a <see cref="T:Microsoft.AspNetCore.Identity.PasswordVerificationResult" /> indicating the result of a password hash comparison.
        /// </summary>
        /// <param name="store">The store containing a user's password.</param>
        /// <param name="user">The user whose password should be verified.</param>
        /// <param name="password">The password to verify.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.PasswordVerificationResult" />
        /// of the operation.
        /// </returns>
        protected override Task<PasswordVerificationResult> VerifyPasswordAsync(IUserPasswordStore<VyprUser> store, VyprUser user, string password)
        {
            return base.VerifyPasswordAsync(store, user, password);
        }

        /// <summary>
        /// Gets the user store.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Unable to cast user role store</exception>
        private VyprUserStore GetUserStore()
        {
            var cast = Store as VyprUserStore;
            if (cast == null)
            {
                throw new NotSupportedException("Unable to cast user role store");
            }
            return cast;
        }

        // authorise filter change to check tenant => Connor
        // user active checks => HP
        // usermanager why did we have to use string method and not int => int

        /// <summary>
        /// Checks the new user token valid.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="registrationToken">The registration token.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">user</exception>
        public async Task<bool> CheckNewUserTokenValid(VyprUser user, string registrationToken)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Check the token and expiry date for that Vypr user matches
            // those being passed in.
            var result = Task.Run(() => user.RegistrationTokenExpiry > DateTime.UtcNow
                   && user.RegistrationToken.Equals(registrationToken));

            return await result;
        }

        /// <summary>
        /// Creates the new user registration token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="registrationToken">The registration token.</param>
        /// <returns></returns>
        public async Task<VyprUser> CreateNewUserRegistrationToken(VyprUser user, string registrationToken = null)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // if user already has a token but expires. Allow re-use of the same token so
            // they can use any of their registration emails but still extend the expiry time.
            if (string.IsNullOrEmpty(registrationToken))
            {
                user.RegistrationToken = Guid.NewGuid().ToString();
            }

            // TODO: HP make this a setting
            user.RegistrationTokenExpiry = DateTime.UtcNow.AddDays(30);

            await GetUserStore().UpdateAsync(user);

            return user;
        }

        /// <summary>
        /// Sends the invitation link.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="UserInactiveException"></exception>
        /// <exception cref="NoTenantFoundException"></exception>
        /// <exception cref="ArgumentNullException">user</exception>
        public async Task SendInvitationLink(VyprUser user)
        {
            ThrowIfDisposed();
            ThrowIfNull(user);

            if (!user.Active)
            {
                throw new UserInactiveException();
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (!await CheckNewUserTokenValid(user, user.RegistrationToken))
            {
                user = await CreateNewUserRegistrationToken(user);
            }

            var link = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/invite?inv_token={HttpUtility.UrlEncode(user.RegistrationToken)}&user_id={user.Email}";

            var emailConfig = ((EmailTemplater)_emailTemplater).EmailConfig;
            var emailMessage = string.Format(emailConfig.RegistrationEmailText, link);

            var emailModel = EmailTemplateModel.Create(
                title: emailConfig.RegistrationEmailTitle,
                message: emailMessage, fullName: $"{user.FirstName} {user.LastName}");

            await _emailTemplater.TemplateAndSendEmail(user.Email, emailConfig.RegistrationEmailTitle, emailModel);
        }

        private static void ThrowIfNull(params object[] objects)
        {
            if (objects.Any(x => x is null))
            {
                throw new ArgumentNullException(nameof(objects));
            }
        }
    }
}