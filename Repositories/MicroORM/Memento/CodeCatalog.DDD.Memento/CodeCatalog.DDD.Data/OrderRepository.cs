using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

using CodeCatalog.DDD.Domain;
using CodeCatalog.DDD.Domain.Infrastructure;

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
                                        OrderId = orderId.ToSqliteGuid(),
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
                                        Id = orderId.ToSqliteGuid(),
                                        CustomerId = customerId
                                    });
        }

        public void Save(Order order)
        {
            throw new NotImplementedException();
        }

        public Order FindBy(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
