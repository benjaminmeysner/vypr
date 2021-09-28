// <copyright file="ApplicationContext.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Classes.Context
{
    using Microsoft.AspNetCore.Http;
    using Vypr.Server.Authentication.Managers;
    using System.Threading.Tasks;
    using VyprCore.Interfaces.Context;
    using VyprCore.Models.Domain;

    /// <summary>
    /// Application context.
    /// </summary>
    public class ApplicationContext : IApplicationContext<VyprUser>
    {
        private readonly VyprUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Application context.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public ApplicationContext(VyprUserManager userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the Vypr user.
        /// </summary>
        /// <returns></returns>
        public async Task<VyprUser> UserAsync()
        {
            var contextUser = _httpContextAccessor.HttpContext.User;
            return await _userManager.GetUserAsync(contextUser);
        }
    }
}
