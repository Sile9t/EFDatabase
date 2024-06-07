using EFDatabase.Abstracts;
using EFDatabase.Services;
using System.Net;

namespace EFDatabase
{
    public class Client
    {
        string _name;
        string _address;
        int _port;
        IMessageSource _messageSource;
        IPEndPoint _endPoint;
        public Client(string name, string address, int port)
        {
            _name = name;
            _address = address;
            _port = port;
            _messageSource = new UDPMessageSource();
            _endPoint = new IPEndPoint(IPAddress.Parse(_address), _port);
        }
        public async Task Start()
        {
            await Listen();
        }
        private async Task Listen()
        {
            while (true)
            {
                try
                {
                    var messageReceived = _messageSource.Receive(ref _endPoint);
                    Console.WriteLine($"Received message from '{messageReceived.From}':");
                    Console.WriteLine(messageReceived.Text);
                    await Confirm(messageReceived, _endPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        async Task Confirm(NetMessage msg, IPEndPoint endPoint)
        {
            msg.Command = Command.Confirmation;
            await _messageSource.SendAsync(msg, endPoint);
        }
        async Task Register(IPEndPoint endPoint)
        {
            var msg = new NetMessage(null, Command.Register, _name, null);
            await _messageSource.SendAsync(msg, _endPoint);
        }
        async Task Sender()
        {
            await Register(_endPoint);
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter recipient name:");
                    var recipientName = Console.ReadLine();
                    Console.WriteLine("Waiting for input...");
                    Console.WriteLine("Enter message:");
                    var text = Console.ReadLine();
                    var message = new NetMessage(text, Command.Message, _name, recipientName);
                    await _messageSource.SendAsync(message, _endPoint);
                    Console.WriteLine("Message sent");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
