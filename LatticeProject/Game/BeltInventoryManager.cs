namespace LatticeProject.Game
{
    internal class BeltInventoryManager
    {
        public BeltInventory inventory = new BeltInventory();

        public int TotalBeltLength
        {
            get => inventory.TotalBeltLength;
            set => inventory.TotalBeltLength = value;
        }

        public void UpdateInventory(float deltaTime)
        {
            inventory.MoveItems(deltaTime * 5f);
            if (inventory.CanRecieveItem())
            {
                inventory.AddToHead(1, deltaTime * 0.5f);
            }

            if (inventory.trailingDistance < 0)
            {
                inventory.RemoveLast();
            }
        }
    }
}
