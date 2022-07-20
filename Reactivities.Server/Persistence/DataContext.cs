using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Category);

            modelBuilder.Entity<Category>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

            base.OnModelCreating(modelBuilder);
        }
    }
}
