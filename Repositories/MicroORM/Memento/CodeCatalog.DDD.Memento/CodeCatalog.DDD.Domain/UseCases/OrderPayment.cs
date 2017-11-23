using System;

using CodeCatalog.DDD.Domain.Exceptions;
using CodeCatalog.DDD.Domain.Infrastructure;
using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain.UseCases
{
    public class OrderPayment
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentService _paymentService;

        public OrderPayment(IOrderRepository orderRepository, 
            IPaymentService paymentService)
        {
            _orderRepository = orderRepository;
            _paymentService = paymentService;
        }

        public PaymentResult Pay(Guid orderId, double amount)
        {
            var paymentResult = new PaymentResult()
                                {
                                    Status = PaymentStatus.Failed
                                };

            try
            {
                var  paymentReference = _paymentService.Pay(amount, orderId);

                paymentResult.PaymentTransactionReference = paymentReference.TransactionId;

                try
                {
                    var order = _orderRepository.FindBy(orderId);

                    order.UpdateOrderWith(paymentReference);

                    _orderRepository.Save(order);

                    paymentResult.Status = PaymentStatus.Succeeded;
                }
                catch (Exception e)
                {
                    paymentResult.Reason = e.Message;
                }
            }
            catch (Exception e)
            {
                paymentResult.Reason = e.Message;
            }

            return paymentResult;
        }
    }
}
