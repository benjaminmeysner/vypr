// <copyright file="MapHelpers.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Epod.Shared.Helpers
{
    using System;
    using VyprCore.Models.Models;

    /// <summary>
    /// Map helpers. Epod.
    /// </summary>
    public static class MapHelpers
    {
        /// <summary>
        /// Coordinateses from string.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>new Map coordinates.</returns>
        /// <exception cref="ArgumentException">Arg exception.</exception>
        public static MapCoordinates CoordinatesFromString(string coordinates)
        {
            if (string.IsNullOrEmpty(coordinates) || !coordinates.Contains(','))
            {
                return null;
            }

            var split = coordinates.Split(',');
            if (double.TryParse(split[0], out double lat)
                && double.TryParse(split[1], out double lng))
            {
                return new MapCoordinates { Lat = lat, Lng = lng };
            }

            throw new ArgumentException($"{coordinates}");
        }
    }
}
