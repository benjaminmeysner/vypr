// <copyright file="UserTokenStrategy.cs" company="Vypr Systems">
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
    /// User token Strategy.
    /// </summary>
    public class VyprUserTokenStrategy : BaseStrategy<VyprUserToken, VyprUserTokenViewModel, VyprUserTokenRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserTenantStrategy" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The context.</param>
        public VyprUserTokenStrategy(IRepository<VyprUserToken> repository, IMapper mapper, IVyprLogger logger, IApplicationContext<VyprUser> context) : base(repository, mapper, logger, context)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TenantStrategy" /> class.</summary>
        public VyprUserTokenStrategy()
        {
        }
    }
}
