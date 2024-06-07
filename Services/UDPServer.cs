using EFDatabase.Abstracts;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EFDatabase.Services
{
    public class UDPServer
    {
        private readonly IMessageSource _messageSource;
        public UDPServer()
        {
            _messageSource = new UDPMessageSource();
        }
        void ProcessMessage(NetMessage message)
        {
            switch (message.Command)
            {
                case Command.Register:
                    break;
                case Command.Message:
                    break;
                case Command.Confirmation:
                    break;
            }
        }
        public async Task Listen()
        {
            //UdpClient udpClient = new UdpClient(12345);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine("Server is waiting for message");

            CancellationTokenSource cts = new CancellationTokenSource();
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    var msg = _messageSource.Receive(endPoint);
                    Console.WriteLine(msg);
                    ProcessMessage(msg);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
