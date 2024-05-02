namespace LatticeProject.Game
{
    internal class BeltInventory : ItemInventory
    {
        public List<GameItem> items = new List<GameItem>();
        public List<float> interItemDistances = new List<float>();

        public int totalBeltLength = 0;

        public float leadingBeltDistance = 0;
        public float trailingBeltDistance = 0;

        public const float minItemDistance = 1;
        public int lastNonZeroDistanceIdx = 0;

        public void AddItem(float distance)
        {
            interItemDistances.Add(distance);
            items.Add(new GameItem());
        }

        public override bool CanRecieveItem(GameItem item)
        {
            return leadingBeltDistance > minItemDistance;
        }

        public override void RecieveItem(GameItem item, float offset)
        {

        }
    }

    internal abstract class ItemInventory
    {
        public abstract bool CanRecieveItem(GameItem item);
        public abstract void RecieveItem(GameItem item, float offset);
    }
}