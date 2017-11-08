using System;
using System.Collections.Generic;
using System.Linq;
using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCase;

namespace CodeCatalog.DDD.Domain
{
    public partial class Product
    {
        public static class ProductFactory
        {
            public static IReadOnlyCollection<Product> CreateFrom(
                IReadOnlyCollection<ProductRequest> productRequests)
            {
                if(productRequests == null)
                    throw new ArgumentException("Products must be provided.");

                if (IsDiscountGreaterThanProductPrice(productRequests))
                    throw new ArgumentException("Invalid discount amount.");

                IList<Product> products = new List<Product>();
                foreach (var productRequest in productRequests)
                {
                    products.Add(new Product(productRequest.ProductId,
                                            productRequest.Discount,
                                            productRequest.Price));
                }

                return products.ToList();
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
