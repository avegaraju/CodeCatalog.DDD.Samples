using System;
using System.Collections.Generic;
using System.Linq;

using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCases;

using FluentAssertions;

using Xunit;

namespace CodeCatalog.DDD.Domain.Test.Unit
{
    public class OrderLineFactoryTests
    {
        [Fact]
        public void CreateFrom_WithValidRequest_CreatesOrderLines()
        {
            var products = new List<ProductRequest>()
            {
                new ProductRequest()
                {
                    Discount = 10,
                    Price = 200,
                    ProductId = (ProductId) 1,
                    Quantity = 1
                }
            };

            var orderLine = OrderLine
                .OrderLineFactory.CreateFrom(products);

            var expectedObject = new
            {
                OrderLineId = (OrderLineId?)null,
                ProductId = (ProductId)1,
                Price = 200,
                Discount = 10,
                Quantity = 1
            };

            orderLine.First()
                .ShouldBeEquivalentTo(expectedObject);
        }

        [Fact]
        public void CreateFrom_WithZeroQuantity_ThrowsException()
        {
            var products = new List<ProductRequest>()
            {
                new ProductRequest()
                {
                    Discount = 10,
                    Price = 200,
                    ProductId = (ProductId) 1,
                    Quantity = 0
                }
            };

            Action action = () => OrderLine
                .OrderLineFactory.CreateFrom(products);

            action.ShouldThrow<ArgumentException>();
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
                    Discount = 190.3m,
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
