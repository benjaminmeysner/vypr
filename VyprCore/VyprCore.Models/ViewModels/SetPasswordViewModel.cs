// <copyright file="SetPasswordViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Models.Resources;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model for password change.
    /// </summary>
    public class SetPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>
        /// The old password.
        /// </value>
        [Display(ResourceType = typeof(StandardText), Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm new password.
        /// </summary>
        /// <value>
        /// The confirm new password.
        /// </value>
        [Display(ResourceType = typeof(StandardText), Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
