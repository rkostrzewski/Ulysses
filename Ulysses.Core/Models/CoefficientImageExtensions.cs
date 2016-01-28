using System;
using System.Linq;

namespace Ulysses.Core.Models
{
    public static class ProcessedImageExtensions
    {
        public static Image ToImage(this ProcessedImage processedImage)
        {
            var imagePixels = processedImage.Values.Select(i => (Pixel)i);
            var imageModel = processedImage.ImageModel;
            
            return new Image(imagePixels, imageModel);
        }

        public static ProcessedImage Abs(this ProcessedImage processedImage)
        {
            var coefficients = processedImage.Values.Select(Math.Abs);
            var imageModel = processedImage.ImageModel;

            return new ProcessedImage(coefficients, imageModel);
        }
    }
}