// <copyright file="SystemAdministrator.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Domain
{
    using VyprCore.Interfaces.Authorization;
    using VyprCore.Interfaces.Entity;

    /// <summary>
    /// System Administrator Role Type
    /// </summary>
    /// <seealso cref="VyprCore.Interfaces.Authorization.IRoleType" />
    public class VyprSystemAdministrator : IRoleType, IEntity
    {
        public int Rank => 1;

        public int? UserId { get; set; }

        public int Id => UserId.Value;
    }
}
