// <copyright file="UserClaimsStrategy.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Identity.Strategy
{
    using AutoMapper;
    using VyprCore.Foundation.BaseClasses.Strategy;
    using VyprCore.Foundation.Identity.Repository;
    using VyprCore.Interfaces.Context;
    using VyprCore.Interfaces.Logging;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// User claims strategy.
    /// </summary>
    public class VyprUserClaimsStrategy : BaseStrategy<VyprUserClaim, VyprUserClaimsViewModel, VyprUserClaimsRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprUserClaimsStrategy" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public VyprUserClaimsStrategy(IRepository<VyprUserClaim> repository, IMapper mapper, IVyprLogger logger, IApplicationContext<VyprUser> context) : base(repository, mapper, logger, context)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="VyprUserClaimsStrategy" /> class.</summary>
        public VyprUserClaimsStrategy()
        {
        }
    }
}
