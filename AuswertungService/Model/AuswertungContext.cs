using Microsoft.EntityFrameworkCore;

namespace AuswertungService.Model
{
    public class AuswertungContext : DbContext
    {
        public AuswertungContext(DbContextOptions<AuswertungContext> options)
            : base(options)
        {
        }
        public DbSet<Auswertung> Auswertungen { get; set; }
    }
}
