namespace CoverGo.PairProgramming.Domain.Models
{
    public class ShoppingCart
    {
        public readonly List<Product> items;

        public ShoppingCart()
        {
            items = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            items.Add(product);
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (Product item in items)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }

        public void ApplyDiscount(string productName, int quantity)
        {
            Product product = items.Find(p => p.Name.Equals(productName));
            if (product != null && product.Quantity >= quantity)
            {
                if (quantity % 2 == 1)
                {
                    product.Price = quantity - 1 * product.Price;
                }
            }
        }

    }
}
