using Ulysses.Core.Exceptions;

namespace Ulysses.Core.Models
{
    public class CoefficientContainer : ImageContainer
    {
        public CoefficientContainer(double[,] values, ImageBitDepth bitDepth) : base(values.GetLength(0), values.GetLength(1), bitDepth)
        {
            Values = values;
        }

        public CoefficientContainer(ImageModel imageModel) : base(imageModel)
        {
            Values = new double[ImageModel.Width, ImageModel.Height];
        }

        public double[,] Values { get; }

        public static Image operator +(Image image, CoefficientContainer coefficients)
        {
            if (image.ImageModel != coefficients.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var output = new Image(image.ImageModel);

            for (int x = 0, k = 0; x < image.ImageModel.Width; x++)
            {
                for (var y = 0; y < image.ImageModel.Height; y++, k++)
                {
                    output.ImagePixels[k] = (Pixel)(image.ImagePixels[k] + coefficients.Values[x, y]);
                }
            }

            return output;
        }

        public static Image operator +(CoefficientContainer coefficients, Image image)
        {
            return image + coefficients;
        }

        public static Image operator -(Image image, CoefficientContainer coefficients)
        {
            if (image.ImageModel != coefficients.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var output = new Image(image.ImageModel);

            for (int x = 0, k = 0; x < image.ImageModel.Width; x++)
            {
                for (var y = 0; y < image.ImageModel.Height; y++, k++)
                {
                    output.ImagePixels[k] = (Pixel)(image.ImagePixels[k] - coefficients.Values[x, y]);
                }
            }

            return output;
        }

        public static Image operator -(CoefficientContainer coefficients, Image image)
        {
            return image - coefficients;
        }

        public static Image operator *(Image image, CoefficientContainer coefficients)
        {
            if (image.ImageModel != coefficients.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var output = new Image(image.ImageModel);

            for (int x = 0, k = 0; x < image.ImageModel.Width; x++)
            {
                for (var y = 0; y < image.ImageModel.Height; y++, k++)
                {
                    output.ImagePixels[k] = (Pixel)(image.ImagePixels[k] * coefficients.Values[x, y]);
                }
            }

            return output;
        }

        public static Image operator *(CoefficientContainer coefficients, Image image)
        {
            return image * coefficients;
        }
    }
}