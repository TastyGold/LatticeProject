namespace LatticeProject.Game
{
    internal class BeltInventory : ItemInventory
    {
        public List<GameItem> items = new List<GameItem>();
        public List<float> interItemDistances = new List<float>();

        public int totalBeltLength = 0;
        public int beltSpeed = 5;

        public float LeadingBeltDistance
        {
            get
            {
                if (interItemDistances.Count > 0) return interItemDistances[0];
                else return 0;
            }
            set
            {
                if (interItemDistances.Count > 0) interItemDistances[0] = value;
                else return;
            }
        } //uses the first element of interItemDistances
        public float trailingBeltDistance = 0;

        public const float minItemDistance = 2/3f;
        public int lastNonZeroDistanceIdx = 0;

        public void AddItem(float distance)
        {
            interItemDistances.Add(distance);
            items.Add(new GameItem());
        }
        
        public void Update(float deltaTime)
        {
            LeadingBeltDistance += deltaTime * beltSpeed;
            trailingBeltDistance -= deltaTime * beltSpeed;
            if (LeadingBeltDistance > minItemDistance)
            {
                RecieveItem(new GameItem(Core.GameManager.nextColor), LeadingBeltDistance - minItemDistance);
                Core.GameManager.nextColor++;
                Core.GameManager.nextColor %= Rendering.Colors.numColors;
            }
            Console.WriteLine($"ItemCount: {items.Count}, DistCount: {interItemDistances.Count}");
        }

        public override bool CanRecieveItem(GameItem item)
        {
            return LeadingBeltDistance > minItemDistance;
        }

        public override void RecieveItem(GameItem item, float offset)
        {
            interItemDistances.Insert(0, LeadingBeltDistance - offset);
            LeadingBeltDistance = offset;
            items.Insert(0, item);
        }
    }

    internal abstract class ItemInventory
    {
        public abstract bool CanRecieveItem(GameItem item);
        public abstract void RecieveItem(GameItem item, float offset);
    }
}