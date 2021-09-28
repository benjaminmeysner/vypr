// <copyright file="FidoCredentialOptionsViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using Fido2NetLib;

    /// <summary>
    /// ViewModel representing a Fido Credential options instance.
    /// </summary>
    public class WebAuthnCreateCredentialOptionsViewModel : CredentialCreateOptions
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string ChallengeSerialised { get; set; }

        /// <summary>
        /// User id.
        /// </summary>
        public int UserId { get; set; }
    }
}
