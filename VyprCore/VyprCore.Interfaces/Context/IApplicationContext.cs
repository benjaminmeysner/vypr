// <copyright file="IApplicationContext.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Context
{
    using System.Threading.Tasks;

    /// <summary>
    /// IApplicationContext
    /// </summary>
    public interface IApplicationContext<TVyprUser>
    {
        /// <summary>
        /// Gets the Vypr user.
        /// </summary>
        /// <returns></returns>
        public Task<TVyprUser> UserAsync();
    }
}
