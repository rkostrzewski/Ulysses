using System;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ProcessingAlgorithms.Templates.NonUniformityCorrection;

namespace Ulysses.ProcessingAlgorithms.Algorithms.NonUniformityCorrection
{
    public class MidwayInfraredEqualization : IImageProcessingAlgorithm
    {
        private readonly ushort _height;
        private readonly ImageModel _imageModel;
        private readonly double _standardDeviation;
        private readonly ushort _width;
        private readonly ushort _windowSize;


        public MidwayInfraredEqualization(MidwayInfraredEqualizationTemplate template)
        {
            _standardDeviation = template.StandardDeviation;
            _imageModel = template.ImageModel;
            _width = _imageModel.Width;
            _height = _imageModel.Height;
            _windowSize = (ushort)Math.Round(4 * _standardDeviation);
        }

        public Image ProcessImage(Image inputImage)
        {
            if (inputImage.ImageModel != _imageModel)
            {
                throw new ImageModelMismatchException(GetType());
            }

            var outputImagePixels = new Pixel[_width * _height];
            for (var i = 0; i < _width * _height; i++)
            {
                outputImagePixels[i] = inputImage.ImagePixels[i];
            }

            var columnHistogram = CumulativeColumnHistogram(outputImagePixels);
            var midwayWeightedHistogram = MidwayWeightedHistogram(columnHistogram);

            for (var currentColumn = _windowSize; currentColumn < _width - _windowSize; currentColumn++)
            {
                SpecifyColumn(outputImagePixels, currentColumn, midwayWeightedHistogram);
            }

            return new Image(outputImagePixels, inputImage.ImageModel);
        }

        private uint[,] CumulativeColumnHistogram(Pixel[] inputImagePixels)
        {
            var columnHistogram = new uint[_width, _height];

            for (uint column = 0; column < _width; column++)
            {
                var histoColumn = LocalMidwayHistogram(inputImagePixels, column);
                for (var row = 0; row < _height; row++)
                {
                    columnHistogram[column, row] = histoColumn[row];
                }
            }

            return columnHistogram;
        }

        private uint[] LocalMidwayHistogram(Pixel[] inputImage, uint column)
        {
            var rowsHistogram = new uint[_height];
            for (var row = 0; row < _height; row++)
            {
                rowsHistogram[row] = inputImage[row * _width + column];
            }

            Array.Sort(rowsHistogram);
            return rowsHistogram;
        }

        private Pixel[,] MidwayWeightedHistogram(uint[,] sortedColumns)
        {
            var finalHistogram = new Pixel[_width - _windowSize, _height];

            for (var centerColumn = _windowSize; centerColumn < _width - _windowSize; centerColumn++)
            {
                for (var row = 0; row < _height; row++)
                {
                    var value = 0.0;
                    for (var nearbyColumn = centerColumn - _windowSize; nearbyColumn < centerColumn + _windowSize + 1; nearbyColumn++)
                    {
                        value += Gaussian(centerColumn - nearbyColumn, _standardDeviation) * sortedColumns[nearbyColumn, row];
                    }

                    finalHistogram[centerColumn, row] = (Pixel)value;
                }
            }

            return finalHistogram;
        }

        private void SpecifyColumn(Pixel[] processedImagePixels, uint currentColumn, Pixel[,] targetValues)
        {
            var columnSorted = LocalMidwayHistogram(processedImagePixels, currentColumn);

            for (var imageRow = 0; imageRow < _height; imageRow++)
            {
                var pixelValue = processedImagePixels[imageRow * _width + currentColumn];

                for (var targetRow = 0; targetRow < _height; targetRow++)
                {
                    if (pixelValue == columnSorted[targetRow])
                    {
                        processedImagePixels[imageRow * _width + currentColumn] = targetValues[currentColumn - _windowSize, targetRow];
                    }
                }
            }
        }

        private static double Gaussian(int x, double sigma)
        {
            return 1 / (sigma * Math.Sqrt(2 * Math.PI)) * Math.Exp(-Math.Pow(x, 2) / (2 * Math.Pow(sigma, 2)));
        }
    }
}