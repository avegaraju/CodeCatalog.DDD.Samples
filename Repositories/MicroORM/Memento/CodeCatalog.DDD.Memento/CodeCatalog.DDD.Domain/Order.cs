using System.Collections.Generic;
using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCase;

namespace CodeCatalog.DDD.Domain
{
    public partial class Order
    {
        private Order(CustomerId customerId,
            bool isPrivilegeCustomer,
            IReadOnlyCollection<ProductRequest> products)
        {
            
        }
    }
}
