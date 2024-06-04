using Microsoft.EntityFrameworkCore;

namespace EFDatabase
{
    public class ChatContext : DbContext
    {
        private readonly string connection = "Server=DESKTOP-U893DOI;Database=lessonDatabase;Trusted_Connection=True;";
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection).UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        public ChatContext()
        {
            
        }
    }
}
