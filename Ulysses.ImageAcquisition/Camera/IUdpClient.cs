using System.Collections.Generic;

namespace Ulysses.ImageAcquisition.Camera
{
    public interface IUdpClient
    {
        IEnumerable<byte> Receive();

        void Close();
    }
}