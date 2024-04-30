using System.Numerics;

namespace LatticeProject.Utility
{
    public static class LatticeMath
    {
        public const float sqrt3 = 1.73205080757f;
        public const float sqrt3_2 = 0.86602540378f;
        public const float sqrt3_3 = 0.57735026919f;
        public static readonly Vector2 epsilon = new Vector2(1e-3f, 1e-3f);

        public static int Modulo(int value, int mod)
        {
            int r = value % mod;
            return r < 0 ? r + mod : r;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a * (1 - t) + b * t;
        }
    }
}