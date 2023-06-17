using Microsoft.EntityFrameworkCore;

namespace FragebogenService.Model
{
    public class FragebogenContext : DbContext
    {
        public FragebogenContext(DbContextOptions<FragebogenContext> fragebogen)
            : base(fragebogen)
        {
        }

        public DbSet<Fragebogen> Frageboegen { get; set; } = null!;
    }
}