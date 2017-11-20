using CodeCatalog.DDD.Domain.Infrastructure;
using System;
using System.Transactions;

using CodeCatalog.DDD.Domain.Exceptions;

namespace CodeCatalog.DDD.Domain.UseCases
{
    public class CreateOrder
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrder(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public Guid Create(OrderRequest request)
        {
            Guid orderId = Guid.NewGuid();

            try
            {
                var order = Order.OrderFactory
                                 .CreateFrom(orderId, request);

                using (var transactionScope = new TransactionScope())
                {
                    _orderRepository.Add(order);

                    transactionScope.Complete();
                }
            }
            catch (Exception e)
            {
                throw new OrderCreationException($"Order cration for Id {orderId} failed.", e);
            }

            return orderId;
        }
    }
}