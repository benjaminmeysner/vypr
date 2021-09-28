// <copyright file="FidoCredential.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fido2NetLib.Objects;
    using Newtonsoft.Json;
    using VyprCore.Interfaces.Entity;

    /// <summary>
    /// Model representing a Fido2 Credential instance.
    /// </summary>
    /// <seealso cref="Vypr.Server.Interfaces.IEntity" />
    public class VyprWebAuthnCredential : IEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public byte[] UserId { get; set; }

        /// <summary>
        /// Gets or sets the public key.
        /// </summary>
        /// <value>
        /// The public key.
        /// </value>
        public byte[] PublicKey { get; set; }

        /// <summary>
        /// Gets or sets the user handle.
        /// </summary>
        /// <value>
        /// The user handle.
        /// </value>
        public byte[] UserHandle { get; set; }

        /// <summary>
        /// Gets or sets the signature counter.
        /// </summary>
        /// <value>
        /// The signature counter.
        /// </value>
        public uint SignatureCounter { get; set; }

        /// <summary>
        /// Gets or sets the type of the cred.
        /// </summary>
        /// <value>
        /// The type of the cred.
        /// </value>
        public string CredType { get; set; }

        /// <summary>
        /// Gets or sets the reg date.
        /// </summary>
        /// <value>
        /// The reg date.
        /// </value>
        public DateTime RegDate { get; set; }

        /// <summary>
        /// Gets or sets the aa unique identifier.
        /// </summary>
        /// <value>
        /// The aa unique identifier.
        /// </value>
        public Guid AaGuid { get; set; }

        /// <summary>
        /// Gets or sets the descriptor.
        /// </summary>
        /// <value>
        /// The descriptor.
        /// </value>
        [NotMapped]
        public PublicKeyCredentialDescriptor Descriptor
        {
            get { return string.IsNullOrWhiteSpace(DescriptorJson) ? null : JsonConvert.DeserializeObject<PublicKeyCredentialDescriptor>(DescriptorJson); }
            set { DescriptorJson = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// Gets or sets the descriptor json.
        /// </summary>
        /// <value>
        /// The descriptor json.
        /// </value>
        public string DescriptorJson { get; set; }
    }
}
