using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCase;
using FluentAssertions;
using Xunit;

namespace CodeCatalog.DDD.Domain.Tests.Unit
{
    public class CustomerFactoryTests
    {
        [Fact]
        public void CreateFrom_CreatesCustomer()
        {
            OrderRequest request = new OrderRequest()
            {
                CustomerId = (CustomerId) 1,
                IsPrivilegeCustomer = false,
                ProductRequests = null
            };

            var customer = Customer.CustomerFactory
                .Create(request.CustomerId, 
                request.IsPrivilegeCustomer);

            var expectedCustomer = new
            {
                CustomerId = request.CustomerId,
                IsPrivilegeCustomer = request.IsPrivilegeCustomer
            };

            customer.ShouldBeEquivalentTo(expectedCustomer);
        }
    }
}
