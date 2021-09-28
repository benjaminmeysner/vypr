// <copyright file="VyprRoleProfile.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.AutoMapperProfiles
{
    using AutoMapper;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Role Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class VyprRoleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprRoleProfile"/> class.
        /// </summary>
        public VyprRoleProfile()
        {
            CreateMap<VyprRole, VyprRoleViewModel>()
                .ForMember(vm => vm.RoleType, o => { 
                    o.PreCondition(r => r.RoleType != null);
                    o.MapFrom(r => r.RoleType.Rank);
                })
                .ForMember(vm => vm.RoleTypeName, o =>
                {
                    o.PreCondition(r => r.RoleType != null);
                    o.MapFrom(r => r.RoleType.Name);
                });

            CreateMap<VyprRoleViewModel, VyprRole>()
                .ForMember(r => r.RoleType, o => {
                    o.PreCondition(vm => vm.RoleType != null);
                    o.MapFrom(vm => VyprRoleType.GetById(vm.RoleType.Value));
                });
        }
    }
}
