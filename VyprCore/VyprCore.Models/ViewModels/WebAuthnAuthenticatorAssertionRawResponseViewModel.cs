// <copyright file="FidoCredentialOptionsViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using Fido2NetLib;

    /// <summary>
    /// ViewModel representing a Fido Credential options instance.
    /// </summary>
    public class WebAuthnAuthenticatorAssertionRawResponseViewModel
    {
        public AuthenticatorAssertionRawResponse Response { get; set; }

        public AssertionOptions AssertionOptions { get; set; }
    }
}
