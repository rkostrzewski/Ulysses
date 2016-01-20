using System.Collections.Generic;

namespace Ulysses.ImageProviders.Camera.Udp
{
    public interface IUdpClient
    {
        IEnumerable<byte> Receive();

        void Close();
    }
}