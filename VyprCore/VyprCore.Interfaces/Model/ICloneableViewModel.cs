// <copyright file="VyprCoreComponentBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Interfaces.Model
{
    /// <summary>
    /// Provides interface for model cloning.
    /// </summary>
    public interface ICloneableViewModel<TViewModel>
    {
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public TViewModel Clone();
    }
}
