// <copyright file="StringHelpers.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Helpers
{
    using System;
    using System.Linq;

    /// <summary>
    /// Regular expression helpers.
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// Removes the trailing slash from the string and returns the new string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string SanitiseQueryString(string str)
        {
            return str.TrimEnd(new[] { '&', '?', ',', ' ' });
        }

        /// <summary>
        /// Commas the seperated parameters if they are not empty or null.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <returns></returns>
        public static string CommaSeperatedParameters(params string[] words)
        {
            string str = string.Empty;
            foreach (var word in words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    str += $"{word}, ";
                }
            }

            return SanitiseQueryString(str);
        }

        /// <summary>
        /// Concats the first characters from the users first name and then
        /// last name. If either is null then will return the '?' character.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <returns></returns>
        public static string CreateUserInitials(params string[] words)
        {
            var initials = words.Take(2).Where(x => !string.IsNullOrEmpty(x)).Select(x => x[0]);

            if (!initials.Any())
            {
                return "?";
            }

            return string.Join(string.Empty, initials);
        }

        /// <summary>
        /// Formats the exception into a human readable format.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string FormatException(Exception e)
        {
            return string.Format("{0}{1}", e.Message, e.InnerException != null ? " (" + e.InnerException.Message + ")" : "");
        }
    }
}
