using LatticeProject.Game;
using LatticeProject.Lattices;

namespace LatticeProject.Rendering
{
    internal static class WorldChunkRenderer
    {
        public static void DrawAllBeltSegments(Lattice lattice, WorldChunk chunk)
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