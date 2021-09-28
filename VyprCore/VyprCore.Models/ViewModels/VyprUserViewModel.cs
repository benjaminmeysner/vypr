// <copyright file="VyprUserViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;
    using VyprCore.Models.Resources;
    using VyprCore.Utilities.Attributes;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model for user.
    /// </summary>
    public class VyprUserViewModel : ICloneableViewModel<VyprUserViewModel>, IEntityViewModel
    {
        public int Id { get; set; }

        [RequiredAsterix]
        [Display(ResourceType = typeof(StandardText), Name = "Email")]
        public string Email { get; set; }

        [RequiredAsterix]
        [Display(ResourceType = typeof(StandardText), Name = "UserName")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(StandardText), Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(StandardText), Name = "LastName")]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(StandardText), Name = "ExternalId")]
        public string ExternalId { get; set; }

        [Display(ResourceType = typeof(StandardText), Name = "Active")]
        public bool Active { get; set; }

        public string SecurityStamp { get; set; }

        public string NormalizedUserName { get; set; }

        public string NormalizedEmail { get; set; }

        public bool IsTenantAdministrator { get; set; }

        public bool IsSystemAdministrator { get; set; }

        int? IEntityViewModel.Id => Id;

        public VyprUserViewModel Clone()
        {
            return (VyprUserViewModel)MemberwiseClone();
        }
    }
}
