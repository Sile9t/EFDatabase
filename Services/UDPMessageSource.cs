﻿using EFDatabase.Abstracts;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EFDatabase.Services
{
    public class UDPMessageSource : IMessageSource
    {
        private readonly UdpClient _udpClient;
        public UDPMessageSource()
        {
            _udpClient = new UdpClient(12345);
        }
        public NetMessage Receive(ref IPEndPoint endPoint)
        {
            var buffer = _udpClient.Receive(ref endPoint);
            var text = Encoding.UTF8.GetString(buffer);
            return NetMessage.DeserializeFromJson(text) ?? new NetMessage();
        }

        public async Task SendAsync(NetMessage msg, IPEndPoint endPoint)
        {
            var text = msg.SerializeToJson();
            var buffer = Encoding.UTF8.GetBytes(text);
            await _udpClient.SendAsync(buffer);
        }
    }
}
