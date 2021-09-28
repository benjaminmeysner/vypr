// <copyright file="FidoRepository.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Data.Repository
{
    using Vypr.Server.Data;
    using VyprCore.Models.Domain;

    /// <summary>
    /// FidoIdentity Repository class.
    /// </summary>
    public class WebAuthnRepository : BaseRepository<VyprWebAuthnCredential, VyprDbContext>
    {
        /// <summary>Initializes a new instance of the <see cref="FidoRepository" /> class.</summary>
        /// <param name="context">The context.</param>
        public WebAuthnRepository(VyprDbContext context) : base(context)
        {
        }
    }
}