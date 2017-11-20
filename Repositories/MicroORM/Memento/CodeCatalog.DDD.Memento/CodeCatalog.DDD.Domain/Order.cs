using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCatalog.DDD.Domain
{
    public partial class Order
    {
        internal Guid OrderId { get; }
        internal Customer Customer { get; }
        internal IReadOnlyCollection<OrderLine> OrderLines { get; }

        private Order(Guid orderId,
                      Customer customer,
                      IReadOnlyCollection<OrderLine> orderLines)
        {
            OrderId = orderId;
            Customer = customer;
            OrderLines = orderLines;
        }

        public double CheckOut()
        {
            double amountToPay = 0;

            foreach (var orderLine in this.OrderLines)
            {
                double discountAmount = GetApplicableDiscount(orderLine);
                double productPriceAfterDiscount = ApplyDiscount(orderLine, discountAmount);

                amountToPay += productPriceAfterDiscount;
            }

            if (Customer.IsPrivilegeCustomer)
                ApplyAdditionalDiscount();

            return amountToPay;

            double ApplyDiscount(OrderLine orderLine, double discountAmount)
            {
                return (orderLine.Price - discountAmount) * orderLine.Quantity;
            }

            double GetApplicableDiscount(OrderLine orderLine)
            {
                return (orderLine.Price * orderLine.Discount) / 100;
            }

            void ApplyAdditionalDiscount()
            {
                amountToPay = (amountToPay * 5) / 100;
            }
        }

        public OrderState GetState()
        {
            return new OrderState()
                   {
                       OrderId = this.OrderId,
                       Customer = this.Customer.GetState(),
                       OrderLines = GetState(this.OrderLines)
                   };
        }

        static IEnumerable<OrderLineState> GetState(IEnumerable<OrderLine> orderLines)
        {
            return orderLines
                    .Select(orderLine => orderLine.GetState())
                    .ToList();
        }
    }
}