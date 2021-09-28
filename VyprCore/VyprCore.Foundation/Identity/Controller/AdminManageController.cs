// <copyright file="AdminManageController.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Identity.Controller
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VyprCore.Foundation.Authentication.Managers;
    using VyprCore.Models.Models;
    using VyprCore.Models.Resources;
    using VyprCore.Models.ViewModels;
    using VyprCore.Utilities.Exceptions;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Base account controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route("admin/[action]")]
    public class AdminManageController : ControllerBase
    {
        private readonly VyprUserManager _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminManageController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public AdminManageController(VyprUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Sends the user invite asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Utilities.Attributes.Authorize(VyprClaims.User_SendInvitation)]
        public async Task<IActionResult> SendUserInviteAsync([FromBody] SendUserInviteViewModel model)
        {
            ThrowIfNull(model, model.UserId);

            var user = await _userManager.FindByIdAsync(model.UserId.Value);
            if (user is null)
            {
                return BadRequest($"{StandardText.UnableToFindUserWithId} '{_userManager.GetUserId(User)}'.");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                return BadRequest(StandardText.NoUserEmailExists);
            }

            try
            {
                await _userManager.SendInvitationLink(user);
            }
            catch (UserInactiveException)
            {
                return BadRequest($"{StandardText.UserInactive}");
            }
            catch (NoTenantFoundException)
            {
                return BadRequest($"{StandardText.NoUserTenantExists}");
            }

            return Ok();
        }

        /// <summary>
        /// Throws if null.
        /// </summary>
        /// <param name="objects">The objects.</param>
        /// <exception cref="Utilities.Exceptions.NullModelException">objects</exception>
        private static void ThrowIfNull(params object[] objects)
        {
            if (objects.Any(x => x is null))
            {
                throw new Utilities.Exceptions.NullModelException(nameof(objects));
            }
        }
    }
}
