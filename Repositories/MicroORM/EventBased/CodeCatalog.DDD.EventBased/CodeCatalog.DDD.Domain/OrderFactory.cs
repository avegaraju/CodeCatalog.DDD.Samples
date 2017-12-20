using System;
using System.Collections.Generic;
using System.Linq;

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

                var orderLines = OrderLine.OrderLineFactory
                                        .CreateFrom(orderRequest.ProductRequests);

                return new Order(orderId: orderId,
                                 customer: customer,
                                 paymentProcessed: false,
                                 orderLines: orderLines);
            }

            public static Order Make(Guid orderId,
                                     Customer customer,
                                     bool paymentProcessed,
                                     IEnumerable<OrderLine> orderLines)
            {
                return new Order(orderId: orderId,
                                 customer: customer,
                                 paymentProcessed: paymentProcessed,
                                 orderLines: orderLines.ToList());
            }
        }
    }
}