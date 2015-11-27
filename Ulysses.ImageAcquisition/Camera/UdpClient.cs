using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Ulysses.ImageAcquisition.Camera
{
    internal sealed class UdpClient : System.Net.Sockets.UdpClient, IUdpClient
    {
        private IPEndPoint _ipEndPoint;

        internal UdpClient(IPEndPoint ipEndPoint, int timeout)
        {
            _ipEndPoint = ipEndPoint;

            Client.ReceiveTimeout = timeout;
            Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            Client.Bind(ipEndPoint);
        }

        public IEnumerable<byte> Receive()
        {
            return Receive(ref _ipEndPoint);
        }
    }
}