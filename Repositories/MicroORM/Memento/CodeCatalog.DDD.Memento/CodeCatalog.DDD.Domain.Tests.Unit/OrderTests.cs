using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CodeCatalog.DDD.Domain.Tests.Unit
{
    public class OrderTests
    {
        [Fact]
        public void CanCreateOrders()
        {
            Order order = new Order();
            order.Should().NotBeNull();
        }

        [Fact]
        public void CanCreateOrderUsingFactory()
        {
             var order =  Order
                .OrderFactory.Create();

            order.Should().NotBeNull();
        }

        [Fact]
        public void CanCreateOrderFromOrderRequest()
        {
            
        }
    }
}
