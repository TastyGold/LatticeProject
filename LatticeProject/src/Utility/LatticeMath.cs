using System.Numerics;

namespace LatticeProject.Utility
{
    internal static class LatticeMath
    {
        public const float sqrt3 = 1.73205080757f;
        public const float sqrt3_2 = 0.86602540378f;
        public const float sqrt3_3 = 0.57735026919f;
        public static readonly Vector2 epsilon = new Vector2(1e-3f, 1e-3f);

        public static readonly VecInt2[] hexNeighbours =
        {
            new VecInt2(1, 0),  //right
            new VecInt2(0, 1),  //down right
            new VecInt2(-1, 1), //down left
            new VecInt2(-1, 0), //left
            new VecInt2(0, -1), //up left
            new VecInt2(1, -1), //up right
        };

        public static int GetS(int q, int r)
        {
            return -q - r;
        }

        public static int Modulo(int value, int mod)
        {
            int r = value % mod;
            return r < 0 ? r + mod : r;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a * (1 - t) + b * t;
        }

        public static bool IsNearlyEqual(this float a, float b, float epsilon)
        {
            return Math.Abs(a - b) <= epsilon;
        }
    }
}