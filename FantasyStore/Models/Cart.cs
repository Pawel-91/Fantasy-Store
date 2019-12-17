using System.Collections.Generic;
using System.Linq;

namespace FantasyStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines => lineCollection;

        public virtual void AddItem(Product product, int quantity)
        {
            var lineItem = lineCollection.FirstOrDefault(cl =>
                cl.Product.ProductID == product.ProductID);

            if (lineItem == null)
            {
                lineCollection.Add(
                    new CartLine
                    {
                        Product = product,
                        Quantity = quantity
                    });
            }
            else
            {
                lineItem.Quantity += quantity;
            }
        }

        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(e => e.Product.Price * e.Quantity);

        public virtual void RemoveLine(Product product)
            => lineCollection.RemoveAll(lc => lc.Product.ProductID == product.ProductID);

        public virtual void Clear()
            => lineCollection.Clear();
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
