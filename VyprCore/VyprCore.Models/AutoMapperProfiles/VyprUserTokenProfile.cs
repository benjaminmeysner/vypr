// <copyright file="VyprUserTokenProfile.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.AutoMapperProfiles
{
    using AutoMapper;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// User Token Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class VyprUserTokenProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprUserTokenProfile"/> class.
        /// </summary>
        public VyprUserTokenProfile()
        {
            CreateMap<VyprUserToken, VyprUserTokenViewModel>().ReverseMap();
        }
    }
}
