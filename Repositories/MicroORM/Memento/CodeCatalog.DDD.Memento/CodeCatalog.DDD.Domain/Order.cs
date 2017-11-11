using System.Collections.Generic;

namespace CodeCatalog.DDD.Domain
{
    public partial class Order
    {
        public Customer Customer { get; }
        public IReadOnlyCollection<OrderLine> OrderLines { get; }

        private Order(Customer customer, 
                      IReadOnlyCollection<OrderLine> orderLines)
        {
            Customer = customer;
            OrderLines = orderLines;
        }
    }
}
