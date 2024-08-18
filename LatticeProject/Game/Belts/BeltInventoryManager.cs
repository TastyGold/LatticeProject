using LatticeProject.Utility;

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

        public VecInt2 RecieverTile { get; set; }

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
        public void UpdateInventory(float deltaTime)
        {
            //actual belt logic
            bool canTransfer = depositInventory is not null && depositInventory.AvailableDistance >= 0 && depositInventory.RecievedItem is null;
            float endOfBeltPadding = depositInventory is not null ? depositInventory.AvailableDistance : 0;

            //note: head of conveyor corresponds to LeadingDistance of -minItemDistance;
            GameItemWithOffset? transferItem = inventory.MoveItems(deltaTime, GameRules.minItemDistance - endOfBeltPadding, canTransfer);
            if (transferItem is not null)
            {
                depositInventory?.TryRecieveItem(transferItem.item, transferItem.offset);
            }
        }

        /// <summary>Should be called after UpdateInventory() to ensure drawing happens correctly</summary>
        public void PrepForNextUpdate(float deltaTime)
        {
            TryAcceptRecievedItem();

            AvailableDistance = inventory.LeadingDistance + deltaTime;
        }
    }
}
