using System.Linq;
using NUnit.Framework;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition.FileSystem;
using Ulysses.ImageAcquisition.Tests.FileSystem.TestData;

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
            var imageAcquisition = new FileSystemAcquisition(imageModel, new[] { TestDataSource.Bpp1Black512X512ImagePath });

            // When
            // Then
            Image image;
            imageAcquisition.TryToObtainImage(out image);
        }

        [Test]
        public void ShouldNotObtainImageProvidedNoFiles()
        {
            // Given
            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp8);
            var imageAcquisition = new FileSystemAcquisition(imageModel, new string[] { });

            // When
            Image image;
            var result = imageAcquisition.TryToObtainImage(out image);

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
        public void ShouldReturnCorrectImageWhenProvidedImageBitDepth(string imagePath, int imageSize, int pixelValue)
        {
            // Given
            var imageModel = new ImageModel(imageSize, imageSize, ImageBitDepth.Bpp8);
            var imageAcquisition = new FileSystemAcquisition(imageModel, new[] { imagePath });

            // When
            Image image;
            imageAcquisition.TryToObtainImage(out image);

            // Then
            var expectedImagePixels = Enumerable.Range(0, imageModel.Width * imageModel.Height).Select(p => (Pixel)pixelValue);
            var expectedImage = new Image(expectedImagePixels, imageModel);
            Assert.AreEqual(expectedImage, image);
        }
    }
}