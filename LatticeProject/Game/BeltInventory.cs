namespace LatticeProject.Game
{
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
}