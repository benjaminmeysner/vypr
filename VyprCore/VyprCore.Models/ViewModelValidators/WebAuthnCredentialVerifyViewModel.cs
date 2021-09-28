// <copyright file="WebAuthnCredentialVerifyViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModelValidator
{
    using FluentValidation;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Forgot password validator.
    /// </summary>
    public class WebAuthnCredentialVerifyViewModelValidator : AbstractValidator<WebAuthnCredentialVerifyViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordViewModelValidator"/> class.
        /// </summary>
        public WebAuthnCredentialVerifyViewModelValidator()
        {
            RuleFor(p => p.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("You must provide a password")
                .NotNull().WithMessage("You must provide a password");
        }
    }
}
