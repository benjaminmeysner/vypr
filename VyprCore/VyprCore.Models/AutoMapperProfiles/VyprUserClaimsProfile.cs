// <copyright file="VyprUserClaimsProfile.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.AutoMapperProfiles
{
    using AutoMapper;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// User Claims Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class VyprUserClaimsProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprUserClaimsProfile"/> class.
        /// </summary>
        public VyprUserClaimsProfile()
        {
            CreateMap<VyprUserClaim, VyprUserClaimsViewModel>().ReverseMap();
        }
    }
}
