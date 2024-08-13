using LatticeProject.Utility;
using Raylib_cs;
using System.Numerics;

namespace LatticeProject.Lattices
{
    internal class SquareLattice : Lattice
    {
        private static readonly VecInt2[] nOffsets =
        {
            new VecInt2(1, 0),
            new VecInt2(0, 1),
            new VecInt2(-1, 0),
            new VecInt2(0, -1),
        };
        public override VecInt2[] GetNeighbourOffsets() => nOffsets;

        private static readonly Vector2[] gOffsets =
        {
            new Vector2(-0.5f, 0.5f),
            new Vector2(-0.5f, -0.5f),
            new Vector2(0.5f, -0.5f)
        };
        public override Vector2[] GetGridPieceOffsets() => gOffsets;

        public override int GetDirectionIndex(VecInt2 a, VecInt2 b)
        {
            VecInt2 dv = b - a;
            if (dv.y == 0) dv.x /= dv.x;
            if (dv.x == 0) dv.y /= dv.y;

            return Array.IndexOf(nOffsets, dv);
        }

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

        public override int GetManhattanDistance(VecInt2 a, VecInt2 b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        public override VecInt2[] GetLinePoints(VecInt2 a, VecInt2 b)
        {
            throw new NotImplementedException();
        }

        public override bool IsValidDirection(VecInt2 a, VecInt2 b)
        {
            throw new NotImplementedException();
        }

        public override void HighlightCell(VecInt2 vertex, float scale, Color col)
        {
            Raylib.DrawRectangleV((GetCartesianCoords(vertex) - new Vector2(0.5f, 0.5f)) * scale, Vector2.One * scale, col);
        }
    }
}