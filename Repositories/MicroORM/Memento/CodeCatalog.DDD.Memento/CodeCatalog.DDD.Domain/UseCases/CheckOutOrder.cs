using System;

using CodeCatalog.DDD.Domain.Exceptions;
using CodeCatalog.DDD.Domain.Infrastructure;

namespace CodeCatalog.DDD.Domain.UseCases
{
    public class CheckOutOrder
    {
        private readonly IOrderRepository _orderRepository;

        public CheckOutOrder(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public decimal Checkout(Guid orderId)
        {
            try
            {
                var order =  _orderRepository.FindBy(orderId);

                return order.CheckOut();
            }
            catch (Exception e)
            {
                throw new OrderCheckoutException($"Cannot checkout order {orderId}", e);
            }
        }
    }
}
