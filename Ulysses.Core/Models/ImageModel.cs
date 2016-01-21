using System;

namespace Ulysses.Core.Models
{
    public class ImageModel : IEquatable<ImageModel>
    {
        public ImageModel(ushort width, ushort height, ImageBitDepth imageBitDepth)
        {
            Width = width;
            Height = height;
            ImageBitDepth = imageBitDepth;
        }

        public ushort Width { get; }
        public ushort Height { get; }
        public ImageBitDepth ImageBitDepth { get; }

        public bool Equals(ImageModel other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return Width == other.Width && Height == other.Height && ImageBitDepth == other.ImageBitDepth;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            
            return obj.GetType() == typeof (ImageModel) && Equals((ImageModel)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)Width;
                hashCode = (hashCode * 397) ^ Height;
                hashCode = (hashCode * 397) ^ (int)ImageBitDepth;
                return hashCode;
            }
        }

        public static bool operator ==(ImageModel left, ImageModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ImageModel left, ImageModel right)
        {
            return !Equals(left, right);
        }
    }
}