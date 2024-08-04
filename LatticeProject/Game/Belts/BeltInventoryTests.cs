namespace LatticeProject.Game.Belts
{
    internal static class BeltInventoryTests
    {
        public static void RunTests()
        {
            Console.WriteLine("Running BeltInventory tests:\n");

            BeltInventory inv = new BeltInventory();
            inv.TotalBeltLength = 10;

            Console.WriteLine(inv.GetInventoryDescription());

            inv.AddToHead(new GameItem(0), 9);
            for (int i = 0; i < 10; i++)
            {
                inv.AddToHead(new GameItem(0), 9);
            }
            inv.AddToHead(new GameItem(0), -10);

            Console.WriteLine(inv.GetInventoryDescription());

            while (inv.Count > 0)
            {
                while (inv.items.Count > 1)
                {
                    Console.Clear();
                    inv.MoveItems(0.0002f, GameRules.minItemDistance, false);
                    Console.WriteLine(inv.GetInventoryDescription());
                }
                inv.RemoveTailingItem();
            }
        }
    }
}
