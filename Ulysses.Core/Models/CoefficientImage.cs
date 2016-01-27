using System.Collections.Generic;
using System.Linq;
using Ulysses.Core.Exceptions;

namespace Ulysses.Core.Models
{
    public class CoefficientImage : ImageContainer
    {
        public CoefficientImage(IEnumerable<double> values, ImageModel imageModel) : base(imageModel)
        {
            Coefficients = values.ToArray();

            if (Coefficients.Length != imageModel.Height * imageModel.Width)
            {
                throw new ImageModelMismatchException();
            }
        }

        public double[] Coefficients { get; }

        public static Image operator +(Image image, CoefficientImage coefficients)
        {
            if (image.ImageModel != coefficients.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var imagePixels = image.ImagePixels.Zip(coefficients.Coefficients, (pixel, coefficient) => (Pixel)(pixel + coefficient));
            return new Image(imagePixels, image.ImageModel);
        }

        public static Image operator +(CoefficientImage coefficients, Image image)
        {
            return image + coefficients;
        }

        public static Image operator -(Image image, CoefficientImage coefficients)
        {
            if (image.ImageModel != coefficients.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var imagePixels = image.ImagePixels.Zip(coefficients.Coefficients, (pixel, coefficient) => (Pixel)(pixel - coefficient));
            return new Image(imagePixels, image.ImageModel);
        }

        public static Image operator -(CoefficientImage coefficients, Image image)
        {
            if (image.ImageModel != coefficients.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var imagePixels = coefficients.Coefficients.Zip(image.ImagePixels, (coefficient, pixel) => (Pixel)(coefficient - pixel));
            return new Image(imagePixels, image.ImageModel);
        }

        public static Image operator *(Image image, CoefficientImage coefficients)
        {
            if (image.ImageModel != coefficients.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var imagePixels = image.ImagePixels.Zip(coefficients.Coefficients, (coefficient, pixel) => (Pixel)(coefficient * pixel));
            return new Image(imagePixels, image.ImageModel);
        }

        public static Image operator *(CoefficientImage coefficients, Image image)
        {
            return image * coefficients;
        }
    }
}