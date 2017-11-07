using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCatalog.DDD.Domain
{
    public partial class Order
    {
        public static class OrderFactory
        {
            public static Order Create()
            {
                return new Order();
            }
        }
    }
}
