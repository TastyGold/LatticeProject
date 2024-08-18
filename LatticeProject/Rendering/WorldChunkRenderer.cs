using LatticeProject.Game;
using LatticeProject.Game.Belts;
using LatticeProject.Lattices;

namespace LatticeProject.Rendering
{
    internal static class WorldChunkRenderer
    {
        public static void DrawAllBeltSegments(Lattice lattice, WorldChunk chunk)
        {
            chunk.beltSegments.ForEach((belt) => BeltRenderer.DrawBeltOutline(lattice, belt));
            chunk.beltSegments.ForEach((belt) => BeltRenderer.DrawBeltConveyor(lattice, belt));
            chunk.beltSegments.ForEach((belt) => BeltItemRenderer.DrawBeltItems(lattice, belt));
        }
    }
}