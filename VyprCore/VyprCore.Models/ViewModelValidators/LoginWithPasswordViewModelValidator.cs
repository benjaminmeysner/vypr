// <copyright file="LoginWithPasswordViewModelValidator.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModelValidator
{
    using FluentValidation;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Forgot password validator.
    /// </summary>
    public class LoginWithPasswordViewModelValidator : AbstractValidator<LoginWithPasswordViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginWithPasswordViewModelValidator"/> class.
        /// </summary>
        public LoginWithPasswordViewModelValidator()
        {
            RuleFor(p => p.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must provide a user name")
                .NotNull().WithMessage("You must provide a user name");

            RuleFor(p => p.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must provide a password")
                .NotNull().WithMessage("You must provide a password");
        }
    }
}
