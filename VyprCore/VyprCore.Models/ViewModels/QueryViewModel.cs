// <copyright file="ClientApiAdmin.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    /// <summary>
    /// Query view model.
    /// </summary>
    public class QueryViewModel<TEntity>
    {
        public Remote.Linq.Expressions.LambdaExpression Filter { get; set; }

        public Remote.Linq.Expressions.LambdaExpression OrderBy { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
