namespace LatticeProject
{
    public static class LatticeMath
    {
        public static int Modulo(int value, int mod)
        {
            int r = value % mod;
            return r < 0 ? r + mod : r;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a * (1 - t) + (b * t);
        }
    }
}