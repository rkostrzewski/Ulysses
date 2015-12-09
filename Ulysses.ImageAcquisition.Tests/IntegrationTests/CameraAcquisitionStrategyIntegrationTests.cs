using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ulysses.Core.ImageProcessing;
using Ulysses.Core.Models;
using Ulysses.ImageAcquisition.Factories;
using Ulysses.ImageAcquisition.Tests.Camera.TestData;

namespace Ulysses.ImageAcquisition.Tests.IntegrationTests
{
    [TestFixture]
    public class CameraAcquisitionIntegrationTests
    {
        [Test]
        [ExpectedException(typeof (SocketException))]
        public void ShouldThrowExceptionWhenNoImageIsSent()
        {
            // Given
            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp8);

            var cameraAcquisition = CameraAcquisitionFactory.CreateInstance(new IPEndPoint(IPAddress.Any, 0), 100, imageModel);
            Image image;

            // When
            // Then
            cameraAcquisition.TryToObtainImage(out image);
        }

        [Test]
        public void ShouldReturnEmptyImageWhenTwoStartSequencesWhereSent()
        {
            // Given
            var ipEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            var udpSender = new UdpClient();
            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp8);

            var cameraAcquisition = CameraAcquisitionFactory.CreateInstance(ipEndpoint, 10000, imageModel);

            Image image;
            // When
            Task.Run(() =>
            {
                while (true)
                {
                    udpSender.Send(Encoding.ASCII.GetBytes("START"), 5, ipEndpoint);
                }
            });

            cameraAcquisition.TryToObtainImage(out image);
            udpSender.Close();
            // Then

            var expectedImage = new Image(imageModel);
            Assert.AreEqual(expectedImage, image);
        }

        [Test]
        public void ShouldReceiveDummyImageWhenSentOne()
        {
            // Given
            var ipEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8090);
            var udpSender = new UdpClient();
            var imageModel = new ImageModel(1024, 768, ImageBitDepth.Bpp8);
            
            var cameraAcquisition = CameraAcquisitionFactory.CreateInstance(ipEndpoint, 10000, imageModel);

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

            cameraAcquisition.TryToObtainImage(out image);
            udpSender.Close();

            // Then
            var expectedImage = TestDataSource.GetExpectedDummyImage(imageModel);
            Assert.AreEqual(expectedImage, image);
        }
    }
}