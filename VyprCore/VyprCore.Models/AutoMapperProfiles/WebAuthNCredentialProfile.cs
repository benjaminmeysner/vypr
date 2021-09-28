// <copyright file="WebAuthnCredentialProfile.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.AutoMapperProfiles
{
    using AutoMapper;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Web Authentication Credentials Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class WebAuthnCredentialProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebAuthnCredentialProfile"/> class.
        /// </summary>
        public WebAuthnCredentialProfile()
        {
            CreateMap<VyprWebAuthnCredential, WebAuthnCredentialViewModel>().ReverseMap();
        }
    }
}
