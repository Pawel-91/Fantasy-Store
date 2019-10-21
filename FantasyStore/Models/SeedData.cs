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
                         Name = "D&D Player's Handbook",
                         Description = "Book with rules for players",
                         Category = "Core", Price = 160 
                    },
                    new Product
                    {
                        Name = "D&D Dungeon Master's Guide",
                        Description = "Book with rules for Dungeon Masters",
                        Category = "Core", Price = 160 
                    },
                    new Product 
                    {
                        Name = "D&D Monster Manual",
                        Description = "Monsters compendium",
                        Category = "Core", Price = 160
                    },
                    new Product 
                    {
                        Name = "Warhammer Fantasy Roleplay",
                        Description = "The Holy Warhammer",
                        Category = "Core", Price = 120
                    },
                    new Product 
                    {
                        Name = "Dusts of Middenheim",
                        Description = "Campaign of Middenheim city for first profession characters",
                        Category = "Adventures", Price = 80
                    },
                    new Product 
                    {
                        Name = "Towers of Altdorf",
                        Description = "Campaign of Altdorf city for second profession characters",
                        Category = "Adventures", Price = 80
                    },
                    new Product 
                    {
                        Name = "Anvils of Nuln",
                        Description = "Campaign of Nuln city for third profession characters",
                        Category = "Adventures", Price = 80
                    },
                    new Product 
                    {
                        Name = "Sunless Citadel",
                        Description = "D&D Adventure for 1-3 level characters",
                        Category = "Adventures", Price = 30
                    },
                    new Product 
                    {
                        Name = "Book of Psionic",
                        Description = "New rules for D&D psionic characters",
                        Category = "Expansion", Price = 1200
                    }
                );
            context.SaveChanges();
            }
        }
    }
}