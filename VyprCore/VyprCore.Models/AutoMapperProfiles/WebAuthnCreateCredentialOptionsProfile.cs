// <copyright file="WebAuthnCreateCredentialOptionsProfile.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.AutoMapperProfiles
{
    using AutoMapper;
    using Fido2NetLib;
    using VyprCore.Models.ViewModels;
    using System;

    /// <summary>
    /// Web Authentication Credentials Profile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class WebAuthnCreateCredentialOptionsProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebAuthnCreateCredentialOptionsProfile"/> class.
        /// </summary>
        public WebAuthnCreateCredentialOptionsProfile()
        {
            CreateMap<CredentialCreateOptions, WebAuthnCreateCredentialOptionsViewModel>().ReverseMap();
        }
    }
}
