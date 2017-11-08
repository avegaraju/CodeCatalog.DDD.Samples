using System;
using System.Collections.Generic;
using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCase;
using FluentAssertions;
using Xunit;

namespace CodeCatalog.DDD.Domain.Tests.Unit
{
    public class OrderFactoryTests
    {
        [Fact]
        public void CreateFrom_WithInvalidProduct_ThrowsException()
        {
            OrderRequest request = new OrderRequest()
            {
                CustomerId = default(CustomerId),
                IsPrivilegeCustomer = false,
                Products = null
            };

            Action action = () => Order.OrderFactory.CreateFrom(request);

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void CreateFrom_WithValidRequest_CreatesOrder()
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

            var order = Order.OrderFactory.CreateFrom(request);

            order.Should().NotBeNull();
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

            Action action = () => Order.OrderFactory.CreateFrom(request);

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void CreateFrom_CreatesOrderWithCorrectProperties()
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

            var order = Order.OrderFactory.CreateFrom(request);

            var expectedOrder = new
            {
                CustomerId = (CustomerId)request.CustomerId,
                ProductRequests = new List<ProductRequest>()
                {
                    new ProductRequest()
                    {
                        Discount = 10.3,
                        Price = 12,
                        ProductId = (ProductId)1
                    }
                }
            };

            order.ShouldBeEquivalentTo(expectedOrder);
        }
    }
}
