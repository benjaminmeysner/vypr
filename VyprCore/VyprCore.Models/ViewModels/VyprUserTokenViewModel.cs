// <copyright file="UserTokenViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;

    /// <summary>
    /// Tenant View Model.
    /// </summary>
    public class VyprUserTokenViewModel : ICloneableViewModel<VyprUserTokenViewModel>, IEntityViewModel
    {
        public virtual int UserId { get; set; }

        public virtual string LoginProvider { get; set; }

        public virtual string Name { get; set; }

        public virtual string Value { get; set; }

        public int UserTokenId { get; set; }

        public int? Id => UserTokenId;

        public VyprUserTokenViewModel Clone()
        {
            return (VyprUserTokenViewModel)MemberwiseClone();
        }
    }
}
