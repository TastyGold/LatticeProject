namespace LatticeProject.Game.Belts
{
    internal class BeltSegmentTraverser
    {
        private readonly BeltSegment segment;

        public int CurrentVertex { get; private set; }
        public float PositionAlongBelt { get; private set; }
        public float PositionAlongPiece { get; private set; }

        public void Advance(float distance)
        {
            PositionAlongPiece += distance;

            while (CurrentVertex < segment.pieceLengths.Count && PositionAlongPiece > segment.pieceLengths[CurrentVertex])
            {
                PositionAlongPiece -= segment.pieceLengths[CurrentVertex];
                CurrentVertex++;
            }

            if (CurrentVertex >= segment.pieceLengths.Count)
            {
                PositionAlongBelt = segment.TotalLength - distance;
                PositionAlongPiece = 0;
            }
            else PositionAlongBelt += distance;
        }

        public void AdvanceReverse(float distance)
        {
            PositionAlongPiece -= distance;

            while (CurrentVertex > 0 && PositionAlongPiece < 0)
            {
                PositionAlongPiece += segment.pieceLengths[CurrentVertex - 1];
                CurrentVertex--;
            }

            /*if (currentVertex <= 0 && positionAlongPiece < 0)
            {
                positionAlongBelt = 0; //if commented, traverser can be behind head node 
                positionAlongPiece = 0;
            }
            else*/

            PositionAlongBelt -= distance;
        }

        public void Reset()
        {
            CurrentVertex = 0;
            PositionAlongBelt = 0;
            PositionAlongPiece = 0;
        }

        public void ResetEnd()
        {
            CurrentVertex = segment.vertices.Count - 1;
            PositionAlongBelt = segment.TotalLength;
            PositionAlongPiece = 0;
        }

        public BeltSegmentTraverser(BeltSegment segment)
        {
            this.segment = segment;
        }
    }
}