using System.Text;
using Moq;
using NUnit.Framework;
using Ulysses.Core.Exceptions;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition.Remote;
using Ulysses.ImageAcquisition.Remote.Udp;
using Ulysses.ImageAcquisition.Tests.Camera.TestData;

namespace Ulysses.ImageAcquisition.Tests.Camera
{
    [TestFixture]
    public class CameraAcquisitionTests
    {
        [Test]
        public void ShouldReturnEmptyImageWhenNoPixelsWhereSent()
        {
            // Given
            var udpClientMock = new Mock<IUdpClient>();
            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp8);

            udpClientMock.Setup(uc => uc.Receive()).Returns(Encoding.ASCII.GetBytes("START"));
            var acquisition = new RemoteAcquisition(imageModel, udpClientMock.Object);

            // When
            Image actualImage;
            acquisition.TryToObtainImage(out actualImage);

            // Then
            var expectedImage = new Image(imageModel);

            Assert.AreEqual(expectedImage, actualImage);
        }

        [Test]
        public void ShouldReturnCorrectImageWhenUsing8BppModel()
        {
            // Given

            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp8);
            var udpClient = PrepareUdpClientMock(imageModel);

            var acquisition = new RemoteAcquisition(imageModel, udpClient);

            // When
            Image actualImage;
            acquisition.TryToObtainImage(out actualImage);

            // Then
            var expectedImage = TestDataSource.GetExpectedDummyImage(imageModel);

            Assert.AreEqual(expectedImage, actualImage);
        }

        [Test]
        public void ShouldReturnCorrectImageWhenUsing12BppModel()
        {
            // Given
            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp12);

            var udpClient = PrepareUdpClientMock(imageModel);

            var acquisition = new RemoteAcquisition(imageModel, udpClient);

            // When
            Image actualImage;
            acquisition.TryToObtainImage(out actualImage);

            // Then
            var expectedImage = TestDataSource.GetExpectedDummyImage(imageModel);

            Assert.AreEqual(expectedImage, actualImage);
        }

        [Test, ExpectedException(typeof (NotSupportedImageException))]
        public void ShouldThrowExceptionWhenUnsupportedParametersAreUsed()
        {
            // Given
            var imageModel = new ImageModel(1024, 1024, ImageBitDepth.Bpp8);

            var udpClient = PrepareUdpClientMock(imageModel);

            var acquisition = new RemoteAcquisition(imageModel, udpClient);

            // When
            // Then
            Image actualImage;
            acquisition.TryToObtainImage(out actualImage);
        }

        [Test, ExpectedException(typeof (ImageModelMismatchException))]
        public void ShouldThrowExceptionWhenDifferentImageIsReceivedThanRequested()
        {
            // Given
            var requestedImageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp8);
            var actualImageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp12);

            var udpClient = PrepareUdpClientMock(actualImageModel);

            var acquisition = new RemoteAcquisition(requestedImageModel, udpClient);

            // When
            // Then
            Image actualImage;
            acquisition.TryToObtainImage(out actualImage);
        }

        [Test]
        public void ShouldWaitUntilImagePixelsAreObtained()
        {
            // Given
            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp12);
            var packetSimulationEnumerator = TestDataSource.SimulateSendingOfDummyImageWithStartMidwayOfSendingAnImage(imageModel).GetEnumerator();

            var udpClientMock = new Mock<IUdpClient>();
            udpClientMock.Setup(r => r.Receive()).Returns(() =>
            {
                packetSimulationEnumerator.MoveNext();
                return packetSimulationEnumerator.Current;
            });

            var acquisition = new RemoteAcquisition(imageModel, udpClientMock.Object);

            // When
            Image actualImage;
            acquisition.TryToObtainImage(out actualImage);

            // Then
            var expectedImage = TestDataSource.GetExpectedDummyImage(imageModel);

            Assert.AreEqual(expectedImage, actualImage);
        }

        private static IUdpClient PrepareUdpClientMock(ImageModel imageModel)
        {
            var udpClientMock = new Mock<IUdpClient>();
            var packetSimulationEnumerator = TestDataSource.SimulateSendingOfDummyImage(imageModel).GetEnumerator();
            packetSimulationEnumerator.MoveNext();

            udpClientMock.Setup(r => r.Receive()).Returns(() =>
            {
                var packet = packetSimulationEnumerator.Current;
                packetSimulationEnumerator.MoveNext();
                return packet;
            });

            return udpClientMock.Object;
        }
    }
}