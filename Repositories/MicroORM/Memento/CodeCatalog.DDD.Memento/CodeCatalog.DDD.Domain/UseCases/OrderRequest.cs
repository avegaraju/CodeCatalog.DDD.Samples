using System.Collections.Generic;
using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain.UseCases
{
    public class OrderRequest
    {
        public IReadOnlyList<ProductRequest> ProductRequests { get; set; }
        public CustomerId CustomerId { get; set; }
        public bool IsPrivilegeCustomer { get; set; }
    }
}
