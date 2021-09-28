// <copyright file="ChangePasswordViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Models.Resources;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model for password change.
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>
        /// The old password.
        /// </value>
        [Display(ResourceType = typeof(StandardText), Name = "OldPassword")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Creates new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        [Display(ResourceType = typeof(StandardText), Name = "NewPassword")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirm new password.
        /// </summary>
        /// <value>
        /// The confirm new password.
        /// </value>
        [Display(ResourceType = typeof(StandardText), Name = "ConfirmNewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
