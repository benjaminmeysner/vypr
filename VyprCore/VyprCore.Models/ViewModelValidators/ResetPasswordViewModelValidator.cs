// <copyright file="ResetPasswordViewModelValidator.cs" company="Vypr Systems">
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
    public class ResetPasswordViewModelValidator : AbstractValidator<ResetPasswordViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordViewModelValidator"/> class.
        /// </summary>
        public ResetPasswordViewModelValidator()
        {
            RuleFor(p => p.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must provide a new password")
                .NotNull().WithMessage("You must provide a new password")
                .Matches(RegExHelpers.MinLengthEightAtLeastOneLetterOneNumberOneSpecial)
                .WithMessage("Password must be at least 8 characters in length, contain at least 1 letter, one number and 1 special character");

            RuleFor(p => p.ConfirmPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please confirm your new password")
                .NotNull().WithMessage("Please confirm your new password")
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");
        }
    }
}
