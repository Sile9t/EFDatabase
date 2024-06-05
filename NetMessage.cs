using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EFDatabase
{
    public class NetMessage
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public NetMessage(string txt, string from, string to)
        {
            Text = txt;
            Date = DateTime.Now;
            From = from;
            To = to;
        }
        public string SerializeToJson() => JsonSerializer.Serialize(this);
        public static NetMessage? DeserializeFromJson(string msg) 
            => JsonSerializer.Deserialize<NetMessage>(msg);
        public override string ToString()
        {
            return $"{Date} From: {From} To: {To} Text: {Text}";
        }
    }
}
