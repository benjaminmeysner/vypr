// <copyright file="ForgotPasswordViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Models.Resources;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Login view model.
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Display(ResourceType = typeof(StandardText), Name = "UserName")]
        public string UserName { get; set; }
    }
}
