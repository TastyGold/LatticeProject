using LatticeProject.Utility;
using Raylib_cs;
using System.Numerics;

namespace LatticeProject.Lattices
{
    internal abstract class Lattice
    {
        public abstract VecInt2[] GetNeighbourOffsets();
        public abstract Vector2[] GetGridPieceOffsets();
        public abstract bool IsValidDirection(VecInt2 a, VecInt2 b);
        public abstract Vector2 GetCartesianCoords(int x, int y);
        public Vector2 GetCartesianCoords(VecInt2 vertex) => GetCartesianCoords(vertex.x, vertex.y);
        public abstract VecInt2 GetClosestVertex(Vector2 cartesianCoods);
        public abstract int GetManhattanDistance(VecInt2 a, VecInt2 b);
        public abstract VecInt2[] GetLinePoints(VecInt2 a, VecInt2 b);
        public abstract int GetDirectionIndex(VecInt2 a, VecInt2 b);
        public abstract void HighlightCell(VecInt2 vertex, float scale, Color col);
    }
}