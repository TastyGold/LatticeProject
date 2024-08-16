using LatticeProject.Game;
using LatticeProject.Game.Belts;
using LatticeProject.Lattices;
using LatticeProject.Utility;
using Raylib_cs;

namespace LatticeProject.Rendering
{
    internal static class GameWorldDebugRenderer
    {
        private static Color beltHighlightHeadColor = new(66, 11, 35, 255);
        private static Color beltHighlightMiddleColor = new(11, 35, 66, 255);
        private static Color beltHighlightTailColor = new(11, 66, 50, 255);

        private static Color beltConnectionColor = Color.Green;

        public static void DrawBeltHighlights(GameState game)
        {
            foreach (BeltSegment belt in game.selection.belts)
            {
                foreach (VecInt2 v in belt)
                {
                    LatticeRenderer.HighlightCell(game.mainLattice, v, beltHighlightMiddleColor);
                }
                if (belt.vertices.Count > 1)
                {
                    LatticeRenderer.HighlightCell(game.mainLattice, belt.vertices[0], beltHighlightHeadColor); //head
                    LatticeRenderer.HighlightCell(game.mainLattice, belt.vertices[^1], beltHighlightTailColor); //tail
                }
            }
        }

        public static void DrawBeltDepositConnections(Lattice lattice, GameState game)
        {
            foreach (BeltSegment belt in game.selection.belts)
            {
                if (belt.vertices.Count > 1 && belt.inventoryManager.depositInventory is not null)
                {
                    Raylib.DrawLineV(
                        lattice.GetCartesianCoords(belt.vertices[^1]) * RenderConfig.scale,
                        lattice.GetCartesianCoords(belt.inventoryManager.depositInventory.RecieverTile) * RenderConfig.scale,
                        beltConnectionColor
                        );
                }
            }
        }
    }
}