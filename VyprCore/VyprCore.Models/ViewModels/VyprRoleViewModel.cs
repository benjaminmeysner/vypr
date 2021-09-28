// <copyright file="RoleViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;
    using VyprCore.Models.Resources;
    using VyprCore.Utilities.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Role view model.
    /// </summary>
    public class VyprRoleViewModel : IEntityViewModel, ICloneableViewModel<VyprRoleViewModel>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [RequiredAsterix]
        [Display(ResourceType = typeof(StandardText), Name = "Name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VyprRoleViewModel"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Display(ResourceType = typeof(StandardText), Name = "Active")]
        public bool Active { get; set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the role.
        /// </summary>
        /// <value>
        /// The type of the role.
        /// </value>
        public int? RoleType { get; set; }

        /// <summary>
        /// Gets or sets the name of the role type.
        /// </summary>
        /// <value>
        /// The name of the role type.
        /// </value>
        [Display(ResourceType = typeof(StandardText), Name = "RoleType")]
        public string RoleTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [tenant has role].
        /// This is specifically used in the client UI, to dictate whether
        /// or not a tenant has this role or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [tenant has role]; otherwise, <c>false</c>.
        /// </value>
        public bool TenantHasRole { get; set; }

        /// <summary>
        /// Gets or sets the tenant role identifier.
        /// This is specifically used in the client UI, to dictate whether
        /// or not a tenant has this role or not.
        /// </summary>
        /// <value>
        /// The tenant role identifier.
        /// </value>
        public int? TenantRoleId { get; set; }

        public VyprRoleViewModel Clone()
        {
            return (VyprRoleViewModel)MemberwiseClone();
        }
    }
}
