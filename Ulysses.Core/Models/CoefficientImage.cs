using Ulysses.Core.Exceptions;

namespace Ulysses.Core.Models
{
    public class ProcessedImage : ImageContainer
    {
        public ProcessedImage(double[] values, ImageModel imageModel) : base(imageModel)
        {
            Values = values;

            if (Values.Length != imageModel.Height * imageModel.Width)
            {
                throw new ImageModelMismatchException();
            }
        }

        internal ProcessedImage(ImageModel imageModel) : base(imageModel)
        {
            Values = new double[imageModel.Width * imageModel.Height];
        }

        public double[] Values { get; set; }

        public static ProcessedImage operator +(double scalar, ProcessedImage processedImage)
        {
            var outputImage = new ProcessedImage(processedImage.ImageModel);

            for (var i = 0; i < outputImage.Values.Length; i++)
            {
                outputImage.Values[i] = processedImage.Values[i] + scalar;
            }

            return outputImage;
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

            var outputImage = new ProcessedImage(processedImage.ImageModel);

            for (var i = 0; i < outputImage.Values.Length; i++)
            {
                outputImage.Values[i] = processedImage.Values[i] + image.ImagePixels[i];
            }

            return outputImage;
        }

        public static ProcessedImage operator +(ProcessedImage firstProcessedImage, ProcessedImage secondProcessedImage)
        {
            if (firstProcessedImage.ImageModel != secondProcessedImage.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var outputImage = new ProcessedImage(firstProcessedImage.ImageModel);

            for (var i = 0; i < outputImage.Values.Length; i++)
            {
                outputImage.Values[i] = firstProcessedImage.Values[i] + secondProcessedImage.Values[i];
            }

            return outputImage;
        }

        public static ProcessedImage operator -(ProcessedImage processedImage)
        {
            var outputImage = new ProcessedImage(processedImage.ImageModel);

            for (var i = 0; i < outputImage.Values.Length; i++)
            {
                outputImage.Values[i] = -processedImage.Values[i];
            }

            return outputImage;
        }

        public static ProcessedImage operator -(Image image, ProcessedImage processedImage)
        {
            if (image.ImageModel != processedImage.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var outputImage = new ProcessedImage(processedImage.ImageModel);

            for (var i = 0; i < outputImage.Values.Length; i++)
            {
                outputImage.Values[i] = image.ImagePixels[i] - processedImage.Values[i];
            }

            return outputImage;
        }

        public static ProcessedImage operator -(ProcessedImage processedImage, Image image)
        {
            if (image.ImageModel != processedImage.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var outputImage = new ProcessedImage(processedImage.ImageModel);

            for (var i = 0; i < outputImage.Values.Length; i++)
            {
                outputImage.Values[i] = processedImage.Values[i] - image.ImagePixels[i];
            }

            return outputImage;
        }

        public static ProcessedImage operator *(Image image, ProcessedImage processedImage)
        {
            if (image.ImageModel != processedImage.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var outputImage = new ProcessedImage(processedImage.ImageModel);

            for (var i = 0; i < outputImage.Values.Length; i++)
            {
                outputImage.Values[i] = processedImage.Values[i] * image.ImagePixels[i];
            }

            return outputImage;
        }

        public static ProcessedImage operator *(ProcessedImage processedImage, Image image)
        {
            return image * processedImage;
        }

        public static ProcessedImage operator *(ProcessedImage first, ProcessedImage second)
        {
            if (first.ImageModel != second.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var outputImage = new ProcessedImage(first.ImageModel);

            for (var i = 0; i < outputImage.Values.Length; i++)
            {
                outputImage.Values[i] = first.Values[i] * second.Values[i];
            }

            return outputImage;
        }

        public static ProcessedImage operator *(double scalar, ProcessedImage processedImage)
        {
            var outputImage = new ProcessedImage(processedImage.ImageModel);

            for (var i = 0; i < outputImage.Values.Length; i++)
            {
                outputImage.Values[i] = processedImage.Values[i] * scalar;
            }

            return outputImage;
        }

        public static ProcessedImage operator /(ProcessedImage processedImage, double scalar)
        {
            var outputImage = new ProcessedImage(processedImage.ImageModel);

            for (var i = 0; i < outputImage.Values.Length; i++)
            {
                outputImage.Values[i] = processedImage.Values[i] / scalar;
            }

            return outputImage;
        }

        public static ProcessedImage operator /(ProcessedImage firstProcessedImage, ProcessedImage secondProcessedImage)
        {
            if (firstProcessedImage.ImageModel != secondProcessedImage.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var outputImage = new ProcessedImage(firstProcessedImage.ImageModel);

            for (var i = 0; i < outputImage.Values.Length; i++)
            {
                outputImage.Values[i] = firstProcessedImage.Values[i] / secondProcessedImage.Values[i];
            }

            return outputImage;
        }
    }
}