using System;

namespace Ulysses.Core.Models
{
    public static class ProcessedImageExtensions
    {
        public static Image ToImage(this ProcessedImage processedImage)
        {
            var imageModel = processedImage.ImageModel;
            var outputImagePixels = new Pixel[imageModel.Width * imageModel.Height];

            for (var i = 0; i < imageModel.Width * imageModel.Height; i++)
            {
                outputImagePixels[i] = (Pixel)processedImage.Values[i];
            }

            return new Image(outputImagePixels, imageModel);
        }

        public static ProcessedImage Abs(this ProcessedImage processedImage)
        {
            var outputImage = new ProcessedImage(processedImage.ImageModel);

            for (var i = 0; i < outputImage.Values.Length; i++)
            {
                outputImage.Values[i] = Math.Abs(processedImage.Values[i]);
            }

            return outputImage;
        }
    }
}