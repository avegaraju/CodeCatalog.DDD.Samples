using System;
using System.Collections.Generic;

using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCases;

using FluentAssertions;

using Xunit;

namespace CodeCatalog.DDD.Domain.Test.Unit
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
                        Discount = 10.3m,
                        Price = 12,
                        ProductId = (ProductId) 1,
                        Quantity = 1
                    }
                }
            };

            var orderId = Guid.NewGuid();

            var order = Order.OrderFactory.CreateFrom(orderId, request);

            var expectedOrder = new
            {
                OrderId = orderId,
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
