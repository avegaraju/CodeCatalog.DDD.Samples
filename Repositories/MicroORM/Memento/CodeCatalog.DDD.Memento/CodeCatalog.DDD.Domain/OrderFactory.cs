using System;
using System.Collections.Generic;
using CodeCatalog.DDD.Domain.UseCases;

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

                var products = OrderLine.OrderLineFactory
                    .CreateFrom(orderRequest.ProductRequests);

                return new Order(customer, products);
            }
        }
    }
}
