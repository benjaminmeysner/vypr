// <copyright file="ImageHelpers.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Utilities.Helpers
{
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Processing;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Image helper methods.
    /// </summary>
    public class ImageHelpers
    {
        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static async Task<byte[]> CompressAndResizeImageAsync(byte[] image, int quality, int width, int height)
        {
            using MemoryStream outputStream = new();
            using MemoryStream inputStream = new(image);
            using Image data = await Image.LoadAsync(inputStream);

            data.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Min, Size = new Size(width, height) }));
            await data.SaveAsJpegAsync(outputStream, new JpegEncoder() { Quality = quality });
            return outputStream.ToArray();
        }

        /// <summary>
        /// Converts to imghtmlelement.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        public static string ToImgHtmlElement(byte[] image, string contentType)
        {
            return $"data:{contentType};base64,{Convert.ToBase64String(image)}";
        }

        /// <summary>
        /// Converts to imghtmlelement.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        public static string ToImgHtmlElement(string base64, string contentType)
        {
            return $"data:{contentType};base64,{base64}";
        }
    }
}
