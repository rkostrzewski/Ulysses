namespace Ulysses.Core.Models
{
    public struct Pixel
    {
        private readonly ushort _value;

        public Pixel(ushort value)
        {
            _value = value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Pixel && Equals((Pixel)obj);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static implicit operator int(Pixel p)
        {
            return p._value;
        }

        public static implicit operator ushort(Pixel p)
        {
            return p._value;
        }

        public static implicit operator byte(Pixel p)
        {
            return p._value > byte.MaxValue ? byte.MaxValue : (byte)p._value;
        }

        public static implicit operator Pixel(ushort value)
        {
            return new Pixel(value);
        }

        public static implicit operator Pixel(int value)
        {
            return new Pixel((ushort)(value >= 0 ? value : 0));
        }

        public static Pixel operator +(Pixel p1, Pixel p2)
        {
            var sum = p1._value + p2._value;
            return new Pixel((ushort)sum);
        }

        public static Pixel operator -(Pixel p1, Pixel p2)
        {
            var difference = p1._value - p2._value;
            return new Pixel((ushort)(difference >= 0 ? difference : 0));
        }

        public static Pixel operator *(Pixel p1, Pixel p2)
        {
            var product = p1._value * p2._value;
            return new Pixel((ushort)product);
        }

        public static Pixel operator /(Pixel p1, Pixel p2)
        {
            var division = p1._value / p2._value;
            return new Pixel((ushort)division);
        }

        public static explicit operator bool(Pixel p)
        {
            return p._value > 0;
        }

        public static bool operator ==(Pixel p1, Pixel p2)
        {
            return p1._value == p2._value;
        }

        public static bool operator !=(Pixel p1, Pixel p2)
        {
            return p1._value != p2._value;
        }

        public bool Equals(Pixel other)
        {
            return _value == other._value;
        }
    }
}