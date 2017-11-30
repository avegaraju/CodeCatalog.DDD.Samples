using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

using CodeCatalog.DDD.Data.Test.Integration.Helpers;
using CodeCatalog.DDD.Domain;
using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCases;

using FluentAssertions;

using Xunit;

namespace CodeCatalog.DDD.Data.Test.Integration
{
    public class OrderRepositoryTests
    {
        private const decimal DEFAULT_DISCOUNT = 10.3m;
        private const decimal DEFAULT_PRICE = 12;
        private const long DEFAULT_PRODUCTID = 1;
        private const int DEFAULT_QUANTITY = 2;

        private readonly string _databaseFileName = "DDDOrderSampleDbTest.sqlite";

        public OrderRepositoryTests()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var databaseFilePath = Path.Combine(appData, _databaseFileName);

            if (File.Exists(databaseFilePath))
                File.Delete(databaseFilePath);
        }

        [Fact]
        [Trait("Category","Integration")]
        public void Add_InsertsOrder()
        {
            Guid orderId = Guid.NewGuid();

            var orderRequest = CreateDefaultOrderRequest();

            var order = Order.OrderFactory.CreateFrom(orderId, orderRequest);

            OrderRepository sut = CreateSut();

            sut.Add(order);

            var orderRow = new OrderHelper().Get(orderId);

            orderRow.CustomerId
                .Should().Be(orderRequest.CustomerId);

            orderRow.Id
                .Should().Be(orderId.ToString());

            orderRow.PaymentProcessed
                    .Should().Be('N');
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Add_InsertsOrderLines()
        {
            Guid orderId = Guid.NewGuid();

            var orderRequest = CreateDefaultOrderRequest();

            var order = Order.OrderFactory.CreateFrom(orderId, orderRequest);

            OrderRepository sut = CreateSut();

            sut.Add(order);

            var orderLineRows = new OrderLineHelper().Get(orderId);

            foreach (var orderLineRow in orderLineRows)
            {
                orderLineRow.ProductId
                    .Should().Be(DEFAULT_PRODUCTID);

                orderLineRow.Discount
                            .Should().Be(DEFAULT_DISCOUNT);

                orderLineRow.Price
                            .Should().Be(DEFAULT_PRICE);

                orderLineRow.Quantity
                            .Should().Be(DEFAULT_QUANTITY);
            }
        }

        [Fact]
        public void FindBy_FindsOrderById()
        {
            Guid orderId = Guid.NewGuid();

            var orderRequest = CreateDefaultOrderRequest();

            var order = Order.OrderFactory.CreateFrom(orderId, orderRequest);

            OrderRepository sut = CreateSut();

            sut.Add(order);

            var persistedOrder = sut.FindBy(orderId);

            var orderState = persistedOrder.GetState();

            orderState.Customer.CustomerId
                      .Should().Be(orderRequest.CustomerId);

            orderState.OrderId
                      .Should().Be(orderId);

            orderState.OrderLines
                      .ShouldBeEquivalentTo(new[]
                                            {
                                                new
                                                {
                                                    Discount = DEFAULT_DISCOUNT,
                                                    Price = DEFAULT_PRICE,
                                                    ProductId = (ProductId)DEFAULT_PRODUCTID,
                                                    Quantity = DEFAULT_QUANTITY
                                                }
                                            }.ToList());
        }

        private static OrderRepository CreateSut()
        {
            return new OrderRepository("DDDOrderSampleDbTest.sqlite");
        }

        private static OrderRequest CreateDefaultOrderRequest()
        {
            return new OrderRequest()
                   {
                       CustomerId = (CustomerId)1,
                       IsPrivilegeCustomer = false,
                       ProductRequests = new List<ProductRequest>()
                                         {
                                             new ProductRequest()
                                             {
                                                 Discount = DEFAULT_DISCOUNT,
                                                 Price = DEFAULT_PRICE,
                                                 ProductId = (ProductId) DEFAULT_PRODUCTID,
                                                 Quantity = DEFAULT_QUANTITY
                                             }
                                         }
                   };
        }
    }
}
