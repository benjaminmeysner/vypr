// <copyright file="UserClaimsViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;

    /// <summary>
    /// Tenant View Model.
    /// </summary>
    public class VyprUserClaimsViewModel : ICloneableViewModel<VyprUserClaimsViewModel>, IEntityViewModel
    {
        public int UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public int? Id { get; set; }

        public VyprUserClaimsViewModel Clone()
        {
            return (VyprUserClaimsViewModel)MemberwiseClone();
        }
    }
}
