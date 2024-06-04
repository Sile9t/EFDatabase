
namespace EFDatabase
{
    public class User
    {
        public int Id {  get; set; }
        public string? FullName { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
