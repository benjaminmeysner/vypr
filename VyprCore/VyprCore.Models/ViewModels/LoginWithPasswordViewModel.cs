// <copyright file="LoginWithPasswordViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Models.Resources;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Login view model.
    /// </summary>
    public class LoginWithPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Display(ResourceType = typeof(StandardText), Name = "UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Display(ResourceType = typeof(StandardText), Name = "Password")]
        public string Password { get; set; }
    }
}
