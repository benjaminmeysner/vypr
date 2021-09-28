// <copyright file="FidoConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Settings
{
    /// <summary>
    /// Fido/WebAuthn configuration model.
    /// </summary>
    public class WebAuthnConfig
    {
        /// <summary>
        /// A human friendly name of the RP
        /// </summary>
        /// <value>
        /// The name of the server.
        /// </value>
        public string ServerName { get; set; }

        /// <summary>
        /// The effetive domain of the RP. Should be unique and will be used as the identity
        //  for the RP.
        /// </summary>
        /// <value>
        /// The server domain.
        /// </value>
        public string ServerDomain { get; set; }

        /// <summary>
        /// Server origin, including protocol host and port.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public string Origin { get; set; }

        /// <summary>
        /// TimestampDriftTolerance specifies a time in milliseconds that will be allowed
        ///  for clock drift on a timestamped attestation.
        /// </summary>
        /// <value>
        /// The time stamp drift tolerance.
        /// </value>
        public int? TimeStampDriftTolerance { get; set; }
    }
}
