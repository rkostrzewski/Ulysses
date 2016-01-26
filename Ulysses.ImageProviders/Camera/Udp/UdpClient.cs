using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Ulysses.ImageProviders.Camera.Udp
{
    internal sealed class UdpClient : System.Net.Sockets.UdpClient, IUdpClient
    {
        private IPEndPoint _ipEndPoint;

        internal UdpClient(IPEndPoint ipEndPoint, int timeoutInMilliseconds)
        {
            _ipEndPoint = ipEndPoint;

            Client.ReceiveTimeout = timeoutInMilliseconds;
            Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            Client.Bind(ipEndPoint);
        }

        public IEnumerable<byte> Receive()
        {
            return Receive(ref _ipEndPoint);
        }
    }
}