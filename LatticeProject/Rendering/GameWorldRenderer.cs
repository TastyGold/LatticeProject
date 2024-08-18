using LatticeProject.Game;
using Raylib_cs;

namespace LatticeProject.Rendering
{
    internal static class GameWorldRenderer
    {
        private static Color mouseCursorBackgroundColor = new(11, 25, 44, 255);
        private static Color mouseCursorOutlineColor = new(57, 76, 102, 255);

        public static void Draw(GameState game)
        {
            LatticeRenderer.HighlightCell(game.mainLattice, game.closestVertex, mouseCursorBackgroundColor);

            if (game.debugMode) GameWorldDebugRenderer.DrawBeltHighlights(game, game.mainChunk.beltSegments);

            LatticeRenderer.DrawLatticeGrid(game.mainLattice, game.mainCam.Target, game.mainCam.Zoom);

            WorldChunkRenderer.DrawAllBeltSegments(game.mainLattice, game.mainChunk);

            if (game.debugMode)
            {
                GameWorldDebugRenderer.DrawBeltDepositConnections(game.mainLattice, game, game.mainChunk.beltSegments);
                if (game.mainChunk.beltSegments.Count > 0) GameWorldDebugRenderer.DrawBeltBounds(game.mainLattice, game.mainChunk.beltSegments[^1]);
            }

            LatticeRenderer.DrawCellOutline(game.mainLattice, game.closestVertex, 4 / game.mainCam.Zoom, mouseCursorOutlineColor);
        }
    }
}