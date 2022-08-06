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
            BuildActivityEntity(modelBuilder);
            BuildProfileEntity(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void BuildActivityEntity(ModelBuilder modelBuilder)
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
            
            modelBuilder.Entity<Activity>()
                .HasMany(a => a.Attendees)
                .WithMany(a => a.AttendingActivities);
        }

        private static void BuildProfileEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>()
                .HasIndex(p => p.UserName)
                .IsUnique();

            modelBuilder.Entity<Profile>()
                .Property(p => p.UserName)
                .IsRequired();

            modelBuilder.Entity<Profile>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Profile>()
                .Property(p => p.Email)
                .IsRequired();
        }
    }
}
