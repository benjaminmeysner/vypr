// <copyright file="SystemAdministratorStrategy.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Authorisation.Strategy
{
    using AutoMapper;
    using Vypr.Server.Data.Repository;
    using Vypr.Server.Data.Strategy;
    using VyprCore.Interfaces.Context;
    using VyprCore.Interfaces.Logging;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// System Administrator Strategy.
    /// </summary>
    public class VyprSystemAdministratorStrategy : BaseStrategy<VyprSystemAdministrator, VyprSystemAdministratorViewModel, VyprSystemAdministratorRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprSystemAdministratorStrategy" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public VyprSystemAdministratorStrategy(IRepository<VyprSystemAdministrator> repository, IMapper mapper, IVyprLogger logger, IApplicationContext<VyprUser> context) : base(repository, mapper, logger, context)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="VyprSystemAdministratorStrategy" /> class.</summary>
        public VyprSystemAdministratorStrategy()
        {
        }
    }
}
