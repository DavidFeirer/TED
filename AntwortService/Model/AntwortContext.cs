using Microsoft.EntityFrameworkCore;

namespace AntwortService.Model
{
    public class AntwortContext : DbContext
    {
        public AntwortContext(DbContextOptions<AntwortContext> options)
            : base(options)
        {
        }
        public DbSet<Antwort> Antworten { get; set; }
    }
}
