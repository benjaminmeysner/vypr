// <copyright file="RegExHelpers.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Helpers
{
    using System;
    using System.Net.Mail;

    /// <summary>
    /// Regular expression helpers.
    /// </summary>
    public static class RegExHelpers
    {
        /// <summary>
        /// The regular expression for minimum length eight, at least one letter, one number and one special
        /// </summary>
        public const string MinLengthEightAtLeastOneLetterOneNumberOneSpecial = "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$";

        /// <summary>
        /// Determines whether [is valid email format] [the specified email].
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///   <c>true</c> if [is valid email format] [the specified email]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidEmailFormat(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            try
            {
                MailAddress m = new(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
