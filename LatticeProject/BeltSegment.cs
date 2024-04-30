using System.Numerics;

namespace LatticeProject
{
    internal class BeltSegment
    {
        public BeltInventory inventory = new BeltInventory();

        public List<VecInt2> vertices = new List<VecInt2>();
        public List<int> pieceLengths = new List<int>();
        private int totalLength = 0;
        public int TotalLength => totalLength;

        public void SimplifyVertices()
        {
            int i = 2;
            while (i < vertices.Count)
            {
                if (GetDirection(vertices[i - 1], vertices[i]) == GetDirection(vertices[i - 2], vertices[i - 1]))
                {
                    vertices.RemoveAt(i - 1);
                }
                else
                {
                    i++;
                }
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
            totalLength = 0;
            for (int i = 1; i < vertices.Count; i++)
            {
                int distance = lattice.GetManhattanDistance(vertices[i], vertices[i - 1]);
                pieceLengths.Add(distance);
                totalLength += distance;
            }
        }

        public static VecInt2 GetDirection(VecInt2 start, VecInt2 end)
        {
            VecInt2 dv = end - start;
            if (dv.x == 0 && dv.y == 0) return VecInt2.Zero;
            if (dv.x == 0) return new VecInt2(0, dv.y / dv.y);
            if (dv.y == 0) return new VecInt2(dv.x / dv.x, 0);
            if (dv.x == -dv.y) return new VecInt2(dv.x / dv.x, dv.y / dv.y);
            else return dv;
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

    internal class BeltInventory
    {
        public List<GameItem> items = new List<GameItem>(); // ordered from end of conveyor to start

        public List<float> interItemDistances = new List<float>();

        public const float minItemDistance = 1;

        public int firstNonZeroDistanceIdx = 0;

        public void AddItem(float distance)
        {
            interItemDistances.Add(distance);
            items.Add(new GameItem());
        }
    }

    internal struct GameItem
    {
        public int id;
    }
}