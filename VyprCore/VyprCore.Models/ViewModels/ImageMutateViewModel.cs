// <copyright file="ImageMutateViewModel.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.ViewModels
{
    /// <summary>
    /// Image mutation view model.
    /// </summary>
    public class ImageMutateViewModel
    {
        public byte[] Image { get; set; }

        public int Quality { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
