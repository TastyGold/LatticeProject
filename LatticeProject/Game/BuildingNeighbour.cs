namespace LatticeProject.Game
{
    internal struct BuildingNeighbour
    {
        public int footprintIdx;
        public int directionIdx;

        public BuildingNeighbour(int footprintIdx, int directionIdx)
        {
            this.footprintIdx = footprintIdx;
            this.directionIdx = directionIdx;
        }
    }
}