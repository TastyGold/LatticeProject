namespace LatticeProject.Game
{
    internal class GameItem
    {
        public int color;

        public GameItem()
        {
            color = 0;
        }

        public GameItem(int color)
        {
            this.color = color;
        }

        public override bool Equals(object? obj)
        {
            return obj is not null
                && obj is GameItem item
                && item.color == color;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(color);
        }

        public override string? ToString()
        {
            return color.ToString();
        }
    }
}