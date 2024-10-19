using LatticeProject.Game;
using LatticeProject.Lattices;
using LatticeProject.Utility;
using Raylib_cs;

namespace LatticeProject.Rendering
{
    internal static class BoundaryRenderer
    {
        public static void DrawHexBoundaryLines(Lattice lattice, HexBoundary bounds)
        {
            VecInt2[] c = new VecInt2[6]
            {
                new(bounds.minQ, bounds.maxR),
                new(bounds.minQ, -bounds.minQ - bounds.maxS),
                new(-bounds.minR - bounds.maxS, bounds.minR),
                new(bounds.maxQ, bounds.minR),
                new(bounds.maxQ, -bounds.maxQ - bounds.minS),
                new(-bounds.maxR - bounds.minS, bounds.maxR)
            };

            DrawBoundaryLine(lattice, c[0], c[1], GameColors.axisColorQ);
            DrawBoundaryLine(lattice, c[1], c[2], GameColors.axisColorS);
            DrawBoundaryLine(lattice, c[2], c[3], GameColors.axisColorR);
            DrawBoundaryLine(lattice, c[3], c[4], GameColors.axisColorQ);
            DrawBoundaryLine(lattice, c[4], c[5], GameColors.axisColorS);
            DrawBoundaryLine(lattice, c[5], c[0], GameColors.axisColorR);
        }

        public static void DrawBoundaryLine(Lattice lattice, VecInt2 a, VecInt2 b,  Color col)
        {
            Raylib.DrawLineV(lattice.GetCartesianCoords(a) * RenderConfig.scale, lattice.GetCartesianCoords(b) * RenderConfig.scale, col);
        }
    }
}