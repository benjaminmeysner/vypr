// <copyright file="AttributeHelpers.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Helpers
{
    using VyprCore.Utilities.Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// Attribute helper methods class.
    /// </summary>
    public static class AttributeHelpers
    {
        /// <summary>
        /// Gets properties display attribute value. If no such attribute exists,
        /// then the appropriate exception is thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">The property.</param>
        /// <returns>The display value.</returns>
        public static string GetDisplayAttributeValue<T>(this Expression<Func<T>> property)
        {
            if (property.Body is not MemberExpression memberExpression)
            {
                throw new InvalidOperationException("Expression must be a member expression.");
            }

            var displayAttribute = memberExpression.Member.GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute is null)
            {
                return $"Cannot resolve DisplayAttribute for bound property.";
            }

            var resourceManager = new ResourceManager(displayAttribute.ResourceType);

            return resourceManager.GetString(displayAttribute.Name);
        }

        /// <summary>
        /// Gets the required asterix value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">The property.</param>
        /// <returns>Whether of not propert has this attribute.</returns>
        /// <exception cref="InvalidOperationException">Expression must be a member expression</exception>
        public static bool GetRequiredAsterixValue<T>(this Expression<Func<T>> property)
        {
            var memberExpression = property.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new InvalidOperationException("Expression must be a member expression");
            }

            return memberExpression.Member.GetCustomAttribute<RequiredAsterixAttribute>() != null;
        }
    }
}
