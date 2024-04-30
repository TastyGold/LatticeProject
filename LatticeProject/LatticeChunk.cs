namespace LatticeProject
{
    internal class LatticeChunk
    {
        public List<BeltSegment> beltSegments = new List<BeltSegment>();
    }

    internal static class LatticeChunkRenderer
    {
        public static void DrawAllBeltSegments(Lattice lattice, LatticeChunk chunk)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < chunk.beltSegments.Count; i++)
                {
                    BeltRenderer.DrawBeltSegment(lattice, chunk.beltSegments[i], j == 0, i);
                    BeltRenderer.DrawBeltItems(lattice, chunk.beltSegments[i]);
                }
            }
        }
    }
}