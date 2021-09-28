// <copyright file="WebAuthnCreateCredentialViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using Fido2NetLib;

    /// <summary>
    /// ViewModel representing a Fido Credential options instance.
    /// </summary>
    public class WebAuthnCreateCredentialViewModel
    {
        /// <summary>
        ///  [WebAuthnCreateCredentialViewModel]
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="response"></param>
        public WebAuthnCreateCredentialViewModel(string userName, string password, AuthenticatorAttestationRawResponse deviceResponse, CredentialCreateOptions createCredentialOptions)
        {
            UserName = userName;
            Password = password;
            DeviceResponse = deviceResponse;
            CreateCredentialOptions = createCredentialOptions;
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public AuthenticatorAttestationRawResponse DeviceResponse { get; set; }

        public CredentialCreateOptions CreateCredentialOptions { get; set; }
    }
}
