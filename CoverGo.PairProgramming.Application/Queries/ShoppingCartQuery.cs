using CoverGo.PairProgramming.Domain.Models;
using MediatR;

namespace CoverGo.PairProgramming.Application.Queries
{
    public class ShoppingCartQuery : IRequest<ShoppingCart>
    {
        public int CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
