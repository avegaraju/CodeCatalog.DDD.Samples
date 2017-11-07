using System;
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
    }
}
