﻿using System;

using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain.Infrastructure
{
    public interface IPaymentService
    {
        PaymentReference Pay(decimal amount, Guid orderId);
    }
}
