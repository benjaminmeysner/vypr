// <copyright file="InvitationalCreatePasswordViewModelValidator.cs" company="Vypr Systems">
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
    public class InvitationalCreatePasswordViewModelValidator : AbstractValidator<InvitationalCreatePasswordViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see SetPasswordViewModelValidator"/> class.
        /// </summary>
        public InvitationalCreatePasswordViewModelValidator()
        {
            RuleFor(p => p.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must provide a password")
                .NotNull().WithMessage("You must provide a password");

            RuleFor(p => p.ConfirmPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must confirm your password")
                .NotNull().WithMessage("You must confirm your password")
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");
        }
    }
}
