using LatticeProject.Game;
using LatticeProject.Lattices;
using LatticeProject.Utility;
using Raylib_cs;

namespace LatticeProject.Rendering
{
    internal static class BoundaryRenderer
    {
        private static readonly Color axisColorQ = new(89, 179, 0, 255);
        private static readonly Color axisColorR = new(0, 153, 250, 255);
        private static readonly Color axisColorS = new(230, 25, 230, 255);

        public static void DrawAxisLine(Lattice lattice, VecInt2 offset, VecInt2 direction, int start, int end, Color col)
        {
            Raylib.DrawLineV(
                lattice.GetCartesianCoords(offset + (direction * start)) * RenderConfig.scale,
                lattice.GetCartesianCoords(offset + (direction * end)) * RenderConfig.scale, 
                col
                );
        }

        public static void DrawHexBoundaryLines(Lattice lattice, HexBoundary bounds)
        {
            DrawAxisLine(lattice, new VecInt2(bounds.minQ, 0), new VecInt2(0, 1), -100, 100, axisColorQ);
            DrawAxisLine(lattice, new VecInt2(bounds.maxQ, 0), new VecInt2(0, 1), -100, 100, axisColorQ);

            DrawAxisLine(lattice, new VecInt2(0, bounds.minR), new VecInt2(1, 0), -100, 100, axisColorR);
            DrawAxisLine(lattice, new VecInt2(0, bounds.maxR), new VecInt2(1, 0), -100, 100, axisColorR);

            DrawAxisLine(lattice, new VecInt2(-bounds.minS, 0), new VecInt2(1, -1), -100, 100, axisColorS);
            DrawAxisLine(lattice, new VecInt2(-bounds.maxS, 0), new VecInt2(1, -1), -100, 100, axisColorS);
        }

        public static void DrawHexBoundaryCorners(Lattice lattice, HexBoundary bounds)
        {
            Raylib.DrawCircleV(lattice.GetCartesianCoords(new VecInt2(bounds.minQ, bounds.maxR)) * RenderConfig.scale, RenderConfig.scale / 5, axisColorS);
            Raylib.DrawCircleV(lattice.GetCartesianCoords(new VecInt2(bounds.maxQ, bounds.minR)) * RenderConfig.scale, RenderConfig.scale / 5, axisColorS);

            Raylib.DrawCircleV(lattice.GetCartesianCoords(new VecInt2(bounds.minQ, -bounds.minQ - bounds.maxS)) * RenderConfig.scale, RenderConfig.scale / 5, axisColorR);
            Raylib.DrawCircleV(lattice.GetCartesianCoords(new VecInt2(bounds.maxQ, -bounds.maxQ - bounds.minS)) * RenderConfig.scale, RenderConfig.scale / 5, axisColorR);

            Raylib.DrawCircleV(lattice.GetCartesianCoords(new VecInt2(-bounds.minR - bounds.maxS, bounds.minR)) * RenderConfig.scale, RenderConfig.scale / 5, axisColorQ);
            Raylib.DrawCircleV(lattice.GetCartesianCoords(new VecInt2(-bounds.maxR - bounds.minS, bounds.maxR)) * RenderConfig.scale, RenderConfig.scale / 5, axisColorQ);
        }
    }
}