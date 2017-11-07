using System.Collections.Generic;
using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain.UseCase
{
    public class OrderRequest
    {
        public IReadOnlyList<ProductRequest> Products { get; set; }
        public CustomerId CustomerId { get; set; }
        public bool IsPrivilegeCustomer { get; set; }
    }
}
