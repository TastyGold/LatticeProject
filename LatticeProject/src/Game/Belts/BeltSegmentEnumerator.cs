using LatticeProject.Lattices;
using LatticeProject.Utility;
using System.Collections;
using System.Net.Http.Headers;

namespace LatticeProject.Game.Belts
{
    /// <summary>
    /// Iterates over every VecInt2 that a belt segment inhabits
    /// </summary>
    internal class BeltSegmentEnumerator : IEnumerator<VecInt2>
    {
        private static readonly HexagonLattice lattice = new();

        private readonly List<VecInt2> vertices;
        private int currentVertexIndex;
        private VecInt2 previousVertexDirection;
        private int stepsRemaining;

        private VecInt2 _current;
        public VecInt2 Current => _current;

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            stepsRemaining--;

            if (stepsRemaining < 0)
            {
                currentVertexIndex++;
                if (vertices.Count <= currentVertexIndex) return false;

                SetTargetVertex(currentVertexIndex);
            }

            _current = vertices[currentVertexIndex] + previousVertexDirection * stepsRemaining;
            return true;
        }

        public void Reset()
        {
            currentVertexIndex = 0;
            _current = vertices[0];
            stepsRemaining = 1;
            if (vertices.Count > 1)
            {
                SetTargetVertex(currentVertexIndex);
            }
            else
            {
                previousVertexDirection = VecInt2.Zero;
                stepsRemaining = 0;
            }
        }

        public static VecInt2 GetDirection(VecInt2 start, VecInt2 destination)
        {
            int dirIndex = lattice.GetDirectionIndex(start, destination);
            return dirIndex <= -1 ? VecInt2.Zero : LatticeMath.hexNeighbours[dirIndex];
        }

        public static int GetNumberOfSteps(VecInt2 start, VecInt2 destination)
        {
            VecInt2 dir = destination - start;
            if (dir == VecInt2.Zero) return 0;
            if (dir.x == 0) return Math.Abs(dir.y);
            if (dir.y == 0) return Math.Abs(dir.x);
            if (Math.Abs(dir.x) == Math.Abs(dir.y)) return Math.Abs(dir.x);
            return 0;
        }

        public void SetTargetVertex(int index)
        {
            if (index < 1) return;
            previousVertexDirection = GetDirection(vertices[index], vertices[index - 1]);
            stepsRemaining = GetNumberOfSteps(vertices[index], vertices[index - 1]) - 1;
        }

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public BeltSegmentEnumerator(List<VecInt2> vertices)
        {
            this.vertices = vertices;
            Reset();
        }
    }
}