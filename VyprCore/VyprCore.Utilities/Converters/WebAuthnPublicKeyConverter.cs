// <copyright file="WebAuthnPublicKeyConverter.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Converters
{
    using Fido2NetLib.Objects;
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// WebAuthnPublicKey converter.
    /// </summary>
    public class WebAuthnPublicKeyConverter : JsonConverter<PublicKeyCredentialType?>
    {
        /// <summary>
        /// [WebAuthnPublicKeyConverter]
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override PublicKeyCredentialType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() == "" ? PublicKeyCredentialType.PublicKey : PublicKeyCredentialType.PublicKey;
        }

        // This method will be ignored on serialization, and the default typeof(DateTime) converter is used instead.
        // This is a bug: https://github.com/dotnet/corefx/issues/41070#issuecomment-560949493
        public override void Write(Utf8JsonWriter writer, PublicKeyCredentialType? value, JsonSerializerOptions options)
        {
        }
    }
}
