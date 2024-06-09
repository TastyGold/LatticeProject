namespace LatticeProject.Game
{
    internal class BuildingInventory : ItemInventory
    {
        public override bool CanRecieveItem(GameItem item)
        {
            throw new NotImplementedException();
        }

        public override void RecieveItem(GameItem item, float offset)
        {
            throw new NotImplementedException();
        }
    }

    internal abstract class ItemInventory
    {
        public abstract bool CanRecieveItem(GameItem item);
        public abstract void RecieveItem(GameItem item, float offset);
    }
}