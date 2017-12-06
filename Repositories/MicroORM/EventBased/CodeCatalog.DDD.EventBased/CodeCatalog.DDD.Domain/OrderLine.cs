using CodeCatalog.DDD.Domain.Types;

namespace CodeCatalog.DDD.Domain
{
    public partial class OrderLine
    {
        internal OrderLineId? OrderLineId { get; set; }
        internal ProductId ProductId { get; }
        internal decimal Discount { get; }
        internal decimal Price { get; }
        internal uint Quantity { get; }

        private OrderLine(OrderLineId? orderLineId,
                          ProductId productId,
                          decimal discount,
                          decimal price,
                          uint quantity)
        {
            OrderLineId = orderLineId;
            ProductId = productId;
            Discount = discount;
            Price = price;
            Quantity = quantity;
        }

        public OrderLineState GetState()
        {
            return new OrderLineState()
                   {
                       OrderLineId = this.OrderLineId,
                       ProductId = this.ProductId,
                       Discount = this.Discount,
                       Price = this.Price,
                       Quantity = this.Quantity
                   };
        }
    }
}