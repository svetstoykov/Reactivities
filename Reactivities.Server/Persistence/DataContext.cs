using Application.Common.Identity.Models;
using Domain.Activities;
using Domain.Profiles;
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

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Category);

            modelBuilder.Entity<Category>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Host)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne<Profile>()
                .WithOne()
                .HasForeignKey<User>(u => u.UserName)
                .HasPrincipalKey<Profile>(p => p.UserName)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasOne<Profile>()
                .WithOne()
                .HasForeignKey<User>(u => u.Email)
                .HasPrincipalKey<Profile>(p => p.Email)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Activity>()
                .HasMany(a => a.Attendees)
                .WithMany(a => a.AttendingActivities);

            base.OnModelCreating(modelBuilder);
        }
    }
}
