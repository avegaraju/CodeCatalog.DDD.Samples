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

        public TransactionResult Pay(Guid orderId, decimal amount)
        {
            var transactionResult = new TransactionResult()
                                {
                                    PaymentStatus = PaymentStatus.Failed,
                                    OrderTransactionStatus = OrderTransactionStatus.Failed
                                };

            try
            {
                var  paymentReference = _paymentService.Pay(amount, orderId);

                transactionResult.PaymentTransactionReference = paymentReference.TransactionId;

                transactionResult.PaymentStatus = PaymentStatus.Succeeded;

                try
                {
                    var order = _orderRepository.FindBy(orderId);

                    order.UpdateOrderWith(paymentReference);

                    _orderRepository.Save(order);

                    transactionResult.OrderTransactionStatus = OrderTransactionStatus.Succeeded;
                }
                catch (Exception e)
                {
                    transactionResult.FailureReason = e.Message;
                }
            }
            catch (Exception e)
            {
                transactionResult.FailureReason = e.Message;
            }

            return transactionResult;
        }
    }
}
