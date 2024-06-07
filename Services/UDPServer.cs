using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EFDatabase.Services
{
    public class UDPServer
    {
        public async Task Listen()
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine("Server is waiting for message");

            CancellationTokenSource cts = new CancellationTokenSource();
            while (!cts.IsCancellationRequested)
            {
                //receiving msg
                var buffer = udpClient.Receive(ref endPoint);
                //decoding msg
                var userMsg = Encoding.UTF8.GetString(buffer);
                //encoding reply msg
                var reply = Encoding.UTF8.GetBytes("Message received");
                //callback to user
                await udpClient.SendAsync(reply, endPoint);
                //getting string from user msg
                var message = NetMessage.DeserializeFromJson(userMsg);
                //reply cancelling
                if (message.Text.ToLower().Equals("exit")) cts.Cancel();
                Console.WriteLine(message);
            }
        }
    }
}
