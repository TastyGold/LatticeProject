namespace LatticeProject
{
    internal class LatticeObjectManager
    {
        public List<LatticeEdge> edges = new List<LatticeEdge>();

        public void AddEdge(int startX, int startY, int endX, int endY)
        {
            edges.Add(new LatticeEdge(startX, startY, endX, endY));
        }
        public void AddEdge(VecInt2 start, VecInt2 end)
        {
            edges.Add(new LatticeEdge(start, end));
        }
    }
}