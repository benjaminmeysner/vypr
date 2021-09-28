// <copyright file="ForgotPasswordViewModelValidator.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModelValidator
{
    using FluentValidation;
    using VyprCore.Utilities.Helpers;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Forgot password validator.
    /// </summary>
    public class ForgotPasswordViewModelValidator : AbstractValidator<ForgotPasswordViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordViewModelValidator"/> class.
        /// </summary>
        public ForgotPasswordViewModelValidator()
        {
            RuleFor(p => p.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must provide a user name")
                .NotNull().WithMessage("You must provide a user name")
                .Must(x => RegExHelpers.IsValidEmailFormat(x)).WithMessage("Please provide a valid user name (email)");
        }
    }
}
