// <copyright file="EnumerationExtensions.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Extensions
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using VyprCore.Models.Models;
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Configures an enumeration for use on
    /// </summary>
    public static class EnumerationExtensions
    {
        /// <summary>
        /// Add Enumeration value converter for a TEnum property on TEntity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
        /// <typeparam name="TExtension">An additional source of values derived from TEnum.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="property">The property.</param>
        public static void OwnEnumeration<TEntity, TEnum>(this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TEnum>> property)
                where TEntity : class
                where TEnum : Enumeration<TEnum>
        {
            builder.Property(property).HasConversion(x => x.Id, x => Enumeration<TEnum>.GetById(x));
        }
    }
}
