using System.Collections.Generic;
using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCase;
using FluentAssertions;
using Xunit;

namespace CodeCatalog.DDD.Domain.Tests.Unit
{
    public class OrderTests
    {
        [Fact]
        public void CheckOut_AppliesApplicableDisocunt()
        {
            var orderRequest = CreateDefaultOrderRequest();

            var order = Order.OrderFactory.CreateFrom(orderRequest);

            var amountToPay = order.CheckOut();

            var expectedAmountToPay = CalculateAmountToPay(order);

            amountToPay.Should().Be(expectedAmountToPay);
        }

        [Fact]
        public void CheckOut_ForPrivilegeCustomers_AppliesAdditionalDiscount()
        {
            var orderRequest = CreateDefaultOrderRequest();
            orderRequest.IsPrivilegeCustomer = true;

            var order = Order.OrderFactory.CreateFrom(orderRequest);

            var amountToPay = order.CheckOut();

            var expectedAmountToPay = CalculateAmountToPay(order);

            var expectedAmountToPayForPrivilegeCustomer = CalculateAmountToPayForPrivilegeCsustomer(expectedAmountToPay);

            amountToPay.Should().Be(expectedAmountToPayForPrivilegeCustomer);
        }

        private double CalculateAmountToPayForPrivilegeCsustomer(double expectedAmountToPay)
        {
            return (expectedAmountToPay * 5) / 100;
        }

        private static OrderRequest CreateDefaultOrderRequest()
        {
            return new OrderRequest()
            {
                CustomerId = default(CustomerId),
                IsPrivilegeCustomer = false,
                ProductRequests = new List<ProductRequest>()
                {
                    new ProductRequest()
                    {
                        Discount = 10.3,
                        Price = 12,
                        ProductId = (ProductId) 1,
                        Quantity = 2
                    }
                }
            };
        }

        private double CalculateAmountToPay(Order order)
        {
            double amountToPay = 0;
            foreach (var orderLine in order.OrderLines)
            {
                double discountAmount = GetApplicableDiscount(orderLine);
                double productPriceAfterDiscount = ApplyDiscount(orderLine, discountAmount);

                amountToPay += productPriceAfterDiscount;
            }

            return amountToPay;
        }

        private static double ApplyDiscount(OrderLine orderLine, double discountAmount)
        {
            return (orderLine.Price - discountAmount) * orderLine.Quantity;
        }

        private static double GetApplicableDiscount(OrderLine orderLine)
        {
            return (orderLine.Price * orderLine.Discount) / 100;
        }
    }
}
