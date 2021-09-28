// <copyright file="LinqExtensions.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// Linq extensions methods.
    /// <summary>
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Selects the asynchronous.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, Task<TResult>> method)
        {
            var result = new List<TResult>();
            foreach (var s in source)
            {
                result.Add(await method(s));
            }

            return result;
        }
    }
}
