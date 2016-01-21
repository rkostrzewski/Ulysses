namespace Ulysses.Core.Models
{
    public struct Pixel
    {
        private readonly ushort _value;

        public Pixel(ushort value)
        {
            _value = value;
        }

        public static implicit operator byte (Pixel p)
        {
            return p._value > byte.MaxValue ? byte.MaxValue : (byte)p._value;
        }

        public static implicit operator ushort(Pixel p)
        {
            return p._value;
        }

        public static implicit operator int (Pixel p)
        {
            return p._value;
        }

        public static implicit operator Pixel(byte value)
        {
            return new Pixel(value);
        }

        public static implicit operator Pixel(ushort value)
        {
            return new Pixel(value);
        }

        public static implicit operator Pixel(int value)
        {
            return new Pixel(value > 0 ? value > ushort.MaxValue ? ushort.MaxValue : (ushort)value : (ushort)0);
        }

        public static Pixel operator +(Pixel p1, Pixel p2)
        {
            return p1._value + p2._value;
        }

        public static Pixel operator -(Pixel p1, Pixel p2)
        {
            return p1._value - p2._value;
        }

        public static Pixel operator *(Pixel p1, Pixel p2)
        {
            var product = (long)p1._value * p2._value;
            
            return product > ushort.MaxValue ? ushort.MaxValue : (ushort)product;
        }

        public static bool operator ==(Pixel p1, Pixel p2)
        {
            return p1._value == p2._value;
        }

        public static bool operator !=(Pixel p1, Pixel p2)
        {
            return p1._value != p2._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Pixel && Equals((Pixel)obj);
        }

        public bool Equals(Pixel other)
        {
            return _value == other._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}