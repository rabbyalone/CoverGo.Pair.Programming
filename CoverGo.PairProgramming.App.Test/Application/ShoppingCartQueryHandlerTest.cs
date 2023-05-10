using CoverGo.PairProgramming.Application.Handlers;
using CoverGo.PairProgramming.Application.Queries;
using CoverGo.PairProgramming.Domain.Models;
using Shouldly;

namespace CoverGo.PairProgramming.App.Test.Application
{
    public class ShoppingCartQueryHandlerTest
    {
        [Fact]
        public async Task Should_Product_Added_Into_Shopping_Cart()
        {
            //Arrange
            var handler = new ShoppingCartQueryHandler();
            var query = new ShoppingCartQuery
            {
                CustomerId = 1,
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Product A"},
                    new Product { Id = 1, Name = "Product B"},
                }
            };

            var expectedResult = new ShoppingCart
            {
                CartId = 1,
                CustomerId = 1,
                Products = query.Products
            };

            //Act

            var result = await handler.Handle(query, CancellationToken.None);

            //Assert 

            result.Products.Count.ShouldBe(query.Products.Count);
            result.Products.Any(a => a.Name.Contains("Product A") || a.Name.Contains("Product B")).ShouldBeTrue();


        }

        [Fact]
        public async Task Should_Check_Total_Sum_In_Shopping_Cart()
        {
            //Arrange
            var handler = new ShoppingCartQueryHandler();
            var query = new ShoppingCartQuery
            {
                CustomerId = 1,
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "T-Shirt", Price = 10},
                    new Product { Id = 1, Name = "Jeans", Price = 20},
                }
            };

            var expectedResult = new ShoppingCart
            {
                CartId = 1,
                CustomerId = 1,
                Products = query.Products
            };

            //Act

            var result = await handler.Handle(query, CancellationToken.None);

            //Assert 
            result.TotalPrice.ShouldBe(expectedResult.Products.Sum(x => x.Price * x.Quantity));
        }

        [Fact]
        public async Task Should_Check_If_Query_Is_Empty()
        {
            //Arrange
            var handler = new ShoppingCartQueryHandler();
            var query = new ShoppingCartQuery();

            var expectedResult = new ShoppingCart
            {
                CartId = 1,
                CustomerId = 1,
                Products = query.Products
            };

            //Act

            //var result = await handler.Handle(query, CancellationToken.None);

            //Assert 
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(query, CancellationToken.None));
        }



        [Fact]
        public async Task Should_Check_Discount_Shopping_Cart()
        {
            //Arrange
            var handler = new ShoppingCartQueryHandler();
            var query = new ShoppingCartQuery
            {
                CustomerId = 1,
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "T-Shirt", Price = 10},
                    new Product { Id = 1, Name = "Jeans", Price = 20, Quantity = 3},
                }
            };

            var expectedResult = new ShoppingCart
            {
                CartId = 1,
                CustomerId = 1,
                Products = query.Products
            };

            //Act

            var result = await handler.Handle(query, CancellationToken.None);

            //Assert 
            result.Discount.ShouldBe(20);
            result.TotalPrice.ShouldBe(40);
        }


    }
}
