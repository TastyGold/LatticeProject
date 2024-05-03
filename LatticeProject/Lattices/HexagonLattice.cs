using LatticeProject.Utility;
using System.Numerics;
using static LatticeProject.Utility.LatticeMath;

namespace LatticeProject.Lattices
{
    internal class HexagonLattice : Lattice
    {
        private static readonly VecInt2[] nOffsets = hexNeighbours;
        public override VecInt2[] GetNeighbourOffsets() => nOffsets;

        public override bool IsValidDirection(VecInt2 a, VecInt2 b)
        {
            VecInt2 difference = a - b;
            return difference.x == 0 || difference.y == 0 || difference.x == -difference.y;
        }

        public override int GetDirectionIndex(VecInt2 a, VecInt2 b)
        {
            VecInt2 dv = b - a;

            if (dv == VecInt2.Zero) return -1;
            else if (dv.y == 0) dv.x = Math.Sign(dv.x);
            else if (dv.x == 0) dv.y = Math.Sign(dv.y);
            else if (dv.x == -dv.y) dv = new VecInt2(Math.Sign(dv.x), Math.Sign(dv.y));

            return Array.IndexOf(nOffsets, dv);
        }

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

        public override int GetManhattanDistance(VecInt2 a, VecInt2 b)
        {
            int dq = Math.Abs(a.x - b.x);
            int dr = Math.Abs(a.y - b.y);

            int sa = -a.x - a.y;
            int sb = -b.x - b.y;

            int ds = Math.Abs(sa - sb);

            return Math.Max(dq, Math.Max(dr, ds));
        }

        public override VecInt2[] GetLinePoints(VecInt2 a, VecInt2 b)
        {
            int distance = GetManhattanDistance(a, b);

            Vector2 position = GetCartesianCoords(a);
            Vector2 step = (GetCartesianCoords(b) - position) / distance;

            VecInt2[] points = new VecInt2[distance + 1];
            for (int i = 0; i < distance; i++)
            {
                points[i] = GetClosestVertex(position + epsilon);
                position += step;
            }
            points[^1] = GetClosestVertex(position);

            return points;
        }
    }
}