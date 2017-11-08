using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain
{
    public partial class Customer
    {
        public CustomerId CustomerId { get; }
        public bool IsPrivilegeCustomer { get; }

        private Customer(CustomerId customerId,
            bool isPrivilegeCustomer)
        {
            CustomerId = customerId;
            IsPrivilegeCustomer = isPrivilegeCustomer;
        }
    }
}
