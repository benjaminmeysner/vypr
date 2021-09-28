// <copyright file="VyprUserViewModelValidator.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModelValidator
{
    using FluentValidation;
    using VyprCore.Utilities.Helpers;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// User View Model validator.
    /// </summary>
    public class VyprUserViewModelValidator : AbstractValidator<VyprUserViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see VyprUserViewModelValidator"/> class.
        /// </summary>
        public VyprUserViewModelValidator()
        {
            RuleFor(p => p.UserName).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("User name cannot be null or empty");
            RuleFor(p => p.Email).Cascade(CascadeMode.Stop).NotEmpty().NotNull().Must(x => RegExHelpers.IsValidEmailFormat(x)).WithMessage("Please provide a valid email");
        }
    }
}
