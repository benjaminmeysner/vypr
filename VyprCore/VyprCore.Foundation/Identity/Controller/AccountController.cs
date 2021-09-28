// <copyright file="BaseAccountController.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Identity.Controller
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using VyprCore.Foundation.Authentication.Managers;
    using VyprCore.Foundation.Authentication.Senders;
    using VyprCore.Interfaces.Context;
    using VyprCore.Interfaces.Email;
    using VyprCore.Models.Domain;
    using VyprCore.Models.Resources;
    using VyprCore.Models.ViewModels;
    using VyprCore.Utilities.Exceptions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using System.Web;

    /// <summary>
    /// Base account controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IApplicationContext<VyprUser> _applicationContext;
        private readonly IEmailSender _emailSender;
        private readonly VyprRoleManager _roleManager;
        private readonly VyprSignInManager _signInManager;
        private readonly VyprUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailTemplater _emailTemplater;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="userManager">The user manager.</param>
        public AccountController(VyprSignInManager signInManager, VyprUserManager userManager, VyprRoleManager roleManager, IApplicationContext<VyprUser> applicationContext, IEmailSender emailSender, IEmailTemplater emailTemplater, IHttpContextAccessor httpContextAccessor)
        {
            _applicationContext = applicationContext;
            _emailSender = emailSender;
            _emailTemplater = emailTemplater;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost, ActionName("loginwp")]
        public virtual async Task<IActionResult> LoginWithPassword([FromBody] LoginWithPasswordViewModel model)
        {
            ThrowIfNull(model);

            try
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Ok(model);
                }
                else
                {
                    // We could show extra cases, locked out, email confirmed etc here.
                    return Unauthorized($"{StandardText.InvalidLoginAttempt}");
                }
            }
            catch (NoTenantFoundException)
            {
                return NotFound($"{StandardText.NoTenantExist}");
            }
        }

        /// <summary>
        /// Gets the claims asynchronous.
        /// </summary>
        /// <returns>Ok and a User with modified claims, or Bad Request if an error occurs.</returns>
        [HttpGet(Name = "getuser")]
        public virtual async Task<IActionResult> GetUserAsync()
        {
            var user = HttpContext.User;
            var claims = new List<KeyValuePair<string, string>>();

            try
            {
                var vyprUser = await _userManager.GetUserAsync(user);
                if (vyprUser == null || (user != null && !user.Identity.IsAuthenticated))
                {
                    return Ok(claims);
                }

                var userClaims = await _userManager.GetClaimsAsync(vyprUser);
                var roleTypes = await _userManager.GetRolesAsync(vyprUser);

                claims.AddRange(roleTypes.Select(r => KeyValuePair.Create("role", r)));
                claims.AddRange(userClaims.Select(uc => KeyValuePair.Create(uc.Type, uc.Value)));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(claims);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            ThrowIfNull(model);

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                // This is a security descision, instead just notify that an email
                // has been sent out.
                return Ok(model);
            }

            // Generate the web token/url in which to embed in the email.
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/resetpassword?reset_code={HttpUtility.UrlEncode(code)}&usr={user.Email}";

            var emailConfig = ((EmailTemplater)_emailTemplater).EmailConfig;
            var emailTitle = "Reset Password";
            //var emailMessage = string.Format(emailConfig.RegistrationEmailText, link);

            var emailModel = EmailTemplateModel.Create(
                title: emailTitle,
                message: $"Please reset your password by clicking <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>here</a>.", fullName: $"{user.FirstName} {user.LastName}");

            await _emailTemplater.TemplateAndSendEmail(user.Email, emailTitle, emailModel);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            ThrowIfNull(model);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.ResetCode, model.Password);

            return YesNoIdentityResponse(result, model);
        }

        [HttpPost]
        public async Task<IActionResult> InviteCreatePassword([FromBody] InvitationalCreatePasswordViewModel model)
        {
            ThrowIfNull(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Ok(model);
            }

            // Check that the user token is valid before continuing
            if (!await _userManager.CheckNewUserTokenValid(user, model.InviteToken))
            {
                return BadRequest(model);
            }

            if (!await _userManager.HasPasswordAsync(user))
            {
                var result = await _userManager.AddPasswordAsync(user, model.Password);

                return YesNoIdentityResponse(result, model);
            }
            // Let the user change their password if they already have one,
            // They have confirmed their identity with the above token.
            else
            {
                var changeToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, changeToken, model.Password);

                return YesNoIdentityResponse(result, model);
            }
        }

        private static void ThrowIfNull(params object[] objects)
        {
            if (objects.Any(x => x is null))
            {
                throw new Utilities.Exceptions.NullModelException(nameof(objects));
            }
        }

        private IActionResult YesNoIdentityResponse(IdentityResult result, object model)
        {
            if (result.Succeeded)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest(model);
            }
        }
    }
}
