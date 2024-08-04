namespace LatticeProject.Game
{
    internal class GameItemWithOffset
    {
        public GameItem item;
        public float offset;

        public GameItemWithOffset(GameItem item, float offset)
        {
            this.item = item;
            this.offset = offset;
        }
    }
}
