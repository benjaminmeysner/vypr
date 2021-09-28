// <copyright file="UploadFileType.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Shared.Models
{
    /// <summary>
    /// Upload file type.
    /// </summary>
    public enum UploadFileType
    {
        /// <summary>
        /// The unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The signature
        /// </summary>
        Signature = 1,

        /// <summary>
        /// The proof
        /// </summary>
        Proof = 2,

        /// <summary>
        /// The additional
        /// </summary>
        Additional = 3
    }
}
