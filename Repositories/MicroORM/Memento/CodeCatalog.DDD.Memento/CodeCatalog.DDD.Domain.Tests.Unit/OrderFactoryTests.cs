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
        public void CreateFrom_CreatesOrderWithCorrectProperties()
        {
            OrderRequest request = new OrderRequest()
            {
                CustomerId = default(CustomerId),
                IsPrivilegeCustomer = false,
                ProductRequests = new List<ProductRequest>()
                {
                    new ProductRequest()
                    {
                        Discount = 10.3,
                        Price = 12,
                        ProductId = (ProductId) 1,
                        Quantity = 1
                    }
                }
            };

            var order = Order.OrderFactory.CreateFrom(request);

            var expectedOrder = new
            {
                Customer = Customer.CustomerFactory
                            .Create(request.CustomerId, 
                                    request.IsPrivilegeCustomer),
                OrderLines = OrderLine
                .OrderLineFactory.CreateFrom(request.ProductRequests)
            };

            order.ShouldBeEquivalentTo(expectedOrder);
        }
    }
}
