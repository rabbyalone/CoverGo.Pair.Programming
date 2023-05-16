using CoverGo.PairProgramming.Domain.Models;
using MediatR;

namespace CoverGo.PairProgramming.Application.Queries
{
    public class AddProductToCartCommand : IRequest<Unit>
    {
        public int CustomerId { get; set; }
        public Product Product { get; set; }

        public AddProductToCartCommand(Product product)
        {
            Product = product;
        }
    }
}
