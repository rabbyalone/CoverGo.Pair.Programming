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
                Discount = CalculateDiscountandTotalPrice(request.Products).Item1,
                TotalPrice = CalculateDiscountandTotalPrice(request.Products).Item2,
            };

            return cart;
        }

        private (decimal, decimal) CalculateDiscountandTotalPrice(List<Product> products)
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

            ArgumentNullException.ThrowIfNull(products);

            decimal totalPrice = products.Sum(x => x.Price * x.Quantity) - discount;

            return (discount, totalPrice);
        }
    }
}
