// <copyright file="VyprSignInManager.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Authentication.Managers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using VyprCore.Interfaces.Context;
    using VyprCore.Models.Domain;
    using VyprCore.Models.Resources;
    using VyprCore.Utilities.Exceptions;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr Signin Manager
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.SignInManager{Vypr.Server.Authentication.Classes.VyprUser}" />
    public class VyprSignInManager : SignInManager<VyprUser>
    {
        /// <summary>
        /// Gets or sets the application context.
        /// </summary>
        /// <value>
        /// The application context.
        /// </value>
        private IApplicationContext<VyprUser> ApplicationContext { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprSignInManager"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="httpConextAccessor">The HTTP conext accessor.</param>
        /// <param name="userClaimsPrincipalFactory">The user claims principal factory.</param>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="authenticationSchemeProvider">The authentication scheme provider.</param>
        /// <param name="userConfirmation">The user confirmation.</param>
        public VyprSignInManager(VyprUserManager userManager, IHttpContextAccessor httpConextAccessor, IUserClaimsPrincipalFactory<VyprUser> userClaimsPrincipalFactory, IOptions<IdentityOptions> options, ILogger<SignInManager<VyprUser>> logger, IAuthenticationSchemeProvider authenticationSchemeProvider, IUserConfirmation<VyprUser> userConfirmation, IApplicationContext<VyprUser> applicationContext) : base(userManager, httpConextAccessor, userClaimsPrincipalFactory, options, logger, authenticationSchemeProvider, userConfirmation)
        {
            ApplicationContext = applicationContext;
        }

        /// <summary>
        /// Gets the <see cref="T:Microsoft.Extensions.Logging.ILogger" /> used to log messages from the manager.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> used to log messages from the manager.
        /// </value>
        public override ILogger Logger { get => base.Logger; set => base.Logger = value; }

        /// <summary>
        /// Returns a flag indicating whether the specified user can sign in at the current tenant.
        /// </summary>
        /// <param name="user">The user whose sign-in status should be returned.</param>
        /// <returns>
        /// The task object representing the asynchronous operation, containing a flag that is true
        /// if the specified user can sign-in, otherwise false.
        /// </returns>
        public override async Task<bool> CanSignInAsync(VyprUser user)
        {
            if (!user.Active)
            {
                throw new UserInactiveException($"{StandardText.UserInactive}");
            }
            
            return await base.CanSignInAsync(user);
        }

        /// <summary>
        /// Attempts a password sign in for a user.
        /// </summary>
        /// <param name="user">The user to sign in.</param>
        /// <param name="password">The password to attempt to sign in with.</param>
        /// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
        /// <returns>
        /// The task object representing the asynchronous operation containing the <see name="SignInResult" />
        /// for the sign-in attempt.
        /// </returns>
        public override Task<SignInResult> CheckPasswordSignInAsync(VyprUser user, string password, bool lockoutOnFailure)
        {
            return base.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }

        /// <summary>
        /// Configures the redirect URL and user identifier for the specified external login <paramref name="provider" />.
        /// </summary>
        /// <param name="provider">The provider to configure.</param>
        /// <param name="redirectUrl">The external login URL users should be redirected to during the login flow.</param>
        /// <param name="userId">The current user's identifier, which will be used to provide CSRF protection.</param>
        /// <returns>
        /// A configured <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationProperties" />.
        /// </returns>
        public override AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null)
        {
            return base.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);
        }

        /// <summary>
        /// Creates a <see cref="T:System.Security.Claims.ClaimsPrincipal" /> for the specified <paramref name="user" />, as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user to create a <see cref="T:System.Security.Claims.ClaimsPrincipal" /> for.</param>
        /// <returns>
        /// The task object representing the asynchronous operation, containing the ClaimsPrincipal for the specified user.
        /// </returns>
        public override Task<ClaimsPrincipal> CreateUserPrincipalAsync(VyprUser user)
        {
            return base.CreateUserPrincipalAsync(user);
        }

        /// <summary>
        /// Signs in a user via a previously registered third party login, as an asynchronous operation.
        /// </summary>
        /// <param name="loginProvider">The login provider to use.</param>
        /// <param name="providerKey">The unique provider identifier for the user.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <returns>
        /// The task object representing the asynchronous operation containing the <see name="SignInResult" />
        /// for the sign-in attempt.
        /// </returns>
        public override Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent)
        {
            return base.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent);
        }

        /// <summary>
        /// Signs in a user via a previously registered third party login, as an asynchronous operation.
        /// </summary>
        /// <param name="loginProvider">The login provider to use.</param>
        /// <param name="providerKey">The unique provider identifier for the user.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="bypassTwoFactor">Flag indicating whether to bypass two factor authentication.</param>
        /// <returns>
        /// The task object representing the asynchronous operation containing the <see name="SignInResult" />
        /// for the sign-in attempt.
        /// </returns>
        public override Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor)
        {
            return base.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent, bypassTwoFactor);
        }

        /// <summary>
        /// Clears the "Remember this browser flag" from the current browser, as an asynchronous operation.
        /// </summary>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public override Task ForgetTwoFactorClientAsync()
        {
            return base.ForgetTwoFactorClientAsync();
        }

        /// <summary>
        /// Gets a collection of <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationScheme" />s for the known external login providers.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationScheme" />s for the known external login providers.
        /// </returns>
        public override Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return base.GetExternalAuthenticationSchemesAsync();
        }

        /// <summary>
        /// Gets the external login information for the current login, as an asynchronous operation.
        /// </summary>
        /// <param name="expectedXsrf">Flag indication whether a Cross Site Request Forgery token was expected in the current request.</param>
        /// <returns>
        /// The task object representing the asynchronous operation containing the <see name="ExternalLoginInfo" />
        /// for the sign-in attempt.
        /// </returns>
        public override Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null)
        {
            return base.GetExternalLoginInfoAsync(expectedXsrf);
        }

        /// <summary>
        /// Gets the <typeparamref name="TUser" /> for the current two factor authentication login, as an asynchronous operation.
        /// </summary>
        /// <returns>
        /// The task object representing the asynchronous operation containing the <typeparamref name="TUser" />
        /// for the sign-in attempt.
        /// </returns>
        public override Task<VyprUser> GetTwoFactorAuthenticationUserAsync()
        {
            return base.GetTwoFactorAuthenticationUserAsync();
        }

        /// <summary>
        /// Returns true if the principal has an identity with the application cookie identity
        /// </summary>
        /// <param name="principal">The <see cref="T:System.Security.Claims.ClaimsPrincipal" /> instance.</param>
        /// <returns>
        /// True if the user is logged in with identity.
        /// </returns>
        public override bool IsSignedIn(ClaimsPrincipal principal)
        {
            return base.IsSignedIn(principal);
        }

        /// <summary>
        /// Returns a flag indicating if the current client browser has been remembered by two factor authentication
        /// for the user attempting to login, as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user attempting to login.</param>
        /// <returns>
        /// The task object representing the asynchronous operation containing true if the browser has been remembered
        /// for the current user.
        /// </returns>
        public override Task<bool> IsTwoFactorClientRememberedAsync(VyprUser user)
        {
            return base.IsTwoFactorClientRememberedAsync(user);
        }

        /// <summary>
        /// Attempts to sign in the specified <paramref name="user" /> and <paramref name="password" /> combination
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user to sign in.</param>
        /// <param name="password">The password to attempt to sign in with.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
        /// <returns>
        /// The task object representing the asynchronous operation containing the <see name="SignInResult" />
        /// for the sign-in attempt.
        /// </returns>
        public override Task<SignInResult> PasswordSignInAsync(VyprUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        /// <summary>
        /// Attempts to sign in the specified <paramref name="userName" /> and <paramref name="password" /> combination
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="userName">The user name to sign in.</param>
        /// <param name="password">The password to attempt to sign in with.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
        /// <returns>
        /// The task object representing the asynchronous operation containing the <see name="SignInResult" />
        /// for the sign-in attempt.
        /// </returns>
        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        /// <summary>
        /// Signs in the specified <paramref name="user" />, whilst preserving the existing
        /// AuthenticationProperties of the current signed-in user like rememberMe, as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public override Task RefreshSignInAsync(VyprUser user)
        {
            return base.RefreshSignInAsync(user);
        }

        /// <summary>
        /// Sets a flag on the browser to indicate the user has selected "Remember this browser" for two factor authentication purposes,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user who choose "remember this browser".</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public override Task RememberTwoFactorClientAsync(VyprUser user)
        {
            return base.RememberTwoFactorClientAsync(user);
        }

        /// <summary>
        /// Signs in the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="authenticationMethod">Name of the method used to authenticate the user.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public override Task SignInAsync(VyprUser user, bool isPersistent, string authenticationMethod = null)
        {
            return base.SignInAsync(user, isPersistent, authenticationMethod);
        }

        /// <summary>
        /// Signs in the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="authenticationProperties">Properties applied to the login and authentication cookie.</param>
        /// <param name="authenticationMethod">Name of the method used to authenticate the user.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public override Task SignInAsync(VyprUser user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        {
            return base.SignInAsync(user, authenticationProperties, authenticationMethod);
        }

        /// <summary>
        /// Signs in the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="additionalClaims">Additional claims that will be stored in the cookie.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public override Task SignInWithClaimsAsync(VyprUser user, bool isPersistent, IEnumerable<Claim> additionalClaims)
        {
            return base.SignInWithClaimsAsync(user, isPersistent, additionalClaims);
        }

        /// <summary>
        /// Signs in the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="authenticationProperties">Properties applied to the login and authentication cookie.</param>
        /// <param name="additionalClaims">Additional claims that will be stored in the cookie.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public override Task SignInWithClaimsAsync(VyprUser user, AuthenticationProperties authenticationProperties, IEnumerable<Claim> additionalClaims)
        {
            return base.SignInWithClaimsAsync(user, authenticationProperties, additionalClaims);
        }

        /// <summary>
        /// Signs the current user out of the application.
        /// </summary>
        /// <returns></returns>
        public override Task SignOutAsync()
        {
            return base.SignOutAsync();
        }

        /// <summary>
        /// Validates the sign in code from an authenticator app and creates and signs in the user, as an asynchronous operation.
        /// </summary>
        /// <param name="code">The two factor authentication code to validate.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="rememberClient">Flag indicating whether the current browser should be remember, suppressing all further
        /// two factor authentication prompts.</param>
        /// <returns>
        /// The task object representing the asynchronous operation containing the <see name="SignInResult" />
        /// for the sign-in attempt.
        /// </returns>
        public override Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient)
        {
            return base.TwoFactorAuthenticatorSignInAsync(code, isPersistent, rememberClient);
        }

        /// <summary>
        /// Signs in the user without two factor authentication using a two factor recovery code.
        /// </summary>
        /// <param name="recoveryCode">The two factor recovery code.</param>
        /// <returns></returns>
        public override Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode)
        {
            return base.TwoFactorRecoveryCodeSignInAsync(recoveryCode);
        }

        /// <summary>
        /// Validates the two factor sign in code and creates and signs in the user, as an asynchronous operation.
        /// </summary>
        /// <param name="provider">The two factor authentication provider to validate the code against.</param>
        /// <param name="code">The two factor authentication code to validate.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="rememberClient">Flag indicating whether the current browser should be remember, suppressing all further
        /// two factor authentication prompts.</param>
        /// <returns>
        /// The task object representing the asynchronous operation containing the <see name="SignInResult" />
        /// for the sign-in attempt.
        /// </returns>
        public override Task<SignInResult> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberClient)
        {
            return base.TwoFactorSignInAsync(provider, code, isPersistent, rememberClient);
        }

        /// <summary>
        /// Stores any authentication tokens found in the external authentication cookie into the associated user.
        /// </summary>
        /// <param name="externalLogin">The information from the external login provider.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the operation.
        /// </returns>
        public override Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo externalLogin)
        {
            return base.UpdateExternalAuthenticationTokensAsync(externalLogin);
        }

        /// <summary>
        /// Validates the security stamp for the specified <paramref name="principal" /> against
        /// the persisted stamp for the current user, as an asynchronous operation.
        /// </summary>
        /// <param name="principal">The principal whose stamp should be validated.</param>
        /// <returns>
        /// The task object representing the asynchronous operation. The task will contain the <typeparamref name="TUser" />
        /// if the stamp matches the persisted value, otherwise it will return false.
        /// </returns>
        public override Task<VyprUser> ValidateSecurityStampAsync(ClaimsPrincipal principal)
        {
            return base.ValidateSecurityStampAsync(principal);
        }

        /// <summary>
        /// Validates the security stamp for the specified <paramref name="user" />.  If no user is specified, or if the store
        /// does not support security stamps, validation is considered successful.
        /// </summary>
        /// <param name="user">The user whose stamp should be validated.</param>
        /// <param name="securityStamp">The expected security stamp value.</param>
        /// <returns>
        /// The result of the validation.
        /// </returns>
        public override Task<bool> ValidateSecurityStampAsync(VyprUser user, string securityStamp)
        {
            return base.ValidateSecurityStampAsync(user, securityStamp);
        }

        /// <summary>
        /// Validates the security stamp for the specified <paramref name="principal" /> from one of
        /// the two factor principals (remember client or user id) against
        /// the persisted stamp for the current user, as an asynchronous operation.
        /// </summary>
        /// <param name="principal">The principal whose stamp should be validated.</param>
        /// <returns>
        /// The task object representing the asynchronous operation. The task will contain the <typeparamref name="TUser" />
        /// if the stamp matches the persisted value, otherwise it will return false.
        /// </returns>
        public override Task<VyprUser> ValidateTwoFactorSecurityStampAsync(ClaimsPrincipal principal)
        {
            return base.ValidateTwoFactorSecurityStampAsync(principal);
        }

        /// <summary>
        /// Used to determine if a user is considered locked out.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Whether a user is considered locked out.
        /// </returns>
        protected override Task<bool> IsLockedOut(VyprUser user)
        {
            return base.IsLockedOut(user);
        }

        /// <summary>
        /// Returns a locked out SignInResult.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// A locked out SignInResult
        /// </returns>
        protected override Task<SignInResult> LockedOut(VyprUser user)
        {
            return base.LockedOut(user);
        }

        /// <summary>
        /// Used to ensure that a user is allowed to sign in.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>
        /// Null if the user should be allowed to sign in, otherwise the SignInResult why they should be denied.
        /// </returns>
        protected override Task<SignInResult> PreSignInCheck(VyprUser user)
        {
            // this will call can sign in
            return base.PreSignInCheck(user);
        }

        /// <summary>
        /// Used to reset a user's lockout count.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the operation.
        /// </returns>
        protected override Task ResetLockout(VyprUser user)
        {
            return base.ResetLockout(user);
        }

        /// <summary>
        /// Signs in the specified <paramref name="user" /> if <paramref name="bypassTwoFactor" /> is set to false.
        /// Otherwise stores the <paramref name="user" /> for use after a two factor check.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="loginProvider">The login provider to use. Default is null</param>
        /// <param name="bypassTwoFactor">Flag indicating whether to bypass two factor authentication. Default is false</param>
        /// <returns>
        /// Returns a <see cref="T:Microsoft.AspNetCore.Identity.SignInResult" />
        /// </returns>
        protected override Task<SignInResult> SignInOrTwoFactorAsync(VyprUser user, bool isPersistent, string loginProvider = null, bool bypassTwoFactor = false)
        {
            return base.SignInOrTwoFactorAsync(user, isPersistent, loginProvider, bypassTwoFactor);
        }
    }
}
