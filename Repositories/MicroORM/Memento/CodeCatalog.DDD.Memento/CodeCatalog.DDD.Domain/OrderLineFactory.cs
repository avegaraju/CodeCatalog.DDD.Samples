using System;
using System.Collections.Generic;
using System.Linq;
using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCases;

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

                if(productRequests.Any(pr=> pr.Quantity < 1))
                    throw new ArgumentException("Quantity cannot be less than 1.");

                return productRequests
                    .Select(productRequest => new OrderLine(productRequest.ProductId,
                        productRequest.Discount,
                        productRequest.Price,
                        productRequest.Quantity)).ToList();
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
