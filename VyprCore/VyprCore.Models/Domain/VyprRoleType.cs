// <copyright file="VyprRoleType.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Domain
{
    using VyprCore.Models.Models;

    /// <summary>
    /// VyprRoleType Enumeration class - allows for extensible "enum types"
    /// </summary>
    /// <seealso cref="VyprCore.Models.Models.Enumeration" />
    /// <remarks>
    /// Consider reading this article: https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/
    /// Also consider reading this Stack Overflow thread: https://stackoverflow.com/a/61771238
    /// </remarks>
    public class VyprRoleType : Enumeration<VyprRoleType>
    {
        /// <summary>
        /// Gets the rank.
        /// </summary>
        /// <value>
        /// The rank. Equal to Id.
        /// </value>
        public int Rank => Id;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprRoleType"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        public VyprRoleType(int id, string name) : base(id, name) { }

        /// <summary>
        /// Gets the system administrator.
        /// </summary>
        /// <value>
        /// The system administrator.
        /// </value>
        public static VyprRoleType SystemAdministrator { get; } = new(1, VyprNames.SystemAdministrator);

        /// <summary>
        /// Names of role types.
        /// </summary>
        public static class VyprNames
        {
            /// <summary>
            /// The system administrator.
            /// </summary>
            public const string SystemAdministrator = "System Administrator";
        }
    }
}
