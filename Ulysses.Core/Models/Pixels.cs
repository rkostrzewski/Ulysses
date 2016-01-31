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
        private ushort _minValue;
        private ushort _maxValue;

        internal Pixels(byte[] pixels, ImageModel imageModel) : this(imageModel)
        {
            if (_pixels.Length != imageModel.Height * imageModel.Width)
            {
                throw new ImageModelMismatchException();
            }

            for (var i = 0; i < pixels.Length; i++)
            {
                _pixels[i] = pixels[i];
            }

            AdjustRangeOfPixels();
        }

        internal Pixels(ushort[] pixels, ImageModel imageModel) : this(imageModel)
        {
            if (_pixels.Length != imageModel.Height * imageModel.Width)
            {
                throw new ImageModelMismatchException();
            }

            for (var i = 0; i < pixels.Length; i++)
            {
                _pixels[i] = pixels[i];
            }

            AdjustRangeOfPixels();
        }

        internal Pixels(Pixel[] pixels, ImageModel imageModel) : this(imageModel)
        {
            if (_pixels.Length != imageModel.Height * imageModel.Width)
            {
                throw new ImageModelMismatchException();
            }

            for (var i = 0; i < pixels.Length; i++)
            {
                _pixels[i] = pixels[i];
            }

            AdjustRangeOfPixels();
        }

        internal Pixels(ImageModel imageModel)
        {
            InitializeMinMaxValues(imageModel.ImageBitDepth);
            _pixels = new Pixel[imageModel.Height * imageModel.Width];
        }

        private void AdjustRangeOfPixels()
        {
            for (var i = 0; i < this.Length; i++)
            {
                if (_pixels[i] >= _minValue && _pixels[i] <= _maxValue)
                {
                    continue;
                }

                if (_pixels[i] < _minValue)
                {
                    _pixels[i] = _minValue;
                }
                
                if (_pixels[i] > _maxValue)
                {
                    _pixels[i] = _maxValue;
                }
            }
        }

        private void InitializeMinMaxValues(ImageBitDepth imageBitDepth)
        {
            _minValue = 0;

            switch (imageBitDepth)
            {
                case ImageBitDepth.Bpp8:
                    _maxValue = 255;
                    break;
                case ImageBitDepth.Bpp12:
                    _maxValue = 4095;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(imageBitDepth), imageBitDepth, null);
            }
        }

        public Pixel this[int x]
        {
            get
            {
                return _pixels[x];
            }
            set
            {
                if (value < _minValue)
                {
                    _pixels[x] = _minValue;
                }

                if (value > _maxValue)
                {
                    _pixels[x] = _maxValue;
                }

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

        public int Length => _pixels.Length;
    }
}