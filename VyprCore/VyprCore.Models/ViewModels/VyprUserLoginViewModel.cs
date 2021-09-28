// <copyright file="UserLoginViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;

    /// <summary>
    /// User login View Model.
    /// </summary>
    public class VyprUserLoginViewModel : ICloneableViewModel<VyprUserLoginViewModel>, IEntityViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        public string ProviderDisplayName { get; set; }

        public int UserId { get; set; }

        public int UserLoginId { get; set; }

        public int? Id => UserLoginId;

        public VyprUserLoginViewModel Clone()
        {
            return (VyprUserLoginViewModel)MemberwiseClone();
        }
    }
}
