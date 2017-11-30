using System;
using System.Collections.Generic;
using System.Linq;

using CodeCatalog.DDD.Data.Rows;
using CodeCatalog.DDD.Domain;
using CodeCatalog.DDD.Domain.Infrastructure;
using CodeCatalog.DDD.Domain.Types;

using Dapper;

namespace CodeCatalog.DDD.Data
{
    public class OrderRepository: RepositoryBase, IOrderRepository
    {
        public OrderRepository(string databaseFileName):base(databaseFileName)
        {
        }

        public void Add(Order order)
        {
            var orderState = order.GetState();

            using (Connection)
            {
                InsertOrder(orderState.OrderId, orderState.Customer.CustomerId);

                InsertOrderLines(orderState.OrderId, orderState.OrderLines);
            }
        }

        private void InsertOrderLines(Guid orderId, IEnumerable<OrderLineState> orderLines)
        {
            var parameters =  BuildParameters();

            Connection
                    .Execute(sql: "insert into OrderLines"
                                  + "("
                                  + "OrderId, "
                                  + "ProductId, "
                                  + "Quantity, "
                                  + "Price, "
                                  + "Discount) "
                                  + "values"
                                  + "("
                                  + ":OrderId, "
                                  + ":ProductId, "
                                  + ":Quantity, "
                                  + ":Price, "
                                  + ":Discount"
                                  + ");",
                                  param: parameters);

            IEnumerable<DynamicParameters> BuildParameters()
            {
                return orderLines
                        .Select(orderLine =>
                                    new DynamicParameters(new
                                    {
                                        OrderId = orderId.ToString(),
                                        ProductId = (long)orderLine.ProductId,
                                        Quantity = orderLine.Quantity,
                                        Price = orderLine.Price,
                                        Discount = orderLine.Discount
                                    }))
                        .ToList();
            }
        }

        private void InsertOrder(Guid orderId, long customerId)
        {
            Connection
                    .Execute(sql: "insert into Orders"
                                  + "("
                                  + "Id, "
                                  + "CustomerId, "
                                  + "PaymentProcessed"
                                  + ") "
                                  + "values"
                                  + "("
                                  + ":Id, "
                                  + ":CustomerId, "
                                  + "'N'"
                                  + ");",
                             param: new
                                    {
                                        Id = orderId.ToString(),
                                        CustomerId = customerId
                                    });
        }

        public void Save(Order order)
        {
            var orderState = order.GetState();

            using (Connection)
            {
                UpdateOrder(orderState.OrderId,
                            orderState.Customer.CustomerId,
                            orderState.PaymentProcessed,
                            orderState.OrderAmount,
                            orderState.PaymentTransactionReference);
            }
        }

        private void UpdateOrder(Guid orderId,
                                 long customerId,
                                 bool paymentProcessed,
                                 decimal orderAmount,
                                 Guid paymentTransactionReference)
        {
            Connection.Execute(sql: $"update ORDERS "
                                    + $"set CUSTOMERID = {customerId}, "
                                    + $"PAYMENTPROCESSED = '{paymentProcessed.ToChar()}', "
                                    + $"ORDERAMOUNT = {orderAmount}, "
                                    + $@"PAYMENTTRANSACTIONREFERENCE = ""{paymentTransactionReference.ToString()}"" "
                                    + $@"where ID = ""{orderId.ToString()}""; ");
        }

        public Order FindBy(Guid orderId)
        {
            using (Connection)
            {
                OrderRow orderRow = GetOrderRow(orderId);

                Customer customer = MakeCustomer(orderRow);
                IEnumerable<OrderLine> orderLines = MakeOrderLines(orderId);

                return Order.OrderFactory.Make(orderRow.Id.ToGuid(),
                                               customer,
                                               orderRow.PaymentProcessed.ToBoolean(),
                                               orderLines);
            }
        }

        private IEnumerable<OrderLine> MakeOrderLines(Guid orderId)
        {
            var orderLineRows
                    = Connection
                            .Query<OrderLineRow>(sql: "select * from ORDERLINES " +
                                                      $@"where ORDERID = ""{orderId.ToString()}"";");

            var orderLines
                    = orderLineRows
                            .Select(ol => OrderLine
                                            .OrderLineFactory
                                            .Make((OrderLineId)(ulong)ol.Id,
                                                  (ProductId)(ulong)ol.ProductId,
                                                  ol.Discount,
                                                  ol.Price,
                                                  (uint)ol.Quantity));
            return orderLines;
        }

        private Customer MakeCustomer(OrderRow orderRow)
        {
            var customerRow
                    = Connection
                            .Query<CustomerRow>(sql: "select * from CUSTOMERS " +
                                                     $@"where ID = ""{orderRow.CustomerId}"";")
                            .FirstOrDefault();

            var customer
                    = Customer
                            .CustomerFactory
                            .Create((CustomerId)(ulong)customerRow.Id,
                                    customerRow.IsPrivilegeCustomer.ToBoolean());

            return customer;
        }

        private OrderRow GetOrderRow(Guid orderId)
        {
            return Connection
                    .Query<OrderRow>(sql: "select * from ORDERS " +
                                          $@"where ID = ""{orderId.ToString()}"";")
                    .FirstOrDefault();
        }
    }
}
