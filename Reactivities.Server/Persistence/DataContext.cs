using Domain.Activities;
using Domain.Common.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<User>
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

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Host);
            
            modelBuilder.Entity<Activity>()
                .HasMany(a => a.Attendees)
                .WithMany(a => a.AttendingActivities);

            base.OnModelCreating(modelBuilder);
        }
    }
}
