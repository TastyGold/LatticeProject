using Raylib_cs;
using System.Numerics;

namespace LatticeProject
{
    internal static class LatticeRenderer
    {
        public static float scale;

        public static void DrawVertices(Lattice lattice, int minX, int minY, int maxX, int maxY)
        {
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    Vector2 vertex = lattice.GetCartesianCoords(x, y);
                    Raylib.DrawCircleV(vertex * scale, 4, new Color(134,146,164,255));
                }
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
                        (center - new Vector2(0.5f, (HexagonLattice.sqrt3_2 - HexagonLattice.sqrt3_3))) * scale,
                        (center - new Vector2(0, HexagonLattice.sqrt3_3)) * scale,
                        lineWidth, new Color(33, 38, 45, 255));
                    Raylib.DrawLineEx(
                        (center - new Vector2(-0.5f, (HexagonLattice.sqrt3_2 - HexagonLattice.sqrt3_3))) * scale,
                        (center - new Vector2(0, HexagonLattice.sqrt3_3)) * scale,
                        lineWidth, new Color(33, 38, 45, 255));
                    Raylib.DrawLineEx(
                        (center + new Vector2(-0.5f, (HexagonLattice.sqrt3_2 - HexagonLattice.sqrt3_3))) * scale,
                        (center - new Vector2(0.5f, (HexagonLattice.sqrt3_2 - HexagonLattice.sqrt3_3))) * scale,
                        lineWidth, new Color(33, 38, 45, 255));
                }
            }
        }

        public static void HighlightNeighbours(Lattice lattice, VecInt2 vertex)
        {
            VecInt2[] nOffsets = lattice.GetNeighbourOffsets();
            for (int i = 0; i < nOffsets.Length; i++)
            {
                Raylib.DrawCircleV(lattice.GetCartesianCoords(vertex + nOffsets[i]) * scale, scale / 10f, Color.Purple);
            }
        }
    }
}