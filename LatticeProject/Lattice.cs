using Raylib_cs;
using System.Numerics;

namespace LatticeProject
{
    internal abstract class Lattice
    {
        public abstract VecInt2[] GetNeighbourOffsets();
        public abstract Vector2 GetCartesianCoords(int x, int y);
        public Vector2 GetCartesianCoords(VecInt2 vertex) => GetCartesianCoords(vertex.x, vertex.y);
        public abstract VecInt2 GetClosestVertex(Vector2 cartesianCoods);
    }

    internal class SquareLattice : Lattice
    {
        private static readonly VecInt2[] nOffsets =
        {
            new VecInt2(0, -1),
            new VecInt2(-1, 0),
            new VecInt2(1, 0),
            new VecInt2(0, 1),
        };
        public override VecInt2[] GetNeighbourOffsets() => nOffsets;

        public override Vector2 GetCartesianCoords(int x, int y)
        {
            return new Vector2(x, y);
        }
        public override VecInt2 GetClosestVertex(Vector2 coords)
        {
            if (coords.X < 0) coords.X--;
            if (coords.Y < 0) coords.Y--;
            return new VecInt2((int)(coords.X + 0.5f), (int)(coords.Y + 0.5f));
        }
    }

    internal class HexagonLattice : Lattice
    {
        public const float sqrt3 = 1.73205080757f;
        public const float sqrt3_2 = 0.86602540378f;
        public const float sqrt3_3 = 0.57735026919f;

        private static readonly VecInt2[] nOffsets =
        {
            new VecInt2(0, -1),
            new VecInt2(1, -1),
            new VecInt2(-1, 0),
            new VecInt2(1, 0),
            new VecInt2(-1, 1),
            new VecInt2(0, 1),
        };
        public override VecInt2[] GetNeighbourOffsets() => nOffsets;

        public override Vector2 GetCartesianCoords(int x, int y)
        {
            return new Vector2(x + y / 2f, y * sqrt3_2);
        }
        public override VecInt2 GetClosestVertex(Vector2 coords)
        {
            Vector2 mousePos = coords;

            //initial guess point
            coords.Y /= sqrt3_2;
            coords.X = (float)Math.Round(coords.X);
            coords.Y = (float)Math.Round(coords.Y);

            coords.X -= coords.Y / 2;

            int y = (int)coords.Y;
            int x = (int)coords.X;

            VecInt2 initial = new VecInt2(x, y);

            //check for closer neighbour
            Vector2 coordsInitial = GetCartesianCoords(initial.x, initial.y);
            float minDist = Vector2.DistanceSquared(mousePos, coordsInitial);
            int minDistIndex = -1;
            for (int i = 0; i < nOffsets.Length; i++)
            {
                float dist = Vector2.DistanceSquared(mousePos, GetCartesianCoords(initial.x + nOffsets[i].x, initial.y + nOffsets[i].y));
                if (dist < minDist)
                {
                    minDistIndex = i;
                    minDist = dist;
                }
            }

            if (minDistIndex == -1) return initial;
            else return initial + nOffsets[minDistIndex];
        }
    }

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

        public static void DrawHexagonalGrid(Lattice lattice, int minX, int minY, int maxX, int maxY)
        {
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    Vector2 center = lattice.GetCartesianCoords(x, y);
                    Raylib.DrawLineEx(
                        (center - new Vector2(0.5f, (HexagonLattice.sqrt3_2 - HexagonLattice.sqrt3_3))) * scale,
                        (center - new Vector2(0, HexagonLattice.sqrt3_3)) * scale,
                        2, new Color(33, 38, 45, 255));
                    Raylib.DrawLineEx(
                        (center - new Vector2(-0.5f, (HexagonLattice.sqrt3_2 - HexagonLattice.sqrt3_3))) * scale,
                        (center - new Vector2(0, HexagonLattice.sqrt3_3)) * scale,
                        2, new Color(33, 38, 45, 255));
                    Raylib.DrawLineEx(
                        (center + new Vector2(-0.5f, (HexagonLattice.sqrt3_2 - HexagonLattice.sqrt3_3))) * scale,
                        (center - new Vector2(0.5f, (HexagonLattice.sqrt3_2 - HexagonLattice.sqrt3_3))) * scale,
                        2, new Color(33, 38, 45, 255));
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