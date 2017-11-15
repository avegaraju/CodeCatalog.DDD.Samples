using System;
using System.Collections.Generic;

using CodeCatalog.DDD.Domain.UseCases;

namespace CodeCatalog.DDD.Domain
{
    public partial class Order
    {
        public static class OrderFactory
        {
            public static Order CreateFrom(Guid orderId,
                                           OrderRequest orderRequest)
            {
                var customer = Customer.CustomerFactory
                                       .Create(orderRequest.CustomerId,
                                               orderRequest.IsPrivilegeCustomer);

                var products = OrderLine.OrderLineFactory
                                        .CreateFrom(orderRequest.ProductRequests);

                return new Order(orderId, customer, products);
            }
        }
    }
}