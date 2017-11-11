using System;
using System.Collections.Generic;
using CodeCatalog.DDD.Domain.UseCase;

namespace CodeCatalog.DDD.Domain
{
    public partial class OrderLine
    {
        public static class OrderLineFactory
        {
            public static IReadOnlyCollection<OrderLine> CreateFrom(
                IReadOnlyCollection<ProductRequest> productRequests)
            {
                if (productRequests == null)
                    throw new ArgumentException("Products must be provided.");

                if (IsDiscountGreaterThanProductPrice(productRequests))
                    throw new ArgumentException("Invalid discount amount.");

                List<OrderLine> orderLines = new List<OrderLine>()
                {
                    new OrderLine()
                };

                return orderLines;
            }

            private static bool IsDiscountGreaterThanProductPrice(
                IReadOnlyCollection<ProductRequest> productRequests)
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
