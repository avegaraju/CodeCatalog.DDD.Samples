using System;

namespace CodeCatalog.DDD.Domain.Infrastructure
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Save(Order order);
        Order FindBy(Guid orderId);
    }
}
