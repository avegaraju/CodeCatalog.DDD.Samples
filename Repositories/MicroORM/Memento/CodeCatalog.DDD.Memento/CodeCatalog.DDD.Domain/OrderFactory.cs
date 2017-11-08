using System;
using System.Collections.Generic;
using CodeCatalog.DDD.Domain.UseCase;

namespace CodeCatalog.DDD.Domain
{
    public partial class Order
    {
        public static class OrderFactory
        {
            public static Order CreateFrom(OrderRequest orderRequest)
            {
                var customer = Customer.CustomerFactory
                    .Create(orderRequest.CustomerId,
                            orderRequest.IsPrivilegeCustomer);

                var products = Product.ProductFactory
                    .CreateFrom(orderRequest.Products);

                return new Order(customer, products);
            }
        }
    }
}
