// <copyright file="VyprCoreLogger.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.AutoMapperProfiles
{
    using AutoMapper;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Role claims profile.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class VyprRoleClaimsProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprRoleClaimsProfile"/> class.
        /// </summary>
        public VyprRoleClaimsProfile()
        {
            CreateMap<VyprRoleClaim, VyprRoleClaimsViewModel>().ReverseMap();
        }
    }
}
