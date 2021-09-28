// <copyright file="RoleTypeViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;

    /// <summary>
    /// Role type viewmodel.
    /// </summary>
    /// <seealso cref="VyprCore.Interfaces.Model.ICloneableViewModel&lt;VyprCore.Models.ViewModels.VyprRoleTypeViewModel&gt;" />
    /// <seealso cref="VyprCore.Interfaces.Entity.IEntityViewModel" />
    public class VyprRoleTypeViewModel : ICloneableViewModel<VyprRoleTypeViewModel>, IEntityViewModel
    {
        public int Rank { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }

        public int? Id => Rank;

        public VyprRoleTypeViewModel Clone()
        {
            return (VyprRoleTypeViewModel)MemberwiseClone();
        }
    }
}
