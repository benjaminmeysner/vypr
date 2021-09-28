// <copyright file="ProfileDisplayViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    /// <summary>
    /// Model for user profile display.
    /// </summary>
    public class ProfileDisplayViewModel
    {
        public string UserName { get; set; }

        public string UserNameHash { get; set; }

        public string Initials { get; set; }
    }
}
