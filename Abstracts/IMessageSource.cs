using System.Net;

namespace EFDatabase.Abstracts
{
    public interface IMessageSource
    {
        void Send(NetMessage msg, IPEndPoint endPoint);
        NetMessage Receive(IPEndPoint endPoint);
    }
}
