// <copyright file="UserLoginRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Data.Repository
{
    using Vypr.Server.Data;
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