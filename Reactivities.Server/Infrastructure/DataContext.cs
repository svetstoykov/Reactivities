using Domain.Activities;
using Domain.Profiles;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
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

        public DbSet<Comment> Comments { get; set; }

        public DbSet<ProfileFollowing> ProfileFollowings { get; set; }

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

            modelBuilder.Entity<Profile>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Author)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProfileFollowing>(profileFollowing =>
            {
                profileFollowing.HasOne(o => o.Observer)
                    .WithMany(p => p.Followings)
                    .HasForeignKey(o => o.ObserverId)
                    .OnDelete(DeleteBehavior.Cascade);

                profileFollowing.HasOne(t => t.Target)
                    .WithMany(p => p.Followers)
                    .HasForeignKey(t => t.TargetId)
                    .OnDelete(DeleteBehavior.Restrict);

                profileFollowing.HasIndex(p => new {p.ObserverId, p.TargetId}).IsUnique();
            });
        }
    }
}