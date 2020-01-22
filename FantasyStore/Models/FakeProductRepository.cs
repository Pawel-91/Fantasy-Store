using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyStore.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products { get; private set; } =
        new List<Product>
        {
            new Product { ProductID = 1, Category = "Books", Name = "D&D Player's Handbook", Price = 40},
            new Product { ProductID = 2, Category = "Books", Name = "D&D Dungeon Master's Guide", Price = 40},
            new Product { ProductID = 3, Category = "Books", Name = "D&D Monster Manual", Price = 40},

        }.AsQueryable();

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                product.ProductID = Products.Count() + 1;
                Products = Products.Append(product);
            }
            else
            {
                List<Product> entries = Products.ToList();

                Product entry = entries
                    .FirstOrDefault(p => p.ProductID == product.ProductID);

                if (entry != null)
                {
                    entry.Name = product.Name;
                    entry.Description = product.Description;
                    entry.Category = product.Category;
                    entry.Price = product.Price;

                    Products = entries.AsQueryable();
                }
            }
        }
    }
}
