using System.Linq;

namespace FantasyStore.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
