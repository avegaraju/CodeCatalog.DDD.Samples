using System;
using System.Collections.Generic;
using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCase;
using FluentAssertions;
using Xunit;

namespace CodeCatalog.DDD.Domain.Tests.Unit
{
    public class ProductFactoryTests
    {
        [Fact]
        public void CreateFrom_CreatesProducts()
        {
            OrderRequest request = new OrderRequest()
            {
                CustomerId = default(CustomerId),
                IsPrivilegeCustomer = false,
                Products = new List<ProductRequest>()
                {
                    new ProductRequest()
                    {
                        Discount = 10.3,
                        Price = 12,
                        ProductId = (ProductId) 1
                    }
                }
            };
            var products = Product.ProductFactory.CreateFrom(request.Products);

            products.Should().HaveCount(request.Products.Count);
        }

        [Fact]
        public void CreateFrom_WithInvalidRequestThrowsException()
        {
            OrderRequest request = new OrderRequest()
            {
                CustomerId = default(CustomerId),
                IsPrivilegeCustomer = false,
                Products = null
            };
            Action action = () => Product.ProductFactory.CreateFrom(request.Products);

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void CreateFrom_WhenProductDiscountIsMoreThanProductPrice_ThrowsException()
        {
            OrderRequest request = new OrderRequest()
            {
                CustomerId = default(CustomerId),
                IsPrivilegeCustomer = false,
                Products = new List<ProductRequest>()
                {
                    new ProductRequest()
                    {
                        Discount = 190.3,
                        Price = 1,
                        ProductId = (ProductId) 1
                    }
                }
            };

            Action action = () => Product.ProductFactory.CreateFrom(request.Products);

            action.ShouldThrow<ArgumentException>();
        }
    }
}
