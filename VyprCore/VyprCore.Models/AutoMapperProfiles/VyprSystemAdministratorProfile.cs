// <copyright file="SystemAdministratorProfile.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.AutoMapperProfiles
{
    using AutoMapper;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// System admin profile.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class VyprSystemAdministratorProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprSystemAdministratorProfile"/> class.
        /// </summary>
        public VyprSystemAdministratorProfile()
        {
            CreateMap<VyprSystemAdministrator, VyprSystemAdministratorViewModel>().ReverseMap();
        }
    }
}
