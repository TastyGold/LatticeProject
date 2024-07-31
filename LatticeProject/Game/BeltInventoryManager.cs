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
            if (inventory.CanRecieveItem() && Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.I))
            {
                inventory.AddToHead(1, -BeltInventory.minItemDistance);
            }
            if (Raylib_cs.Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.O))
            {
                inventory.RemoveTailingItem();
            }
        }
    }
}
