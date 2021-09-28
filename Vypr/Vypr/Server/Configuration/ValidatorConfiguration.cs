// <copyright file="ValidatorConfiguration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Configuration
{
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using VyprCore.Models.ViewModels;
    using VyprCore.Models.ViewModelValidator;

    /// <summary>
    /// Validator configurations.
    /// </summary>
    public static class ValidatorConfiguration
    {
        /// <summary>
        /// Adds the validators.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddConfiguredValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<LoginWithPasswordViewModel>, LoginWithPasswordViewModelValidator>();
            services.AddTransient<IValidator<ChangePasswordViewModel>, ChangePasswordViewModelValidator>();
            services.AddTransient<IValidator<ForgotPasswordViewModel>, ForgotPasswordViewModelValidator>();
            services.AddTransient<IValidator<ResetPasswordViewModel>, ResetPasswordViewModelValidator>();
            services.AddTransient<IValidator<SetPasswordViewModel>, SetPasswordViewModelValidator>();
            services.AddTransient<IValidator<VyprUserViewModel>, VyprUserViewModelValidator>();
            services.AddTransient<IValidator<InvitationalCreatePasswordViewModel>, InvitationalCreatePasswordViewModelValidator>();

            return services;
        }
    }
}
