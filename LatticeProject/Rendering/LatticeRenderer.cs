using LatticeProject.Lattices;
using LatticeProject.Utility;
using Raylib_cs;
using System.Numerics;
using static LatticeProject.Utility.LatticeMath;
using static LatticeProject.Rendering.RenderConfig;

namespace LatticeProject.Rendering
{
    internal static class RenderConfig
    {
        public static float scale;
    }

    internal static class LatticeRenderer
    {
        public static void DrawVertex(Lattice lattice, VecInt2 vertex, float size, Color col)
        {
            Raylib.DrawCircleV(lattice.GetCartesianCoords(vertex) * scale, size * scale, col);
        }

        public static void DrawVertices(Lattice lattice, VecInt2[] points, float size, Color col)
        {
            for (int i = 0; i < points.Length; i++)
            {
                DrawVertex(lattice, points[i], size, col);
            }
        }

        public static void DrawVertices(Lattice lattice, int minX, int minY, int maxX, int maxY, float size, Color col)
        {
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    DrawVertex(lattice, new VecInt2(x, y), size, col);
                }
            }
        }

        public static void HighlightNeighbours(Lattice lattice, VecInt2 vertex)
        {
            VecInt2[] nOffsets = lattice.GetNeighbourOffsets();
            for (int i = 0; i < nOffsets.Length; i++)
            {
                DrawVertex(lattice, vertex + nOffsets[i], 0.1f, Color.Purple);
            }
        }

        public static void DrawHexagonalGrid(Lattice lattice, float lineWidth, int minX, int minY, int maxX, int maxY)
        {
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    Vector2 center = lattice.GetCartesianCoords(x, y);
                    Raylib.DrawLineEx(
                        (center - new Vector2(0.5f, sqrt3_2 - sqrt3_3)) * scale,
                        (center - new Vector2(0, sqrt3_3)) * scale,
                        lineWidth, new Color(33, 38, 45, 255));
                    Raylib.DrawLineEx(
                        (center - new Vector2(-0.5f, sqrt3_2 - sqrt3_3)) * scale,
                        (center - new Vector2(0, sqrt3_3)) * scale,
                        lineWidth, new Color(33, 38, 45, 255));
                    Raylib.DrawLineEx(
                        (center + new Vector2(-0.5f, sqrt3_2 - sqrt3_3)) * scale,
                        (center - new Vector2(0.5f, sqrt3_2 - sqrt3_3)) * scale,
                        lineWidth, new Color(33, 38, 45, 255));
                }
            }
        }
    }
}