// <copyright file="VyprUserProfile.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.AutoMapperProfiles
{
    using AutoMapper;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Tenant Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class VyprUserProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprUserProfile"/> class.
        /// </summary>
        public VyprUserProfile()
        {
            CreateMap<VyprUser, VyprUserViewModel>()
                .ForMember(vm => vm.IsSystemAdministrator, y => y.MapFrom(u => u.SystemAdministrator != null));
            CreateMap<VyprUserViewModel, VyprUser>()
                .ForMember(u => u.SystemAdministrator, y => y.Ignore());
        }
    }
}
