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
        public UDPServer(IMessageSource messageSource)
        {
            _messageSource = messageSource;
            _endPoint = new IPEndPoint(IPAddress.Any, 0);
        }
        async Task ProcessMessage(NetMessage message)
        {
            if (message == null) return;
            switch (message.Command)
            {
                case Command.Register:
                    await Register(message);
                    break;
                case Command.Message:
                    await Reply(message);
                    break;
                case Command.Confirmation:
                    await ConfirmMessageReceived(message.Id);
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
        private async Task Reply(NetMessage message)
        {
            if (clients.TryGetValue(message.To, out IPEndPoint ep))
            {
                int id = 0;
                using (var context = new ChatContext())
                {
                    var fromUser = context.Users.First(x => x.FullName == message.From);
                    var toUser = context.Users.First(x => x.FullName == message.To);
                    var msg = new Message
                    {
                        From = fromUser,
                        To = toUser,
                        IsSent = false,
                        Text = message.Text
                    };
                    context.Messages.Add(msg);
                    context.SaveChanges();
                    id = msg.Id;
                }
                message.Id = id;
                await _messageSource.SendAsync(message, ep);
                Console.WriteLine($"Message replied form : {message.From} to : {message.To}");
            }
            else Console.WriteLine("User not found");
        }
        async Task ConfirmMessageReceived(int id)
        {
            Console.WriteLine($"Message comfirmation id : {id}");
            using (var context = new ChatContext())
            {
                var msg = context.Messages.FirstOrDefault(x => x.Id == id);
                if (msg != null)
                {
                    msg.IsSent = true;
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task Listen()
        {
            Console.WriteLine("Server is waiting for message");

            while (_work)
            {
                try
                {
                    var msg = _messageSource.Receive(ref _endPoint);
                    Console.WriteLine(msg);
                    await ProcessMessage(msg);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex);
                }
            }
        }
        bool _work = true;
        public void Stop()
        {
            _work = false;
        }
    }
}
