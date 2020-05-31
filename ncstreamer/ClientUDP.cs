using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ncstreamer
{
    class ClientUDP
    {
        public UdpClient udp;
        public IPEndPoint hostEndPoint;
        public string hostname;
        public int nport;
        
        public ClientUDP(string hostName, int port)
        {
            hostEndPoint = new IPEndPoint(IPAddress.Parse(hostName), port);
            udp = new UdpClient(hostName, port);
            hostname = hostName;
            nport = port;
        }

        public byte[] Send(byte[] bytes)
        {
            udp.Send(bytes, bytes.Length);
            return udp.Receive(ref hostEndPoint);
        }
    }
}
