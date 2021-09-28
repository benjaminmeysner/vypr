// <copyright file="StringExtensions.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Helpers
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Regular expression helpers.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Removes the spaces from the string in a newly returned string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string RemoveSpaces(this string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }

        /// <summary>
        /// Removes the trailing slash from the string and returns the new string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string RemoveTrailingSlash(this string str)
        {
            return str.TrimEnd(new[] { '/', '\\' });
        }
    }
}
