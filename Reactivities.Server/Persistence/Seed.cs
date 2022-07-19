using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Models.Enumerations;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            await SeedCategories(context);
            await SeedActivities(context);
            
            await context.SaveChangesAsync();
        }

        private static async Task SeedCategories(DataContext context)
        {
            if(context.Categories.Any())
                return;

            var categories = Enum.GetValues<CategoryType>()
                .Select(c => new Category
                {
                    Id = (int) c,
                    Name = c.ToString()
                })
                .ToList();

            await context.Categories.AddRangeAsync(categories);
        }

        private static async Task SeedActivities(DataContext context)
        {
            if (context.Activities.Any()) 
                return;

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
                },
                new()
                {
                    Title = "Past Activity 2",
                    Date = DateTime.Now.AddMonths(-1),
                    Description = "Activity 1 month ago",
                    CategoryId = (int) CategoryType.Film,
                    City = "Paris",
                    Venue = "Louvre",
                },
                new()
                {
                    Title = "Future Activity 1",
                    Date = DateTime.Now.AddMonths(1),
                    Description = "Activity 1 month in future",
                    CategoryId = (int) CategoryType.Food,
                    City = "London",
                    Venue = "Natural History Museum",
                },
                new()
                {
                    Title = "Future Activity 2",
                    Date = DateTime.Now.AddMonths(2),
                    Description = "Activity 2 months in future",
                    CategoryId = (int) CategoryType.Drinks,
                    City = "London",
                    Venue = "O2 Arena",
                },
                new()
                {
                    Title = "Future Activity 3",
                    Date = DateTime.Now.AddMonths(3),
                    Description = "Activity 3 months in future",
                    CategoryId = (int) CategoryType.Drinks,
                    City = "London",
                    Venue = "Another pub",
                },
                new()
                {
                    Title = "Future Activity 4",
                    Date = DateTime.Now.AddMonths(4),
                    Description = "Activity 4 months in future",
                    CategoryId = (int) CategoryType.Music,
                    City = "London",
                    Venue = "Yet another pub",
                },
                new()
                {
                    Title = "Future Activity 5",
                    Date = DateTime.Now.AddMonths(5),
                    Description = "Activity 5 months in future",
                    CategoryId = (int) CategoryType.Music,
                    City = "London",
                    Venue = "Just another pub",
                },
                new()
                {
                    Title = "Future Activity 6",
                    Date = DateTime.Now.AddMonths(6),
                    Description = "Activity 6 months in future",
                    CategoryId = (int) CategoryType.Music,
                    City = "London",
                    Venue = "Roundhouse Camden",
                },
                new()
                {
                    Title = "Future Activity 7",
                    Date = DateTime.Now.AddMonths(7),
                    Description = "Activity 2 months ago",
                    CategoryId = (int) CategoryType.Travel,
                    City = "London",
                    Venue = "Somewhere on the Thames",
                },
                new()
                {
                    Title = "Future Activity 8",
                    Date = DateTime.Now.AddMonths(8),
                    Description = "Activity 8 months in future",
                    CategoryId = (int) CategoryType.Film,
                    City = "London",
                    Venue = "Cinema",
                }
            };

            await context.Activities.AddRangeAsync(activities);
        }
    }
}