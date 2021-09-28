// <copyright file="LoginWithWebAuthnViewModelValidator.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModelValidator
{
    using FluentValidation;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Login with webauthn validator.
    /// </summary>
    public class LoginWithWebAuthnViewModelValidator : AbstractValidator<LoginWithWebAuthnViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginWithWebAuthnViewModelValidator"/> class.
        /// </summary>
        public LoginWithWebAuthnViewModelValidator()
        {
            RuleFor(p => p.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must provide a user name")
                .NotNull().WithMessage("You must provide a user name");
        }
    }
}
