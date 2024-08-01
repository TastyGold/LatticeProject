namespace LatticeProject.Game
{
    internal class BeltInventoryManager : IItemInventory
    {
        public BeltInventory inventory = new BeltInventory();
        public IItemInventory? depositInventory;

        public int TotalBeltLength
        {
            get => inventory.TotalBeltLength;
            set => inventory.TotalBeltLength = value;
        }

        public bool CanRecieveItem(GameItem item)
        {
            return inventory.CanRecieveItem();
        }

        public void RecieveItem(GameItem item, float offset)
        {
            inventory.AddToHead(item.color, offset);
        }

        public void UpdateInventory(float deltaTime)
        {
            depositInventory = this;
            inventory.MoveItems(deltaTime);
            if (inventory.CanRecieveItem() && Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.I))
            {
                inventory.AddToHead(1, -GameRules.minItemDistance);
            }
            if (Raylib_cs.Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.O))
            {
                inventory.RemoveTailingItem();
            }

            if (inventory.items.First?.Value.distance == GameRules.minItemDistance && depositInventory is not null)
            {
                GameItem itemToTransfer = new GameItem(inventory.items.First.Value.itemId);
                if (depositInventory.CanRecieveItem(itemToTransfer))
                {
                    // need to do this more effectively (need to change the offset on recieve item)
                    // will need to change moveItems() method to fix this

                    depositInventory.RecieveItem(itemToTransfer, -GameRules.minItemDistance);
                    inventory.RemoveTailingItem();
                }
            }
        }
    }
}
