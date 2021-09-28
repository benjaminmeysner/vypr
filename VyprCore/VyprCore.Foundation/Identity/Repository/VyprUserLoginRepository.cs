// <copyright file="UserLoginRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Identity.Repository
{
    using VyprCore.Foundation.BaseClasses.Repository;
    using VyprCore.Foundation.Context;
    using VyprCore.Models.Domain;

    /// <summary>
    /// User Logins Repository Class.
    /// </summary>
    public class VyprUserLoginRepository : BaseRepository<VyprUserLogin, VyprDbContext>
    {
        /// <summary>Initializes a new instance of the <see cref="VyprUserLoginRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public VyprUserLoginRepository(VyprDbContext context) : base(context)
        {
        }
    }
}