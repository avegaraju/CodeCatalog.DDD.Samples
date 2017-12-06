using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain
{
    public partial class Customer
    {
        public static class CustomerFactory
        {
            public static Customer Create(CustomerId customerId, 
                bool isPrivilegeCustomer)
            {
                return new Customer(customerId, isPrivilegeCustomer);
            }
        }
    }
}
