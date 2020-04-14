using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace FantasyStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();
            if(!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product 
                    {
                         Name = "DeeDee Player's Hand book",
                         Description = "Book with rules for players",
                         Category = "Core", Price = 160 
                    },
                    new Product
                    {
                        Name = "DeeDee Dungeon Master's Guile",
                        Description = "Book with rules for Dungeon Masters",
                        Category = "Core", Price = 160 
                    },
                    new Product 
                    {
                        Name = "DeeDee Manual Monster",
                        Description = "Monsters compendium",
                        Category = "Core", Price = 160
                    },
                    new Product 
                    {
                        Name = "Lolhammer Fantasy Loleplay",
                        Description = "The Holy Warhammer",
                        Category = "Core", Price = 120
                    },
                    new Product 
                    {
                        Name = "Dusts of Middleheim",
                        Description = "Campaign of Middleheim city for first profession characters",
                        Category = "Adventures", Price = 80
                    },
                    new Product 
                    {
                        Name = "Towers of Olddorf",
                        Description = "Campaign of Oltdorf city for second profession characters",
                        Category = "Adventures", Price = 80
                    },
                    new Product 
                    {
                        Name = "Anvils of Null",
                        Description = "Campaign of Null city for third profession characters",
                        Category = "Adventures", Price = 80
                    },
                    new Product 
                    {
                        Name = "Sunless Citydel",
                        Description = "DeeDee Adventure for 1-3 level characters",
                        Category = "Adventures", Price = 30
                    },
                    new Product 
                    {
                        Name = "Book of Paranoic",
                        Description = "New rules for DeeDee Paranoic characters",
                        Category = "Expansion", Price = 100
                    }
                );
            context.SaveChanges();
            }
        }
    }
}