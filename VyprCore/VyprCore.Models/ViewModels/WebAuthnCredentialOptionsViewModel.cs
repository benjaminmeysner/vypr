// <copyright file="FidoCredentialOptionsViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    /// <summary>
    /// ViewModel representing a Fido Credential options instance.
    /// </summary>
    public class WebAuthnCredentialOptionsViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the type of the attestation.
        /// </summary>
        /// <value>
        /// The type of the attestation.
        /// </value>
        public string AttestationType { get; set; }

        /// <summary>
        /// Gets or sets the type of the authentication.
        /// </summary>
        /// <value>
        /// The type of the authentication.
        /// </value>
        public string AuthType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [require resident key].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [require resident key]; otherwise, <c>false</c>.
        /// </value>
        public bool RequireResidentKey { get; set; }

        /// <summary>
        /// Gets or sets the user verification.
        /// </summary>
        /// <value>
        /// The user verification.
        /// </value>
        public string UserVerification { get; set; }
    }
}
