using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition.Tests.Camera.TestData;
using Ulysses.ImageProviders;
using Ulysses.ImageProviders.Camera;
using Ulysses.ImageProviders.Factories;
using Ulysses.ImageProviders.Templates;

namespace Ulysses.ImageAcquisition.Tests.IntegrationTests
{
    [TestFixture]
    public class CameraImageProviderIntegrationTests
    {
        [Test]
        [ExpectedException(typeof (SocketException))]
        public void ShouldThrowExceptionWhenNoImageIsSent()
        {
            // Given
            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp8);
            var template = new ImageProviderTemplate
            {
                ImageProviderType = ImageProviderType.CameraProvider,
                ImageModel = imageModel,
                CameraImageProviderTemplate = new CameraImageProviderTemplate { Port = 0, Timeout = 100 }
            };
            var cameraImageProvider = new ImageProviderFactory().CreateInstance(template);

            Image image;

            // When
            // Then
            cameraImageProvider.TryToObtainImage(out image);
        }

        [Test]
        public void ShouldReturnEmptyImageWhenTwoStartSequencesWhereSent()
        {
            // Given
            const int port = 8080;
            var ipEndpoint = new IPEndPoint(IPAddress.Parse(CameraImageProvider.LocalhostIpAddress), port);
            var udpSender = new UdpClient();
            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp8);
            var template = new ImageProviderTemplate
            {
                ImageProviderType = ImageProviderType.CameraProvider,
                ImageModel = imageModel,
                CameraImageProviderTemplate = new CameraImageProviderTemplate { Port = port, Timeout = 10000 }
            };
            var cameraImageProvider = new ImageProviderFactory().CreateInstance(template);

            Image image;
            // When
            Task.Run(() =>
            {
                while (true)
                {
                    udpSender.Send(Encoding.ASCII.GetBytes("START"), 5, ipEndpoint);
                }
            });

            cameraImageProvider.TryToObtainImage(out image);
            udpSender.Close();
            // Then

            var expectedImage = new Image(imageModel);
            Assert.AreEqual(expectedImage, image);
        }

        [Test]
        public void ShouldReceiveDummyImageWhenSentOne()
        {
            const int port = 8090;
            var ipEndpoint = new IPEndPoint(IPAddress.Parse(CameraImageProvider.LocalhostIpAddress), port);
            var udpSender = new UdpClient();
            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp8);
            var template = new ImageProviderTemplate
            {
                ImageProviderType = ImageProviderType.CameraProvider,
                ImageModel = imageModel,
                CameraImageProviderTemplate = new CameraImageProviderTemplate { Port = port, Timeout = 10000 }
            };
            var cameraImageProvider = new ImageProviderFactory().CreateInstance(template);

            Image image;
            // When

            Task.Run(() =>
            {
                var dummyImageEnumerator = TestDataSource.SimulateSendingOfDummyImage(imageModel).GetEnumerator();
                while (dummyImageEnumerator.MoveNext())
                {
                    var bytesToSend = dummyImageEnumerator.Current;
                    udpSender.Send(bytesToSend, bytesToSend.Length, ipEndpoint);
                }
            });

            cameraImageProvider.TryToObtainImage(out image);
            udpSender.Close();

            // Then
            var expectedImage = TestDataSource.GetExpectedDummyImage(imageModel);
            Assert.AreEqual(expectedImage, image);
        }
    }
}