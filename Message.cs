using System.Data;

namespace EFDatabase
{
    public class Message
    {
        public int Id { get; set; }
        public string Text {  get; set; }
        public DataSetDateTime Date { get; set; }
        public bool IsSent {  get; set; }
        public User From { get; set; }
        public User To { get; set; }
    }
}
