namespace LatticeProject.Game.Belts
{
    internal class BeltInventoryManager : IItemReciever
    {
        public BeltInventory inventory = new BeltInventory();
        public IItemReciever? depositInventory;

        public int TotalBeltLength
        {
            get => inventory.TotalBeltLength;
            set => inventory.TotalBeltLength = value;
        }

        //ItemReciever things
        public GameItem? RecievedItem { get; private set; }
        public float RecievedItemOffset { get; private set; }

        public float AvailableDistance { get; private set; }

        public bool TryAcceptRecievedItem()
        {
            if (RecievedItem is null) return false;

            inventory.AddToHead(RecievedItem, RecievedItemOffset);
            RecievedItem = null;
            return true;
        }

        public bool TryRecieveItem(GameItem item, float offset)
        {
            if (RecievedItem is not null) return false;

            RecievedItem = item;
            RecievedItemOffset = offset;
            return true;
        }

        //Manager methods
        public void PrepareUpdate(float deltaTime)
        {
            TryAcceptRecievedItem();

            AvailableDistance = inventory.LeadingDistance + deltaTime;
        }

        public void UpdateInventory(float deltaTime)
        {
            depositInventory = this; //temporary

            //manually add/remove items
            if (inventory.CanRecieveItem() && Raylib_cs.Raylib.IsKeyDown(Raylib_cs.KeyboardKey.I))
            {
                inventory.AddToHead(new GameItem(1), -GameRules.minItemDistance);
            }
            if (Raylib_cs.Raylib.IsKeyPressed(Raylib_cs.KeyboardKey.O))
            {
                inventory.RemoveTailingItem();
            }

            //actual belt logic
            bool canTransfer = depositInventory.AvailableDistance >= 0 && depositInventory.RecievedItem is null;
            //note: head of conveyor corresponds to LeadingDistance of -minItemDistance;
            GameItemWithOffset? transferItem = inventory.MoveItems(deltaTime, GameRules.minItemDistance - depositInventory.AvailableDistance, canTransfer);
            if (transferItem is not null)
            {
                depositInventory.TryRecieveItem(transferItem.item, transferItem.offset);
            }
        }
    }
}
