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
            double x = coords.X;
            double y = coords.Y;

            //Algorithm used:
            //https://www.redblobgames.com/grids/hexagons/more-pixel-to-hex.html#chris-cox

            double t = sqrt3 * y + 1;           // scaled y, plus phase
            double temp1 = Math.Floor(t + x);   // (y+x) diagonal, this calc needs floor
            double temp2 = t - x;               // (y-x) diagonal, no floor needed
            double temp3 = 2 * x + 1;           // scaled horizontal, no floor needed, needs +1 to get correct phase

            double qf = (temp1 + temp3) / 3.0;  // pseudo x with fraction
            double rf = (temp1 + temp2) / 3.0;  // pseudo y with fraction

            int q = (int)Math.Floor(qf);        // pseudo x, quantized and thus requires floor
            int r = (int)Math.Floor(rf);        // pseudo y, quantized and thus requires floor

            return new VecInt2(q - r, r);
        }
    }
}