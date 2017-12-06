using System;

namespace CodeCatalog.DDD.Domain.Types
{
    public struct ProductId : IEquatable<ProductId>

    {
        private readonly ulong _value;
        public static implicit operator ulong(ProductId id) => id._value;
        public static implicit operator long(ProductId id) => (long)id._value;
        public static explicit operator ProductId(ulong id) => new ProductId(id);

        private ProductId(ulong value)
        {
            _value = value;
        }

        public bool Equals(ProductId other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (obj is ProductId other)
                return Equals(other);

            return false;
        }
        
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(ProductId left, ProductId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ProductId left, ProductId right)
        {
            return !left.Equals(right);
        }

        public override string ToString() => _value.ToString();
    }
}
