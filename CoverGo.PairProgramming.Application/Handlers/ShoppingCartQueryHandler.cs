using CoverGo.PairProgramming.Application.Queries;
using CoverGo.PairProgramming.Domain.Models;
using MediatR;

namespace CoverGo.PairProgramming.Application.Handlers
{
    public class AddProductToCartCommandHandler : IRequestHandler<AddProductToCartCommand, Unit>
    {

        private readonly ShoppingCart cart;

        public AddProductToCartCommandHandler(ShoppingCart cart)
        {
            this.cart = cart;
        }

        public Task<Unit> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request.Product);

            try
            {
                cart.AddProduct(request.Product);
            }
            catch (Exception)
            {

                throw;
            }
            return Task.FromResult(Unit.Value);
        }

    }
}
