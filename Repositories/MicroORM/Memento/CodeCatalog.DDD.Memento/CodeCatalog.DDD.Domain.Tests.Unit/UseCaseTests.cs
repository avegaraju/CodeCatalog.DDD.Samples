using System;
using System.Collections.Generic;
using System.Transactions;

using CodeCatalog.DDD.Domain.Exceptions;
using CodeCatalog.DDD.Domain.Infrastructure;
using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCases;

using FluentAssertions;

using Moq;

using Xunit;

namespace CodeCatalog.DDD.Domain.Test.Unit
{
    public class UseCaseTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;

        public UseCaseTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
        }

        [Fact]
        public void CreateOrder_WhenRepositoryThrowsException_RethrowsOrderCreationException()
        {
            _orderRepositoryMock
                    .Setup(obj => obj.Add(It.IsAny<Order>()))
                    .Throws<Exception>();

            CreateOrder sut = new CreateOrder(_orderRepositoryMock.Object);

            Action action = () => sut.Create(CreateDefaultOrderRequest());

            action
                .ShouldThrow<OrderCreationException>()
                .Which.Message
                .Should().StartWith("Order creation for Id");

        }

        [Fact]
        public void CreateOrder_CallsRepositoryWithinATransaction()
        {
            Transaction transaction = null;

            _orderRepositoryMock
                    .Setup(obj => obj.Add(It.IsAny<Order>()))
                    .Callback(() => transaction = Transaction.Current);

            CreateOrder sut = new CreateOrder(_orderRepositoryMock.Object);

            sut.Create(CreateDefaultOrderRequest());

            transaction
                .Should().NotBeNull();
        }

        [Fact]
        public void CreateOrder_WhenNoExceptionsAreThrown_CompletesTheTransaction()
        {
            Transaction transaction = null;
            TransactionStatus transactionStatus = TransactionStatus.Active;

            _orderRepositoryMock
                    .Setup(obj => obj.Add(It.IsAny<Order>()))
                    .Callback(() =>
                              {
                                  transaction = Transaction.Current;
                                  transaction.TransactionCompleted
                                          += (sender, args) =>
                                             {
                                                 transactionStatus = TransactionStatus.Committed;
                                             };
                              });

            CreateOrder sut = new CreateOrder(_orderRepositoryMock.Object);

            sut.Create(CreateDefaultOrderRequest());

            transactionStatus
                .Should().Be(TransactionStatus.Committed);
        }

        [Fact]
        public void Checkout_WhenRepositoryThrowsException_ReThrowsCheckoutException()
        {
            _orderRepositoryMock
                    .Setup(obj => obj.FindBy(It.IsAny<Guid>()))
                    .Throws<Exception>();

            var sut = new CheckOutOrder(_orderRepositoryMock.Object);

            Action action = ()=>  sut.Checkout(Guid.NewGuid());

            action
                .ShouldThrow<OrderCheckoutException>()
                .Which.Message
                .Should().StartWith("Cannot checkout order");
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
    }
}
