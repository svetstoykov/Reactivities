using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Identity.Models;
using Domain.Activities;
using Domain.Activities.Enums;
using Domain.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                    new User { Email = profile.Email, UserName = profile.UserName, Profile = profile }).ToList();

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
                .Select(Category.New)
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
                Activity.New(
                    "Past Activity 1", 
                    DateTime.Now.AddMonths(-2),
                    "Activity 2 months ago", 
                    "London",
                    "Pub",  
                    (int) CategoryType.Culture,
                    profiles[random.Next(0, profiles.Count)].Id),
                Activity.New(
                    "Future Activity 1",
                    DateTime.Now.AddMonths(+2),
                    "Activity in 2 months",
                    "London",
                    "Pub",
                    (int) CategoryType.Drinks,
                    profiles[random.Next(0, profiles.Count)].Id),
                Activity.New(
                    "Past Activity 2",
                    DateTime.Now.AddMonths(-3),
                    "Activity 3 months ago",
                    "Vienna",
                    "Movie",
                    (int) CategoryType.Film,
                    profiles[random.Next(0, profiles.Count)].Id),
                Activity.New(
                    "Future Activity 2",
                    DateTime.Now.AddMonths(+3),
                    "Activity in 3 months",
                    "London",
                    "Club",
                    (int) CategoryType.Music,
                    profiles[random.Next(0, profiles.Count)].Id),
                Activity.New(
                    "Future Activity 4",
                    DateTime.Now.AddMonths(+5),
                    "Activity in 5 months",
                    "Paris",
                    "Club",
                    (int) CategoryType.Music,
                    profiles[random.Next(0, profiles.Count)].Id),
                Activity.New(
                    "Past Activity 3",
                    DateTime.Now.AddMonths(-2),
                    "Activity 2 months ago",
                    "Sofia",
                    "Pub",
                    (int) CategoryType.Culture,
                    profiles[random.Next(0, profiles.Count)].Id),
                Activity.New(
                    "Future Activity 6",
                    DateTime.Now.AddMonths(+8),
                    "Activity in 8 months",
                    "Oslo",
                    "Dinner",
                    (int) CategoryType.Food,
                    profiles[random.Next(0, profiles.Count)].Id),
                Activity.New(
                    "Future Activity 11",
                    DateTime.Now.AddMonths(+8),
                    "Activity in 8 months",
                    "Liverpool",
                    "Museum",
                    (int) CategoryType.Travel,
                    profiles[random.Next(0, profiles.Count)].Id),
                Activity.New(
                    "Past Activity 9",
                    DateTime.Now.AddMonths(-2),
                    "Activity 2 months ago",
                    "London",
                    "Museum",
                    (int) CategoryType.Culture,
                    profiles[random.Next(0, profiles.Count)].Id),
                Activity.New(
                    "Past Activity 12",
                    DateTime.Now.AddMonths(-5),
                    "Activity 2 months ago",
                    "London",
                    "Park",
                    (int) CategoryType.Travel,
                    profiles[random.Next(0, profiles.Count)].Id),
                Activity.New(
                    "Future Activity 15",
                    DateTime.Now.AddMonths(+1),
                    "Activity in 1 month",
                    "Manchester",
                    "Museum",
                    (int) CategoryType.Travel,
                    profiles[random.Next(0, profiles.Count)].Id),
            };

            await context.Activities.AddRangeAsync(activities);
        }
    }
}