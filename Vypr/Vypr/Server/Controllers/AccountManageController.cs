// <copyright file="BaseAccountManageController.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using VyprCore.Models.Domain;
    using VyprCore.Models.Resources;
    using VyprCore.Models.ViewModels;
    using VyprCore.Utilities.Helpers;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Base account controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [Route("account/manage/[action]")]
    public class AccountManageController : ControllerBase
    {
        private readonly UserManager<VyprUser> _userManager;
        private readonly SignInManager<VyprUser> _signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManageController"/> class.
        /// </summary>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="userManager">The user manager.</param>
        public AccountManageController(SignInManager<VyprUser> signInManager, UserManager<VyprUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordViewModel model)
        {
            ThrowIfNull(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest($"{StandardText.UnableToFindUserWithId} '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                return BadRequest(model);
            }

            await _signInManager.RefreshSignInAsync(user);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetPasswordAsync([FromBody] SetPasswordViewModel model)
        {
            ThrowIfNull(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"{StandardText.UnableToFindUserWithId} '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.Password);
            if (!addPasswordResult.Succeeded)
            {
                return BadRequest(model);
            }

            await _signInManager.RefreshSignInAsync(user);

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> HasPasswordAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            ThrowIfNull(user);

            var hasPassword = await _userManager.HasPasswordAsync(user);

            return Ok(hasPassword);
        }

        [HttpGet]
        public async Task<IActionResult> GetProfileDisplayAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            ThrowIfNull(user);

            var hashBytes = System.Security.Cryptography.MD5.Create()
                    .ComputeHash(System.Text.Encoding.ASCII.GetBytes(user.Email != null ? user.Email : ""));

            var profileDisplayModel = new ProfileDisplayViewModel
            {
                UserName = user.UserName,
                UserNameHash = string.Join("", Enumerable.Range(0, hashBytes.Length).Select(i => hashBytes[i].ToString("x2"))),
                Initials = StringHelpers.CreateUserInitials(user.FirstName, user.LastName),
            };

            return Ok(profileDisplayModel);
        }

        [HttpPost]
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        private static void ThrowIfNull(params object[] objects)
        {
            if (objects.Any(x => x is null))
            {
                throw new VyprCore.Utilities.Exceptions.NullModelException(nameof(objects));
            }
        }
    }
}
