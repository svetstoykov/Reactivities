using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Identity.Models;
using Domain.Activities;
using Domain.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Enumerations;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<User> userManager)
        {
            await SeedUsers(userManager);
            await SeedCategories(context);
            await SeedActivities(context, userManager);

            await context.SaveChangesAsync();
        }

        private static async Task SeedUsers(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var profiles = new List<Profile>()
                {
                    Profile.New("mikescott", "mikescott@dundermifflin.com", "Michael Scott"),
                    Profile.New("dwightshrute", "dwightshrute@dundermifflin.com", "Dwight Shrute"),
                    Profile.New("pambeasley", "pambeasley@dundermifflin.com", "Pam Beasley")
                };

                var users = profiles.Select(profile => 
                    new User {Email = profile.Email, UserName = profile.UserName, Profile = profile}).ToList();

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "P@ssW0rd");
                }
            }
        }

        private static async Task SeedCategories(DataContext context)
        {
            if (context.Categories.Any())
                return;

            var categories = Enum.GetValues<CategoryType>()
                .Select(c => new Category
                {
                    Id = (int)c,
                    Name = c.ToString()
                })
                .ToList();

            await context.Categories.AddRangeAsync(categories);
        }

        private static async Task SeedActivities(DataContext context, UserManager<User> userManager)
        {
            if (context.Activities.Any())
                return;

            var profiles = await userManager.Users
                .Select(u => u.Profile)
                .ToListAsync();

            var random = new Random();

            var activities = new List<Activity>
            {
                new()
                {
                    Title = "Past Activity 1",
                    Date = DateTime.Now.AddMonths(-2),
                    Description = "Activity 2 months ago",
                    CategoryId = (int) CategoryType.Culture,
                    City = "London",
                    Venue = "Pub",
                    HostId = profiles[random.Next(0, profiles.Count - 1)].Id
                },
                new()
                {
                    Title = "Past Activity 2",
                    Date = DateTime.Now.AddMonths(-1),
                    Description = "Activity 1 month ago",
                    CategoryId = (int) CategoryType.Film,
                    City = "Paris",
                    Venue = "Louvre",
                    HostId = profiles[random.Next(0, profiles.Count - 1)].Id
                },
                new()
                {
                    Title = "Future Activity 1",
                    Date = DateTime.Now.AddMonths(1),
                    Description = "Activity 1 month in future",
                    CategoryId = (int) CategoryType.Food,
                    City = "London",
                    Venue = "Natural History Museum",
                    HostId = profiles[random.Next(0, profiles.Count - 1)].Id
                },
                new()
                {
                    Title = "Future Activity 2",
                    Date = DateTime.Now.AddMonths(2),
                    Description = "Activity 2 months in future",
                    CategoryId = (int) CategoryType.Drinks,
                    City = "London",
                    Venue = "O2 Arena",
                    HostId = profiles[random.Next(0, profiles.Count - 1)].Id
                },
                new()
                {
                    Title = "Future Activity 3",
                    Date = DateTime.Now.AddMonths(3),
                    Description = "Activity 3 months in future",
                    CategoryId = (int) CategoryType.Drinks,
                    City = "London",
                    Venue = "Another pub",
                    HostId = profiles[random.Next(0, profiles.Count - 1)].Id
                },
                new()
                {
                    Title = "Future Activity 4",
                    Date = DateTime.Now.AddMonths(4),
                    Description = "Activity 4 months in future",
                    CategoryId = (int) CategoryType.Music,
                    City = "London",
                    Venue = "Yet another pub",
                    HostId = profiles[random.Next(0, profiles.Count - 1)].Id
                },
                new()
                {
                    Title = "Future Activity 5",
                    Date = DateTime.Now.AddMonths(5),
                    Description = "Activity 5 months in future",
                    CategoryId = (int) CategoryType.Music,
                    City = "London",
                    Venue = "Just another pub",
                    HostId = profiles[random.Next(0, profiles.Count - 1)].Id
                },
                new()
                {
                    Title = "Future Activity 6",
                    Date = DateTime.Now.AddMonths(6),
                    Description = "Activity 6 months in future",
                    CategoryId = (int) CategoryType.Music,
                    City = "London",
                    Venue = "Roundhouse Camden",
                    HostId = profiles[random.Next(0, profiles.Count - 1)].Id
                },
                new()
                {
                    Title = "Future Activity 7",
                    Date = DateTime.Now.AddMonths(7),
                    Description = "Activity 2 months ago",
                    CategoryId = (int) CategoryType.Travel,
                    City = "London",
                    Venue = "Somewhere on the Thames",
                    HostId = profiles[random.Next(0, profiles.Count - 1)].Id
                },
                new()
                {
                    Title = "Future Activity 8",
                    Date = DateTime.Now.AddMonths(8),
                    Description = "Activity 8 months in future",
                    CategoryId = (int) CategoryType.Film,
                    City = "London",
                    Venue = "Cinema",
                    HostId = profiles[random.Next(0, profiles.Count - 1)].Id
                }
            };

            await context.Activities.AddRangeAsync(activities);
        }
    }
}