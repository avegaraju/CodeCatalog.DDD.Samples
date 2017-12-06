using System;

namespace CodeCatalog.DDD.Domain
{
    public class CustomerState
    {
        public long CustomerId { get; set; }
        public bool IsPrivilegeCustomer { get; set; }
    }
}
