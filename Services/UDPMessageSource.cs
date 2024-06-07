using EFDatabase.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EFDatabase.Services
{
    public class UDPMessageSource : IMessageSource
    {
        public NetMessage Receive(IPEndPoint endPoint)
        {
            throw new NotImplementedException();
        }

        public void Send(NetMessage msg, IPEndPoint endPoint)
        {
            throw new NotImplementedException();
        }
    }
}
