using System;

namespace CodeCatalog.DDD.Domain.Types
{
    public class TransactionResult
    {
        public PaymentStatus PaymentStatus { get; set; }
        public OrderTransactionStatus OrderTransactionStatus { get; set; }
        public Guid PaymentTransactionReference { get; set; }
        public string FailureReason { get; set; }
    }

    public enum PaymentStatus
    {
        Succeeded,
        Failed
    }

    public enum OrderTransactionStatus
    {
        Succeeded,
        Failed
    }
}
