// <copyright file="UserRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Data.Repository
{
    using Vypr.Server.Data;
    using VyprCore.Models.Domain;

    /// <summary>
    /// User Repository Class.
    /// </summary>
    public class VyprUserRepository : BaseRepository<VyprUser, VyprDbContext>
    {
        /// <summary>Initializes a new instance of the <see cref="VyprUserRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public VyprUserRepository(VyprDbContext context) : base(context)
        {
        }
    }
}