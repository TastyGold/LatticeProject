namespace LatticeProject.Game
{
    internal interface IItemInventory
    {
        public bool CanRecieveItem(GameItem item);
        public void RecieveItem(GameItem item, float offset);
    }
}