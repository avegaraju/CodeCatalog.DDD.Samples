using System;
using System.Collections.Generic;
using System.Text;
using CodeCatalog.DDD.Domain.UseCase;

namespace CodeCatalog.DDD.Domain
{
    public partial class Order
    {
        public static class OrderFactory
        {
            public static Order CreateFrom(OrderRequest orderRequest)
            {
                if(orderRequest.Products == null)
                    throw new ArgumentNullException(nameof(orderRequest.Products));

                throw new NotImplementedException();
            }
        }
    }
}
