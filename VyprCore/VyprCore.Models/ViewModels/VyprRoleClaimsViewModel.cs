// <copyright file="RoleClaimsViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;

    /// <summary>
    /// Role Claims view model
    /// </summary>
    public class VyprRoleClaimsViewModel : ICloneableViewModel<VyprRoleClaimsViewModel>, IEntityViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the type of the claim.
        /// </summary>
        /// <value>
        /// The type of the claim.
        /// </value>
        public string ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the claim value.
        /// </summary>
        /// <value>
        /// The claim value.
        /// </value>
        public string ClaimValue { get; set; }

        /// <summary>
        /// Gets or sets the claim description.
        /// </summary>
        /// <value>
        /// The claim description.
        /// </value>
        public string ClaimDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [role has claim].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [role has claim]; otherwise, <c>false</c>.
        /// </value>
        public bool RoleHasClaim { get; set; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public VyprRoleClaimsViewModel Clone()
        {
            return (VyprRoleClaimsViewModel)MemberwiseClone();
        }
    }
}
