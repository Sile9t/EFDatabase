using EFDatabase.Abstracts;
using EFDatabase.Services;

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
    }
}
