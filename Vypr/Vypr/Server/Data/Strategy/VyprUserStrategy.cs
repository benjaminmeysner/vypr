// <copyright file="UserStrategy.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Data.Strategy
{
    using AutoMapper;
    using Vypr.Server.Data.Repository;
    using VyprCore.Interfaces.Context;
    using VyprCore.Interfaces.Logging;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// User strategy.
    /// </summary>
    public class VyprUserStrategy : BaseStrategy<VyprUser, VyprUserViewModel, VyprUserRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprUserStrategy" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public VyprUserStrategy(IRepository<VyprUser> repository, IMapper mapper, IVyprLogger logger, IApplicationContext<VyprUser> context) : base(repository, mapper, logger, context)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="VyprUserStrategy" /> class.</summary>
        public VyprUserStrategy()
        {
        }
    }
}
