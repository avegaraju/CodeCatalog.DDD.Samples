using System;
using System.Collections.Generic;
using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCase;
using FluentAssertions;
using Xunit;

namespace CodeCatalog.DDD.Domain.Tests.Unit
{
    public class OrderLineFactoryTests
    {
        [Fact]
        public void CanCreateOrderLine()
        {
            var products = new List<ProductRequest>()
            {
                new ProductRequest()
                {
                    Discount = 10,
                    Price = 200,
                    ProductId = (ProductId) 1
                }
            };

            var orderLine = OrderLine
                .OrderLineFactory.CreateFrom(products);

            orderLine.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void CreateFrom_WithInvalidRequestThrowsException()
        {
            Action action = () => OrderLine
            .OrderLineFactory.CreateFrom(null);

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void CreateFrom_WhenProductDiscountIsMoreThanProductPrice_ThrowsException()
        {
            var productRequests = new List<ProductRequest>()
            {
                new ProductRequest()
                {
                    Discount = 190.3,
                    Price = 1,
                    ProductId = (ProductId) 1
                }
            };

            Action action = () => OrderLine
            .OrderLineFactory.CreateFrom(productRequests);

            action.ShouldThrow<ArgumentException>();
        }
    }
}
