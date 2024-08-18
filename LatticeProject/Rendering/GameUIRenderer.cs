using LatticeProject.Game;
using LatticeProject.Game.Belts;
using Raylib_cs;

namespace LatticeProject.Rendering
{
    internal static class GameUIRenderer
    {
        public static void Draw(GameState game)
        {
            Raylib.DrawFPS(10, 10);
            Raylib.DrawText("Simulation speed = " + game.simulationSpeed.ToString()[..Math.Min(game.simulationSpeed.ToString().Length, 5)], 10, 30, 20, Color.LightGray);
            if (game.mainChunk.beltSegments.Count > 0)
            {
                BeltInventory inv = game.mainChunk.beltSegments[^1].inventoryManager.inventory;
                Raylib.DrawText("Total belt length = " + inv.TotalBeltLength.ToString(), 10, 50, 20, Color.Purple);
                Raylib.DrawText("Leading distance = " + inv.LeadingDistance.ToString(), 10, 70, 20, Color.Blue);
                Raylib.DrawText("Leading error = " + inv.CalculateLeadingDistanceError().ToString(), 10, 90, 20, Color.Maroon);
                Raylib.DrawText("Available distance = " + game.mainChunk.beltSegments[^1].inventoryManager.AvailableDistance.ToString(), 10, 110, 20, Color.Red);
                Raylib.DrawText("Selected belt = " + game.selection.connectingBelt?.ToString(), 10, 130, 20, Color.Orange);
                DrawStrings(inv.GetInventoryDescription().Split('\n'), 10, 150, 20, Color.Lime);
            }
        }

        public static void DrawStrings(string[] strings, int x, int y, int fontSize, Color col)
        {
            for (int i = 0; i < strings.Length; i++)
            {
                Raylib.DrawText(strings[i], x, y + 20 * i, fontSize, col);
            }
        }
    }
}