// <copyright file="SystemAdministratorRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Authorisation.Repository
{
    using VyprCore.Foundation.BaseClasses.Repository;
    using VyprCore.Foundation.Context;
    using VyprCore.Models.Domain;

    /// <summary>
    /// User Logins Repository Class.
    /// </summary>
    public class VyprSystemAdministratorRepository : BaseRepository<VyprSystemAdministrator, VyprDbContext>
    {
        /// <summary>Initializes a new instance of the <see cref="VyprSystemAdministratorRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public VyprSystemAdministratorRepository(VyprDbContext context) : base(context)
        {
        }
    }
}