using LatticeProject.Lattices;
using LatticeProject.Utility;
using System.Numerics;

namespace LatticeProject.Game
{
    internal class BeltSegment
    {
        public BeltInventory inventory = new BeltInventory();

        public List<VecInt2> vertices = new List<VecInt2>();
        public List<int> pieceLengths = new List<int>();
        public int TotalLength
        {
            get => inventory.totalBeltLength;
            private set => inventory.totalBeltLength = value;
        }

        public void SimplifyVertices(Lattice lattice)
        {
            int i = 2;
            while (i < vertices.Count)
            {
                int dir1 = lattice.GetDirectionIndex(vertices[i - 1], vertices[i]);
                int dir2 = lattice.GetDirectionIndex(vertices[i - 2], vertices[i - 1]);

                if (dir1 >= 0 && dir1 == dir2)
                {
                    vertices.RemoveAt(i - 1);
                }
                else i++;
            }
        }

        public void FixInvalidPieces(Lattice lattice)
        {
            //for (int i = 1; i < vertices.Count; i++)
            //{
            //    if (!lattice.IsValidDirection(vertices[i - 1], vertices[i]))
            //    {
            //        vertices.Insert(i - 1, new VecInt2(vertices[i - 1].x, vertices[i].y));
            //        i++;
            //    }
            //}

            throw new NotImplementedException(TotalLength.ToString());
        }

        public void UpdateLengths(Lattice lattice)
        {
            pieceLengths.Clear();
            TotalLength = 0;
            for (int i = 1; i < vertices.Count; i++)
            {
                int distance = lattice.GetManhattanDistance(vertices[i], vertices[i - 1]);
                pieceLengths.Add(distance);
                TotalLength += distance;
            }
        }

        public Vector2 GetPositionAlongBelt(Lattice lattice, float value, bool fromEnd)
        {
            if (fromEnd) value = TotalLength - value;

            if (value <= 0) return lattice.GetCartesianCoords(vertices[0]);

            int vertex = 0;
            while (vertex < pieceLengths.Count && value > pieceLengths[vertex])
            {
                value -= pieceLengths[vertex];
                vertex++;
            }

            if (vertex >= vertices.Count - 1)
            {
                return lattice.GetCartesianCoords(vertices[^1]);
            }

            value /= pieceLengths[vertex];

            return Vector2.Lerp(lattice.GetCartesianCoords(vertices[vertex]), lattice.GetCartesianCoords(vertices[vertex + 1]), value);
        }
    }
}