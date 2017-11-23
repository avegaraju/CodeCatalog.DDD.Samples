using System;

namespace CodeCatalog.DDD.Domain.Types
{
    public class PaymentResult
    {
        public PaymentStatus Status { get; set; }
        public Guid PaymentTransactionReference { get; set; }
        public string Reason { get; set; }
    }

    public enum PaymentStatus
    {
        Succeeded,
        Failed
    }
}
