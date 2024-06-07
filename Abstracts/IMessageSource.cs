using System.Net;

namespace EFDatabase.Abstracts
{
    public interface IMessageSource
    {
        Task SendAsync(NetMessage msg, IPEndPoint endPoint);
        NetMessage Receive(IPEndPoint endPoint);
    }
}
