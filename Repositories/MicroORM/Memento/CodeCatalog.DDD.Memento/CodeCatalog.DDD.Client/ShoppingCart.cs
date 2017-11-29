using System.Collections;
using System.Collections.Generic;

using CodeCatalog.DDD.Domain.Types;
using CodeCatalog.DDD.Domain.UseCases;

namespace CodeCatalog.DDD.Client
{
    public class ShoppingCart
    {
        private readonly OrderRequest _orderRequest;
        private readonly IList<ProductRequest> _productRequests;

        public ShoppingCart()
        {
            _orderRequest = new OrderRequest();
            _productRequests = new List<ProductRequest>();
        }
        public OrderRequest BuildOrderRequest()
        {
            return _orderRequest;
        }

        public ShoppingCart AddProduct(ulong productid,
                                       decimal discount,
                                       decimal price,
                                       uint quantity)
        {
            var productRequest = new ProductRequest()
                                 {
                                     ProductId = (ProductId)productid,
                                     Discount = discount,
                                     Price = price,
                                     Quantity = quantity
                                 };

            _productRequests.Add(productRequest);

            return this;
        }

        public ShoppingCart ForCustomer(CustomerId customerId, bool isPrivilegeCustomer = true)
        {
            _orderRequest.CustomerId = customerId;
            _orderRequest.IsPrivilegeCustomer = isPrivilegeCustomer;

            return this;
        }
    }
}
