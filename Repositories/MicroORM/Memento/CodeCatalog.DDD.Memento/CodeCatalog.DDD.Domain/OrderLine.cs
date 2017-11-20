using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain
{
    public partial class OrderLine
    {
        internal ProductId ProductId { get; }
        internal double Discount { get; }
        internal double Price { get; }
        internal uint Quantity { get; }

        private OrderLine(ProductId productId,
                          double discount,
                          double price,
                          uint quantity)
        {
            ProductId = productId;
            Discount = discount;
            Price = price;
            Quantity = quantity;
        }

        public OrderLineState GetState()
        {
            return new OrderLineState()
                   {
                       ProductId = this.ProductId,
                       Discount = this.Discount,
                       Price = this.Price,
                       Quantity = this.Quantity
                   };
        }
    }
}