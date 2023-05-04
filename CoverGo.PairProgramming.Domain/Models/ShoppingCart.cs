namespace CoverGo.PairProgramming.Domain.Models
{
    public class ShoppingCart
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }

    }
}
