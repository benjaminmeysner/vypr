// <copyright file="ChangePasswordViewModelValidator.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModelValidator
{
    using FluentValidation;
    using VyprCore.Models.ViewModels;
    using VyprCore.Utilities.Helpers;

    /// <summary>
    /// Forgot password validator.
    /// </summary>
    public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordViewModelValidator"/> class.
        /// </summary>
        public ChangePasswordViewModelValidator()
        {
            RuleFor(p => p.OldPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must provide your old password")
                .NotNull().WithMessage("You must provide your old password");

            RuleFor(p => p.NewPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must provide a new password")
                .NotNull().WithMessage("You must provide a new password")
                .Matches(RegExHelpers.MinLengthEightAtLeastOneLetterOneNumberOneSpecial)
                .WithMessage("Password must be at least 8 characters in length, contain at least 1 letter, one number and 1 special character");

            RuleFor(p => p.ConfirmNewPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must confirm your new password")
                .NotNull().WithMessage("You must confirm your new password")
                .Equal(x => x.NewPassword).WithMessage("Your new password does not match");
        }
    }
}
