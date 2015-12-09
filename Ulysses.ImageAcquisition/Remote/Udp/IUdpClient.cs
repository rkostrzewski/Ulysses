using System.Collections.Generic;

namespace Ulysses.ImageAcquisition.Remote.Udp
{
    public interface IUdpClient
    {
        IEnumerable<byte> Receive();

        void Close();
    }
}