using System.Linq;

namespace FantasyStore.Models
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
