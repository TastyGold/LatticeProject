namespace LatticeProject
{
    internal class LatticeEdge
    {
        public int startX;
        public int startY;
        public int endX;
        public int endY;

        public VecInt2 Start
        {
            get
            {
                return new VecInt2(startX, startY);
            }
            set
            {
                startX = value.x;
                startY = value.y;
            }
        }
        public VecInt2 End
        {
            get
            {
                return new VecInt2(endX, endY);
            }
            set
            {
                endX = value.x;
                endY = value.y;
            }
        }

        public LatticeEdge(int startX, int startY, int endX, int endY)
        {
            this.startX = startX;
            this.startY = startY;
            this.endX = endX;
            this.endY = endY;
        }
        public LatticeEdge(VecInt2 start, VecInt2 end)
        {
            startX = start.x;
            startY = start.y;
            endX = end.x;
            endY = end.y;
        }
    }
}