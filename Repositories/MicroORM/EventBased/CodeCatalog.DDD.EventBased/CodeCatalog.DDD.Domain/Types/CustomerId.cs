using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCatalog.DDD.Domain.Types
{
    public struct CustomerId : IEquatable<CustomerId>

    {
        private readonly ulong _value;
        public static implicit operator ulong(CustomerId id) => id._value;
        public static implicit operator long(CustomerId id) => (long)id._value;
        public static explicit operator CustomerId(ulong id) => new CustomerId(id);

        private CustomerId(ulong value)
        {
            _value = value;
        }

        public bool Equals(CustomerId other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (obj is CustomerId other)
                return Equals(other);

            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(CustomerId left, CustomerId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CustomerId left, CustomerId right)
        {
            return !left.Equals(right);
        }

        public override string ToString() => _value.ToString();
    }
}
