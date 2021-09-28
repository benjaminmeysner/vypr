// <copyright file="VyprRoleProfile.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.AutoMapperProfiles
{
    using AutoMapper;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Vypr role type profile.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class VyprRoleTypeProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprRoleTypeProfile"/> class.
        /// </summary>
        public VyprRoleTypeProfile()
        {
            CreateMap<VyprRoleType, VyprRoleTypeViewModel>().ReverseMap();
        }
    }
}
