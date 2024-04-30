using LatticeProject.Utility;
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
            throw new NotImplementedException();
        }

        public override VecInt2[] GetLinePoints(VecInt2 a, VecInt2 b)
        {
            throw new NotImplementedException();
        }

        public override bool IsValidDirection(VecInt2 a, VecInt2 b)
        {
            throw new NotImplementedException();
        }
    }
}