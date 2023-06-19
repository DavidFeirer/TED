using Microsoft.EntityFrameworkCore;

namespace QueueService.Model
{
    public class MessageContext<T> : DbContext 
    {
        public MessageContext(DbContextOptions<MessageContext<T>> options)
            : base(options)
        {
        }
        public DbSet<Message<T>> Messages { get; set; } = null!;
    }
}
