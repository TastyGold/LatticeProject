using LatticeProject.Game.Belts;
using LatticeProject.Utility;

namespace LatticeProject.Game
{
    internal static class TestWorldGenerator
    {
        public static void GenerateStraightConveyorsWorld(GameState game, int size)
        {
            for (int i = 0; i < size; i++)
            {
                BeltSegment belt = new()
                {
                    vertices = new List<VecInt2>()
                    {
                        new(0, i),
                        new(size, i)
                    }
                };

                belt.UpdateLengths(game.mainLattice);
                for (int j = (int)(size / GameRules.minItemDistance) - 1; j >= 0; j--)
                {
                    belt.inventoryManager.inventory.AddToHead(new GameItem(i % 9), j * GameRules.minItemDistance);
                }

                belt.inventoryManager.RecieverTile = new(0, i);
                belt.inventoryManager.depositInventory = belt.inventoryManager;

                game.mainChunk.beltSegments.Add(belt);
            }
        }
    }
}