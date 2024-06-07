using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EFDatabase.Abstracts
{
    public interface IMessageSource
    {
        void Send(NetMessage msg, IPEndPoint endPoint);
        NetMessage Receive(IPEndPoint endPoint);
    }
}
