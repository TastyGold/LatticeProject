namespace LatticeProject.Game.Belts
{
    internal class BeltInventoryElement
    {
        public GameItem item;
        public float distance;
        public int count;

        public bool ItemDistanceMatches(GameItem item, float distance)
        {
            return this.item == item
                && this.distance == distance;
        }

        public override string? ToString()
        {
            return $"id={item}, dist={distance}, count={count}";
        }

        public BeltInventoryElement(GameItem item, float distance, int count)
        {
            this.item = item;
            this.distance = distance;
            this.count = count;
        }
    }
}
