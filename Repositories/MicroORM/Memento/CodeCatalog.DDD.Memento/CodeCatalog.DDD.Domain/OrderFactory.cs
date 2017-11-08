using System;
using System.Collections.Generic;
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
                if (IsDiscountGreaterThanProductPrice(orderRequest.Products))
                    throw new ArgumentException("Invalid discount amount.");

                return new Order(orderRequest.CustomerId,
                    orderRequest.IsPrivilegeCustomer,
                    orderRequest.Products);
            }

            private static bool IsDiscountGreaterThanProductPrice(
                IReadOnlyList<ProductRequest> productRequests)
            {
                foreach (var productRequest in productRequests)
                {
                    var discountAmount = GetDiscountAmount(productRequest);

                    if (productRequest.Price - discountAmount < 0)
                        return true;
                }
                return false;
            }

            private static double GetDiscountAmount(ProductRequest productRequest)
            {
                return (productRequest.Price * productRequest.Discount) / 100;
            }
        }
    }
}
