using EFDatabase.Abstracts;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EFDatabase.Services
{
    public class UDPServer
    {
        Dictionary<string, IPEndPoint> clients = new Dictionary<string, IPEndPoint>();
        private readonly IMessageSource _messageSource;
        private IPEndPoint _endPoint;
        public UDPServer()
        {
            _messageSource = new UDPMessageSource();
            _endPoint = new IPEndPoint(IPAddress.Any, 0);
        }
        async Task ProcessMessage(NetMessage message)
        {
            switch (message.Command)
            {
                case Command.Register:
                    await Register(message);
                    break;
                case Command.Message:
                    break;
                case Command.Confirmation:
                    break;
            }
        }
        private async Task Register(NetMessage message)
        {
            Console.WriteLine($"Message register {message.From}");
            if (clients.TryAdd(message.From, _endPoint))
            {
                using (var context = new ChatContext())
                {
                    context.Users.Add(new User { FullName = message.From });
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task Listen()
        {
            Console.WriteLine("Server is waiting for message");

            CancellationTokenSource cts = new CancellationTokenSource();
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    var msg = _messageSource.Receive(_endPoint);
                    Console.WriteLine(msg);
                    await ProcessMessage(msg);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
