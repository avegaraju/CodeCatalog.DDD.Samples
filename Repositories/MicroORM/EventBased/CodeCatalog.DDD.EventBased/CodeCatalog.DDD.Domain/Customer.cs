using System;

using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain
{
    public partial class Customer
    {
        internal CustomerId CustomerId { get; }
        internal bool IsPrivilegeCustomer { get; }

        private Customer(CustomerId customerId,
            bool isPrivilegeCustomer)
        {
            CustomerId = customerId;
            IsPrivilegeCustomer = isPrivilegeCustomer;
        }

        public CustomerState GetState()
        {
            return new CustomerState()
                   {
                       CustomerId = this.CustomerId,
                       IsPrivilegeCustomer = this.IsPrivilegeCustomer
                   };
        }
    }
}
