using CoverGo.PairProgramming.Application.Handlers;
using CoverGo.PairProgramming.Application.Queries;
using CoverGo.PairProgramming.Domain.Models;

namespace CoverGo.PairProgramming.App.Test.Application
{
    public class ShoppingCartQueryHandlerTest
    {
        [Fact]
        public async Task Should_Product_Added_Into_Shopping_Cart_And_Calculate_Total()
        {
            //Arrange
            Product productA = new Product { Name = "Product A", Price = 10.99m, Quantity = 2 };
            Product productB = new Product { Name = "Product B", Price = 19.99m, Quantity = 1 };

            ShoppingCart cart = new ShoppingCart();
            AddProductToCartCommandHandler handler = new AddProductToCartCommandHandler(cart);


            //Act            

            await handler.Handle(new AddProductToCartCommand(productA), CancellationToken.None);
            await handler.Handle(new AddProductToCartCommand(productB), CancellationToken.None);

            decimal totalPrice = cart.CalculateTotal();

            //Assert 

            Assert.Contains(productA, cart.items);
            Assert.Contains(productB, cart.items);

            decimal expectedTotal = (productA.Price * productA.Quantity) +
                                (productB.Price * productB.Quantity);

            Assert.Equal(expectedTotal, totalPrice);


        }

        [Fact]
        public async Task Should_Check_Total_Sum_In_Shopping_Cart()
        {
            //Arrange
            Product productA = new Product { Name = "Product A", Price = 10.99m, Quantity = 2 };
            Product productB = new Product { Name = "Product B", Price = 19.99m, Quantity = 1 };

            ShoppingCart cart = new ShoppingCart();
            AddProductToCartCommandHandler handler = new AddProductToCartCommandHandler(cart);


            //Act            

            await handler.Handle(new AddProductToCartCommand(productA), CancellationToken.None);
            await handler.Handle(new AddProductToCartCommand(productB), CancellationToken.None);

            decimal totalPrice = cart.CalculateTotal();

            //Assert 

            decimal expectedTotal = (productA.Price * productA.Quantity) +
                                (productB.Price * productB.Quantity);

            Assert.Equal(expectedTotal, totalPrice);
        }

        [Fact]
        public async Task Should_Check_If_Query_Is_Empty()
        {
            //Arrange

            var expectedResult = new ShoppingCart();
            var handler = new AddProductToCartCommandHandler(expectedResult);


            //Act

            //var result = await handler.Handle(new AddProductToCartCommand(new Product()), CancellationToken.None);

            //Assert 
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(new AddProductToCartCommand(null), CancellationToken.None));
        }



        [Fact]
        public async Task Should_Check_Discount_Shopping_Cart()
        {
            //Arrange
            Product jeans = new Product { Name = "Jeans", Price = 20m, Quantity = 3 };
            Product tShirt = new Product { Name = "T-Shirt", Price = 10, Quantity = 3 };

            ShoppingCart cart = new ShoppingCart();
            AddProductToCartCommandHandler handler = new AddProductToCartCommandHandler(cart);

            //Act

            await handler.Handle(new AddProductToCartCommand(jeans), CancellationToken.None);
            await handler.Handle(new AddProductToCartCommand(tShirt), CancellationToken.None);

            cart.ApplyDiscount("Jeans", 3);
            decimal totalPrice = cart.CalculateTotal();

            //Assert .

            decimal expectedTotal = (tShirt.Price * tShirt.Quantity) + 40;
            Assert.Equal(expectedTotal, totalPrice);
        }


    }
}
