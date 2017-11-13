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
                amountToPay = (amountToPay * 5) /  100;
            }
        }
    }
}
