using System;
using System.Collections.Generic;
using System.Linq;

using CodeCatalog.DDD.Domain.Events;
using CodeCatalog.DDD.Domain.Exceptions;
using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCases;

using FluentAssertions;

using Xunit;

namespace CodeCatalog.DDD.Domain.Test.Unit
{
    public class OrderTests
    {
        [Fact]
        public void CheckOut_AppliesApplicableDisocunt()
        {
            var orderRequest = CreateDefaultOrderRequest();

            var order = Order.OrderFactory.CreateFrom(Guid.NewGuid(), orderRequest);

            var amountToPay = order.CheckOut();

            var expectedAmountToPay = CalculateAmountToPay(order);

            amountToPay.Should().Be(expectedAmountToPay);
        }

        [Fact]
        public void CheckOut_ForPrivilegeCustomers_AppliesAdditionalDiscount()
        {
            var orderRequest = CreateDefaultOrderRequest();
            orderRequest.IsPrivilegeCustomer = true;

            var order = Order.OrderFactory.CreateFrom(Guid.NewGuid(), orderRequest);

            var amountToPay = order.CheckOut();

            var expectedAmountToPay = CalculateAmountToPay(order);

            var expectedAmountToPayForPrivilegeCustomer = CalculateAmountToPayForPrivilegeCsustomer(expectedAmountToPay);

            amountToPay.Should().Be(expectedAmountToPayForPrivilegeCustomer);
        }

        [Fact]
        public void UpdateOrderWith_WithoutOrderCheckoutIsNotAllowed()
        {
            var orderRequest = CreateDefaultOrderRequest();
            orderRequest.IsPrivilegeCustomer = true;

            var order = Order.OrderFactory.CreateFrom(Guid.NewGuid(), orderRequest);

            Action action = () => order.UpdateOrderWith(new PaymentReference()
                                                                 {
                                                                     TransactionId = Guid.NewGuid()
                                                                 });

            action.ShouldThrow<OrderNotCheckedOutException>()
                  .Which.Message
                  .Should().StartWith("Order payment status cannot "
                                      + "be updated without cheking "
                                      + "out the order.");
        }

        [Fact]
        public void UpdateOrderWith_UpdatesTheOrder()
        {
            var expectedPaymentTransactionReference = Guid.NewGuid();

            var orderRequest = CreateDefaultOrderRequest();
            orderRequest.IsPrivilegeCustomer = true;

            var order = Order.OrderFactory.CreateFrom(Guid.NewGuid(), orderRequest);

            var orderAmount =  order.CheckOut();

            order.UpdateOrderWith(new PaymentReference()
                                           {
                                               TransactionId = expectedPaymentTransactionReference
                                           });

            var orderState = order.GetState();

            orderState.OrderAmount
                .Should().Be(orderAmount);

            orderState.PaymentProcessed
                .Should().BeTrue();

            orderState.PaymentTransactionReference
                .Should().Be(expectedPaymentTransactionReference);
        }

        [Fact]
        public void UpdateOrderWith_RaisedOrderUpdatedEvent()
        {
            var orderId = Guid.NewGuid();

            var expectedPaymentTransactionReference = Guid.NewGuid();

            var orderRequest = CreateDefaultOrderRequest();
            orderRequest.IsPrivilegeCustomer = true;

            var order = Order.OrderFactory.CreateFrom(orderId, orderRequest);

            var orderAmount = order.CheckOut();

            order.UpdateOrderWith(new PaymentReference()
                                  {
                                      TransactionId = expectedPaymentTransactionReference
                                  });
            var events = order.DequeueEvents();

            var @event = events
                    .OfType<OrderUpdated>()
                    .First();

            @event.OrderAmount
                  .Should().Be(orderAmount);

            @event.OrderId
                  .Should().Be(orderId);

            @event.PaymentTransactionReference
                  .Should().Be(expectedPaymentTransactionReference);
        }

        private decimal CalculateAmountToPayForPrivilegeCsustomer(decimal expectedAmountToPay)
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
                        Discount = 10.3m,
                        Price = 12,
                        ProductId = (ProductId) 1,
                        Quantity = 2
                    }
                }
            };
        }

        private decimal CalculateAmountToPay(Order order)
        {
            decimal amountToPay = 0;

            var orderState =  order.GetState();
            
            foreach (var orderLine in orderState.OrderLines)
            {
                decimal discountAmount = CalculateApplicableDiscount(orderLine);
                decimal productPriceAfterDiscount = ApplyDiscount(orderLine, discountAmount);

                amountToPay += productPriceAfterDiscount;
            }

            return amountToPay;
        }

        private static decimal ApplyDiscount(OrderLineState orderLine, decimal discountAmount)
        {
            return (orderLine.Price - discountAmount) * orderLine.Quantity;
        }

        private static decimal CalculateApplicableDiscount(OrderLineState orderLine)
        {
            return (orderLine.Price * orderLine.Discount) / 100;
        }
    }
}
