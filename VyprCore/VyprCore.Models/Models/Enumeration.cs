// <copyright file="Enumeration.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Enumeration base class.
    /// </summary>
    public abstract class Enumeration<T> where T : Enumeration<T>
    {
        private static List<T> EnumValues { get; } = new List<T>();

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Enumeration"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
            EnumValues.Add((T)this);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;

        public static List<T> GetValues()
        {
            return EnumValues;
        }

        public static T GetById(int id)
        {
            foreach (T t in EnumValues)
            {
                if (t.Id == id) return t;
            }
            return null;
        }
    }
}
