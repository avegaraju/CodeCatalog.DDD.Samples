using System;

using CodeCatalog.DDD.Domain;
using CodeCatalog.DDD.Domain.Infrastructure;

using Dapper;

namespace CodeCatalog.DDD.Data
{
    internal class OrderRepository: RepositoryBase, IOrderRepository
    {
        public void Add(Order order)
        {
            throw new NotImplementedException();
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
