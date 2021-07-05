using Microsoft.EntityFrameworkCore;
using System.Linq;

#nullable disable

namespace WebApplication1.Context
{
    public partial class abTestdbContext : DbContext
    {
        public abTestdbContext(DbContextOptions<abTestdbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=DbConnection");
            }
        }
    }
}
