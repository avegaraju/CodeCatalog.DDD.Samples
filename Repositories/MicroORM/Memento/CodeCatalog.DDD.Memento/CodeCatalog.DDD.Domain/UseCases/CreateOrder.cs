using CodeCatalog.DDD.Domain.Infrastructure;
using System;

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

            var order = Order.OrderFactory
                             .CreateFrom(orderId, request);

            _orderRepository.Add(order);

            return orderId;
        }
    }
}