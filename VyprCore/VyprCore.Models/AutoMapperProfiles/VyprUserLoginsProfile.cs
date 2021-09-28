// <copyright file="VyprUserLoginsProfile.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.AutoMapperProfiles
{
    using AutoMapper;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// User Logins Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class VyprUserLoginsProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprUserLoginsProfile"/> class.
        /// </summary>
        public VyprUserLoginsProfile()
        {
            CreateMap<VyprUserLogin, VyprUserLoginViewModel>().ReverseMap();
        }
    }
}
