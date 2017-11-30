using System;

namespace CodeCatalog.DDD.Domain.Types
{
    public struct OrderLineId: IEquatable<OrderLineId>
    {
        private readonly ulong _value;
        public static implicit operator ulong(OrderLineId id) => id._value;
        public static implicit operator long(OrderLineId id) => (long)id._value;
        public static explicit operator OrderLineId(ulong id) => new OrderLineId(id);

        private OrderLineId(ulong value)
        {
            _value = value;
        }

        public bool Equals(OrderLineId other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (obj is OrderLineId other)
                return Equals(other);

            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(OrderLineId left, OrderLineId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(OrderLineId left, OrderLineId right)
        {
            return !left.Equals(right);
        }

        public override string ToString() => _value.ToString();
    }
}
