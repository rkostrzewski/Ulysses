﻿using System;
using System.Linq;
using System.Windows.Media.Imaging;
using NUnit.Framework;
using Ulysses.App.Core.Exceptions;
using Ulysses.App.Modules.ImageDisplay.Commands;
using Ulysses.App.Modules.ImageDisplay.Models;
using Ulysses.Core.Models;

namespace Ulysses.App.Modules.ImageDisplay.Tests.Commands
{
    [TestFixture]
    public class SetOutputImageCommandTests
    {
        private IImageConverter _imageConverter;

        [SetUp]
        public void SetUp()
        {
            _imageConverter = new BitmapImageConverter();
        }

        [Test]
        public void ShouldSetOutputImageWhenInputImageWas8BppImage()
        {
            // Given
            BitmapSource outputImage = null;
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp8);
            var inputImage = new Image(new byte[] { 1, 2, 3, 4 }, imageModel);
            var command = new SetOutputImageCommand(_imageConverter) { OnImageUpdate = value => outputImage = value };

            // When
            command.Execute(inputImage);

            // Then
            Assert.IsTrue(BitmapImageIsSameAsImage(outputImage, inputImage));
        }

        [Test]
        public void ShouldSetOutputImageWhenInputImageWas12BppImage()
        {
            // Given
            BitmapSource outputImage = null;
            var imageModel = new ImageModel(2, 2, ImageBitDepth.Bpp12);
            var inputImage = new Image(new ushort[] { 0, 1024, 2048, 4095 }, imageModel);
            var expectedImage = new Image(new ushort[] { 0, 64, 128, 255 }, imageModel);
            var command = new SetOutputImageCommand(_imageConverter) { OnImageUpdate = value => outputImage = value };

            // When
            command.Execute(inputImage);

            // Then
            Assert.IsTrue(BitmapImageIsSameAsImage(outputImage, expectedImage));
        }

        [Test]
        public void ShouldSetOutputImageToNothingWhenInputImageWasNothing()
        {
            // Given
            BitmapSource outputImage = new BitmapImage();

            var command = new SetOutputImageCommand(_imageConverter) { OnImageUpdate = value => outputImage = value };

            // When
            command.Execute(null);

            // Then
            Assert.IsNull(outputImage);
        }

        [Test]
        public void ShouldNotAllowToExecuteWhenNotProvidedWayToSetImage()
        {
            // Given
            var command = new SetOutputImageCommand(_imageConverter) { OnImageUpdate = null };
            // When
            // Then
            Assert.IsFalse(command.CanExecute(null));
            Assert.Throws<CannotExecuteCommandException>(() => command.Execute(null));
        }

        private static bool BitmapImageIsSameAsImage(BitmapSource outputImage, Image inputImage)
        {
            var outputImagePixels = new byte[(int)(outputImage.Width * outputImage.Height)];
            outputImage.CopyPixels(outputImagePixels, (int)outputImage.Width, 0);

            return Math.Abs(outputImage.Width - inputImage.ImageModel.Width) < float.Epsilon &&
                   Math.Abs(outputImage.Height - inputImage.ImageModel.Height) < float.Epsilon &&
                   outputImagePixels.SequenceEqual(inputImage.ImagePixels.Select(p => (byte)p));
        }
    }
}