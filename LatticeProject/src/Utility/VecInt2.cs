namespace LatticeProject.Utility
{
    internal struct VecInt2
    {
        public int x;
        public int y;

        public static VecInt2 Zero = new VecInt2(0, 0);
        public static VecInt2 One = new VecInt2(1, 1);
        public static VecInt2 Up = new VecInt2(0, -1);
        public static VecInt2 Right = new VecInt2(1, 0);
        public static VecInt2 Down = new VecInt2(0, 1);
        public static VecInt2 Left = new VecInt2(-1, 0);

        public static VecInt2 operator +(VecInt2 a, VecInt2 b)
        {
            return new VecInt2(a.x + b.x, a.y + b.y);
        }
        public static VecInt2 operator -(VecInt2 a, VecInt2 b)
        {
            return new VecInt2(a.x - b.x, a.y - b.y);
        }
        public static VecInt2 operator *(VecInt2 v, int m)
        {
            return new VecInt2(v.x * m, v.y * m);
        }
        public static VecInt2 operator /(VecInt2 v, int m)
        {
            return new VecInt2(v.x / m, v.y / m);
        }

        public static bool operator ==(VecInt2 a, VecInt2 b)
        {
            return !(a != b);
        }
        public static bool operator !=(VecInt2 a, VecInt2 b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public static VecInt2 Rotate(VecInt2 v, int angle)
        {
            return (angle % 4) switch
            {
                0 => new VecInt2(v.x, v.y),
                1 => new VecInt2(v.y, -v.x),
                2 => new VecInt2(-v.x, -v.y),
                _ => new VecInt2(-v.y, v.x),
            };
        }

        public override string ToString()
        {
            return $"<{x}, {y}>";
        }

        public override bool Equals(object? obj)
        {
            return obj is VecInt2 @int &&
                   x == @int.x &&
                   y == @int.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public VecInt2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}