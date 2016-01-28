using System.IO;
using System.Linq;
using NUnit.Framework;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition.Tests.FileSystem.TestData;
using Ulysses.ImageProviders;
using Ulysses.ImageProviders.Factories;
using Ulysses.ImageProviders.FileSystem;
using Ulysses.ImageProviders.Templates;

namespace Ulysses.ImageAcquisition.Tests.FileSystem
{
    [TestFixture]
    public class FileSystemAcquisitionTests
    {
        [Test, ExpectedException(typeof (ImageModelMismatchException))]
        public void ShouldThrowExceptionWhenImageModelIsDifferentThanActual()
        {
            // Given
            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp8);
            var imageProvider = new FileSystemImageProvider(imageModel, new[] { TestDataSource.Bpp1Black512X512ImagePath }, false);

            // When
            // Then
            Image image;
            imageProvider.TryToObtainImage(out image);
        }

        [Test]
        public void ShouldNotObtainImageProvidedNoFiles()
        {
            // Given
            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp8);
            var imageProvider = new FileSystemImageProvider(imageModel, new string[] { }, false);

            // When
            Image image;
            var result = imageProvider.TryToObtainImage(out image);

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase(TestDataSource.Bpp1Black512X512ImagePath, 512, 0)]
        [TestCase(TestDataSource.Bpp1White512X512ImagePath, 512, 255)]
        [TestCase(TestDataSource.Bpp4Gray512X512ImagePath, 512, 128)]
        [TestCase(TestDataSource.Bpp8Gray1024X1024ImagePath, 1024, 125)]
        [TestCase(TestDataSource.Bpp32Gray1024X1024ImagePath, 1024, 200)]
        [TestCase(TestDataSource.Bpp32Color1024X1024ImagePath, 1024, 81)]
        public void ShouldReturnCorrectImageWhenProvidedImageModelWith8BytesPerPixelBitDepth(string imagePath, int imageSize, int pixelValue)
        {
            // Given
            var absoluteFilePath = Path.GetFullPath(imagePath);
            var imageModel = new ImageModel((ushort)imageSize, (ushort)imageSize, ImageBitDepth.Bpp8);
            var template = new ImageProviderTemplate
            {
                ImageProviderType = ImageProviderType.FileSystemProvider,
                FileSystemImageProviderTemplate =
                    new FileSystemImageProviderTemplate { FolderPath = Path.GetDirectoryName(absoluteFilePath), FileSearchPattern = Path.GetFileName(absoluteFilePath) },
                ImageModel = imageModel
            };

            var imageProvider = new ImageProviderFactory().CreateInstance(template);

            // When
            Image image;
            imageProvider.TryToObtainImage(out image);

            // Then
            var expectedImagePixels = Enumerable.Range(0, imageModel.Width * imageModel.Height).Select(p => (Pixel)pixelValue);
            var expectedImage = new Image(expectedImagePixels, imageModel);
            Assert.AreEqual(expectedImage, image);
        }

        [Test]
        [TestCase(TestDataSource.RawBpp12512X512BlackImagePath, 512, 512, 0, 0)]
        [TestCase(TestDataSource.RawBpp121024X1024WhiteImagePath, 1024, 1024, 4095, 4095)]
        [TestCase(TestDataSource.RawBpp12256X512CheckeredImagePath, 256, 512, 1024, 2048)]
        public void ShouldReturnCorrectImageWhenProvidedImageModelWith12BytesPerPixelBitDepth(string imagePath, int imageWidth, int imageHeight, int evenPixelValue, int oddPixelValue)
        {
            // Given
            var absoluteFilePath = Path.GetFullPath(imagePath);
            var imageModel = new ImageModel((ushort)imageWidth, (ushort)imageHeight, ImageBitDepth.Bpp12);
            var template = new ImageProviderTemplate
            {
                ImageProviderType = ImageProviderType.FileSystemProvider,
                FileSystemImageProviderTemplate =
                    new FileSystemImageProviderTemplate { FolderPath = Path.GetDirectoryName(absoluteFilePath), FileSearchPattern = Path.GetFileName(absoluteFilePath) },
                ImageModel = imageModel
            };

            var imageProvider = new ImageProviderFactory().CreateInstance(template);

            // When
            Image image;
            imageProvider.TryToObtainImage(out image);

            // Then
            var expectedImagePixels = Enumerable.Range(0, imageModel.Width * imageModel.Height).Select(p => p % 2 == 0 ? (Pixel)evenPixelValue : (Pixel)oddPixelValue);
            var expectedImage = new Image(expectedImagePixels, imageModel);
            Assert.AreEqual(expectedImage, image);
        }
    }
}