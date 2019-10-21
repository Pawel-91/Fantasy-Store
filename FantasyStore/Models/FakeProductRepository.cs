using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyStore.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product { Category = "Books", Name = "D&D Player's Handbook", Price = 40},
            new Product { Category = "Books", Name = "D&D Dungeon Master's Guide", Price = 40},
            new Product { Category = "Books", Name = "D&D Monster Manual", Price = 40},

        }.AsQueryable();
    }
}
