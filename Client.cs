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
        public Client(string name, string address, int port)
        {
            _name = name;
            _address = address;
            _port = port;
            _messageSource = new UDPMessageSource();
        }
        void Start()
        {

        }
        private async Task Listen()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(_address), _port);
            while (true)
            {
                try
                {
                    var messageReceived = _messageSource.Receive(ref endPoint);
                    Console.WriteLine($"Received message from '{messageReceived.From}':");
                    Console.WriteLine(messageReceived.Text);
                    await Confirm(messageReceived, endPoint);
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
    }
}
