// <copyright file="WebAuthn.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models
{
    using Fido2NetLib;
    using Fido2NetLib.Objects;
    using System.Text;

    /// <summary>
    /// Wrapper around the Fido object.
    /// </summary>
    /// <seealso cref="Fido2NetLib.Fido2" />
    public class WebAuthn : Fido2
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebAuthn"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public WebAuthn(Fido2Configuration config) : base(config)
        {
        }

        /// <summary>
        /// Default extensions.
        /// </summary>
        /// <returns></returns>
        public static AuthenticationExtensionsClientInputs DefaultExtensions()
        {
            return new AuthenticationExtensionsClientInputs { Extensions = true, UserVerificationIndex = true, Location = true, UserVerificationMethod = true, BiometricAuthenticatorPerformanceBounds = new AuthenticatorBiometricPerfBounds { FAR = float.MaxValue, FRR = float.MaxValue } };
        }

        /// <summary>
        /// Default auth selection.
        /// </summary>
        /// <returns></returns>
        public static AuthenticatorSelection DefaultAuthSelection()
        {
            return new AuthenticatorSelection { RequireResidentKey = false, UserVerification = UserVerificationRequirement.Required, AuthenticatorAttachment = AuthenticatorAttachment.Platform }; // No USB Keys, Windows Hello, Yubi-Keys etc. Only fingerprints.
        }

        /// <summary>
        /// Creates a new <see cref="Fido2" /> user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>new Fido2 user.</returns>
        public static Fido2User CreateWebAuthnUser(string name, string displayName, int userId)
        {
            return new Fido2User
            {
                DisplayName = displayName,
                Name = name,
                Id = Encoding.UTF8.GetBytes(displayName),
            };
        }
    }
}
