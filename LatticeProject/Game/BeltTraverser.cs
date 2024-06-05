namespace LatticeProject.Game
{
    internal class BeltTraverser
    {
        private readonly BeltSegment segment;

        public int currentVertex = 0;
        public float positionAlongBelt = 0;
        public float positionAlongPiece = 0;

        public void Advance(float distance)
        {
            positionAlongPiece += distance;

            while (currentVertex < segment.pieceLengths.Count && positionAlongPiece > segment.pieceLengths[currentVertex])
            {
                positionAlongPiece -= segment.pieceLengths[currentVertex];
                currentVertex++;

                if (currentVertex >= segment.pieceLengths.Count)
                {
                    positionAlongBelt = segment.TotalLength - distance;
                    positionAlongPiece = 0;
                }
            }

            positionAlongBelt += distance;
        }

        public void Reset()
        {
            currentVertex = 0;
            positionAlongBelt = 0;
            positionAlongPiece = 0;
        }

        public BeltTraverser(BeltSegment segment)
        {
            this.segment = segment;
        }
    }
}