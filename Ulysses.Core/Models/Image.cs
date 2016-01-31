using System;
using System.Linq;
using Ulysses.Core.Exceptions;

namespace Ulysses.Core.Models
{
    public class Image : ImageContainer, IEquatable<Image>
    {
        public Image(byte[] imagePixels, ImageModel imageModel) : base(imageModel)
        {
            ImagePixels = new Pixels(imagePixels, imageModel);
        }

        public Image(ushort[] imagePixels, ImageModel imageModel) : base(imageModel)
        {
            ImagePixels = new Pixels(imagePixels, imageModel);
        }

        public Image(Pixel[] imagePixels, ImageModel imageModel) : base(imageModel)
        {
            ImagePixels = new Pixels(imagePixels, imageModel);
        }

        public Image(ImageModel imageModel) : base(imageModel)
        {
            ImagePixels = new Pixels(imageModel);
        }

        public Pixels ImagePixels { get; }

        public bool Equals(Image other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return ImageModel == other.ImageModel && ImagePixels.Equals(other.ImagePixels);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == typeof (Image) && Equals((Image)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = ImagePixels.GetHashCode();
            hashCode = (hashCode * 397) ^ ImageModel.GetHashCode();
            return hashCode;
        }

        public static Image operator +(Image first, Image second)
        {
            if (first.ImageModel != second.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var outputImage = new Image(first.ImageModel);

            for (var i = 0; i < outputImage.ImagePixels.Length; i++)
            {
                outputImage.ImagePixels[i] = first.ImagePixels[i] + second.ImagePixels[i];
            }

            return outputImage;
        }

        public static Image operator -(Image first, Image second)
        {
            if (first.ImageModel != second.ImageModel)
            {
                throw new ImageModelMismatchException();
            }

            var outputImage = new Image(first.ImageModel);

            for (var i = 0; i < outputImage.ImagePixels.Length; i++)
            {
                outputImage.ImagePixels[i] = first.ImagePixels[i] - second.ImagePixels[i];
            }

            return outputImage;
        }

        public static bool operator ==(Image left, Image right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Image left, Image right)
        {
            return !Equals(left, right);
        }
    }
}