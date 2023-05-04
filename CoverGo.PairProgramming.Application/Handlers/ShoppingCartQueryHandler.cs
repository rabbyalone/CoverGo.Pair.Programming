using CoverGo.PairProgramming.Application.Queries;
using CoverGo.PairProgramming.Domain.Models;
using MediatR;

namespace CoverGo.PairProgramming.Application.Handlers
{
    public class ShoppingCartQueryHandler : IRequestHandler<ShoppingCartQuery, ShoppingCart>
    {
        public async Task<ShoppingCart> Handle(ShoppingCartQuery request, CancellationToken cancellationToken)
        {

            ArgumentNullException.ThrowIfNull(request, nameof(request));
            ArgumentNullException.ThrowIfNull(request.Products);

            ShoppingCart cart = new ShoppingCart
            {
                CartId = 1,
                CustomerId = request.CustomerId,
                Products = new List<Product>(request.Products),
                Discount = CalculateDiscount(request.Products),
                TotalPrice = CalculateTotalPrice(request.Products),
            };

            return cart;
        }

        private decimal CalculateTotalPrice(List<Product> products)
        {
            decimal total = 0;
            foreach (Product product in products)
            {
                total = (product.Price * product.Quantity) - CalculateDiscount(products);
            }
            return total;
        }

        private decimal CalculateDiscount(List<Product> products)
        {
            decimal discount = 0;
            foreach (var item in products ?? new List<Product>())
            {
                if (item.Name.Contains("Jeans"))
                {
                    if (item.Quantity % 2 == 1)
                        discount = item.Price;
                }

            }

            return discount;
        }
    }
}
