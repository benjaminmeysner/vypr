// <copyright file="SystemAdministratorViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;

    /// <summary>
    /// Tenant View Model.
    /// </summary>
    public class VyprSystemAdministratorViewModel : ICloneableViewModel<VyprSystemAdministratorViewModel>, IEntityViewModel
    {
        public static int Rank => 1;

        public int? UserId { get; set; }

        public int? Id => UserId;

        public VyprSystemAdministratorViewModel Clone()
        {
            return (VyprSystemAdministratorViewModel)MemberwiseClone();
        }
    }
}
