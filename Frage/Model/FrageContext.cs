using Microsoft.EntityFrameworkCore;

namespace FrageService.Model
{
    public class FrageContext : DbContext
    {
        public FrageContext(DbContextOptions<FrageContext> frage)
            : base(frage)
        {
        }
        public DbSet<Frage> Fragen { get; set; } = null!;
    }
}
