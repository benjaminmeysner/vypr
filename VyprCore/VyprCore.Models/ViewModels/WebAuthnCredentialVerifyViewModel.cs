// <copyright file="WebAuthnCreateCredentialViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Models.Resources;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// ViewModel representing a Fido Credential options instance.
    /// </summary>
    public class WebAuthnCredentialVerifyViewModel
    {
        public string UserName { get; set; }

        [Display(ResourceType = typeof(StandardText), Name = nameof(StandardText.ConfirmPassword))]
        public string Password { get; set; }
    }
}
