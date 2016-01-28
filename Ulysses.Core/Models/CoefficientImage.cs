using System.Collections.Generic;
using System.Linq;
using Ulysses.Core.Exceptions;

namespace Ulysses.Core.Models
{
    public class ProcessedImage : ImageContainer
    {
        public ProcessedImage(IEnumerable<double> values, ImageModel imageModel) : base(imageModel)
        {
            Values = values.ToArray();

            if (Values.Length != imageModel.Height * imageModel.Width)
            {
                throw new ImageModelMismatchException();
            }
        }

        public double[] Values { get; set; }

        public static ProcessedImage operator +(double scalar, ProcessedImage processedImage)
        {
            var imagePixels = processedImage.Values.Select(p => p + scalar);
            return new ProcessedImage(imagePixels, processedImage.ImageModel);
        }

        public static ProcessedImage operator +(ProcessedImage processedImage, double scalar)
        {
            return scalar + processedImage;
        }

        public static ProcessedImage operator +(Image image, ProcessedImage processedImage)
        {
            return processedImage + image;
        }

        public static ProcessedImage operator +(ProcessedImage processedImage, Image image)
        {
            if (image.ImageModel != processedImage.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var imagePixels = image.ImagePixels.Zip(processedImage.Values, (pixel, processedPixel) => pixel + processedPixel);
            return new ProcessedImage(imagePixels, image.ImageModel);
        }

        public static ProcessedImage operator +(ProcessedImage firstProcessedImage, ProcessedImage secondProcessedImage)
        {
            if (firstProcessedImage.ImageModel != secondProcessedImage.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var processedPixels = firstProcessedImage.Values.Zip(secondProcessedImage.Values, (pixel, processedPixel) => pixel + processedPixel);
            return new ProcessedImage(processedPixels, firstProcessedImage.ImageModel);
        }

        public static ProcessedImage operator -(ProcessedImage processedImage)
        {
            var imagePixels = processedImage.Values.Select(i => -i);
            return new ProcessedImage(imagePixels, processedImage.ImageModel);
        }

        public static ProcessedImage operator -(Image image, ProcessedImage processedImage)
        {
            if (image.ImageModel != processedImage.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var imagePixels = image.ImagePixels.Zip(processedImage.Values, (pixel, processedPixel) => (pixel - processedPixel));
            return new ProcessedImage(imagePixels, image.ImageModel);
        }

        public static ProcessedImage operator -(ProcessedImage processedImage, Image image)
        {
            if (image.ImageModel != processedImage.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var imagePixels = processedImage.Values.Zip(image.ImagePixels, (processedPixel, pixel) => (processedPixel - pixel));
            return new ProcessedImage(imagePixels, image.ImageModel);
        }

        public static ProcessedImage operator *(Image image, ProcessedImage processedImage)
        {
            if (image.ImageModel != processedImage.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var imagePixels = image.ImagePixels.Zip(processedImage.Values, (processedPixel, pixel) => (processedPixel * pixel));
            return new ProcessedImage(imagePixels, image.ImageModel);
        }

        public static ProcessedImage operator *(ProcessedImage processedImage, Image image)
        {
            if (image.ImageModel != processedImage.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var imagePixels = processedImage.Values.Zip(image.ImagePixels, (processedPixel, pixel) => (processedPixel * pixel));
            return new ProcessedImage(imagePixels, image.ImageModel);
        }

        public static ProcessedImage operator *(double scalar, ProcessedImage processedImage)
        {
            var imagePixels = processedImage.Values.Select(c => scalar * c);
            return new ProcessedImage(imagePixels, processedImage.ImageModel);
        }

        public static ProcessedImage operator /(ProcessedImage processedImage, double scalar)
        {
            var imagePixels = processedImage.Values.Select(c => c / scalar);
            return new ProcessedImage(imagePixels, processedImage.ImageModel);
        }

        public static ProcessedImage operator /(ProcessedImage firstProcessedImage, ProcessedImage secondProcessedImage)
        {
            if (firstProcessedImage.ImageModel != secondProcessedImage.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var imagePixels = firstProcessedImage.Values.Zip(secondProcessedImage.Values, (first, second) => (first / second));
            return new ProcessedImage(imagePixels, firstProcessedImage.ImageModel);
        }
    }
}