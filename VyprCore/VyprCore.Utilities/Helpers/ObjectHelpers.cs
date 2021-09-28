// <copyright file="ObjectHelpers.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Helpers
{
    public static class ObjectHelpers
    {
        /// <summary>
        /// Values the or false.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ValueOrFalse(object value)
        {
            if (value is null)
            {
                return false;
            }

            return (bool)value;
        }
    }
}
