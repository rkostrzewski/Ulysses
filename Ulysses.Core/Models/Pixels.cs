using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ulysses.Core.Exceptions;

namespace Ulysses.Core.Models
{
    public class Pixels : IEnumerable<Pixel>, IEquatable<Pixels>
    {
        private readonly Pixel[] _pixels;

        internal Pixels(IEnumerable<byte> pixels, ImageModel imageModel) : this(pixels.Select(p => (Pixel)p), imageModel)
        {
        }

        internal Pixels(IEnumerable<ushort> pixels, ImageModel imageModel) : this(pixels.Select(p => (Pixel)p), imageModel)
        {
        }

        internal Pixels(IEnumerable<Pixel> pixels, ImageModel imageModel)
        {
            _pixels = pixels.ToArray();

            if (_pixels.Length != imageModel.Height * imageModel.Width)
            {
                throw new ImageModelMismatchException();
            }
        }

        internal Pixels(int width, int height)
        {
            _pixels = new Pixel[height * width];
        }

        public Pixel this[int x]
        {
            get
            {
                return _pixels[x];
            }
            set
            {
                _pixels[x] = value;
            }
        }

        public IEnumerator<Pixel> GetEnumerator()
        {
            return _pixels.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Equals(Pixels other)
        {
            return this.SequenceEqual(other);
        }
    }
}