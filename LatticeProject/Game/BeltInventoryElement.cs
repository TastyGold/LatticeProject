namespace LatticeProject.Game
{
    internal class BeltInventoryElement
    {
        public int itemId;
        public float distance;
        public int count;

        public bool ItemDistanceMatches(int itemId, float distance)
        {
            return this.itemId == itemId
                && this.distance == distance;
        }

        public override string? ToString()
        {
            return $"id={itemId}, dist={distance}, count={count}";
        }

        public BeltInventoryElement(int itemId, float distance, int count)
        {
            this.itemId = itemId;
            this.distance = distance;
            this.count = count;
        }
    }
}
