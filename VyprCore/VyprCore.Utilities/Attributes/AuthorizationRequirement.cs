// <copyright file="VyprAuthorizationRequirement.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Attributes
{
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// Role Authorization Requirement
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.IAuthorizationRequirement" />
    public class AuthorizationRequirement : IAuthorizationRequirement
    {
    }
}
