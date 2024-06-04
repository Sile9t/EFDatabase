using Microsoft.EntityFrameworkCore;

namespace EFDatabase
{
    public class ChatContext : DbContext
    {
        private readonly string connection = "Server=localhost;Database=lessonDatabase;Trusted_Connection=True;";
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection).UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                //key connecting
                entity.HasKey(x => x.Id).HasName("user_pk");
                entity.ToTable("users");
                //fields
                entity.Property(e => e .FullName).HasColumnName("FullName").HasMaxLength(255);
            });
            modelBuilder.Entity<Message>(entity =>
            {
                //key connecting
                entity.HasKey(x => x.Id).HasName("message_pk");
                entity.ToTable("messages");
                //fields
                entity.Property(e => e.Text).HasColumnType("message_text");
                entity.Property(e => e.Date).HasColumnType("message_date");
                entity.Property(e => e.IsSent).HasColumnType("is_sent");
                entity.Property(e => e.Id).HasColumnType("id");
                //connections
                entity.HasOne(x => x.To).WithMany(m => m.MessagesTo);
                entity.HasOne(x => x.From).WithMany(m => m.MessagesFrom);
            });
        }
        public ChatContext()
        {
            
        }
    }
}
