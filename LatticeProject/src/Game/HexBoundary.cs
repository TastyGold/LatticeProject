namespace LatticeProject.Game
{
    internal class HexBoundary
    {
        public int minQ, maxQ; //vertical
        public int minR, maxR; //horizontal
        public int minS, maxS; //slant

        public HexBoundary() { }

        public HexBoundary(int minQ, int maxQ, int minR, int maxR, int minS, int maxS)
        {
            this.minQ = minQ;
            this.maxQ = maxQ;
            this.minR = minR;
            this.maxR = maxR;
            this.minS = minS;
            this.maxS = maxS;
        }
    }
}