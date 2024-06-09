namespace LatticeProject.Game
{
    internal static class BeltInventoryTests
    {
        public static void RunTests()
        {
            return;
            Console.WriteLine("Running BeltInventory tests:\n");

            BeltInventory inv = new BeltInventory();
            inv.TotalBeltLength = 10;

            Console.WriteLine(inv.GetInventoryDescription());

            inv.AddToHead(0, 9);
            for (int i = 0; i < 10; i++)
            {
                inv.AddToHead(0, 9);
            }
            inv.AddToHead(0, -10);

            Console.WriteLine(inv.GetInventoryDescription());

            while (inv.Count > 0)
            {
                while (inv.items.Count > 1)
                {
                    Console.Clear();
                    inv.MoveItems(0.0002f);
                    Console.WriteLine(inv.GetInventoryDescription());
                }
                inv.RemoveTailingItem();
            }
        }
    }
}
