using System.Collections.Generic;

namespace CodeCatalog.DDD.Domain
{
    public partial class Order
    {
        public Customer Customer { get; }
        public IReadOnlyCollection<Product> Products { get; }

        private Order(Customer customer, 
                      IReadOnlyCollection<Product> products)
        {
            Customer = customer;
            Products = products;
        }
    }
}
