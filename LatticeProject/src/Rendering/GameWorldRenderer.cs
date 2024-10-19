using LatticeProject.Game;

namespace LatticeProject.Rendering
{
    internal static class GameWorldRenderer
    {

        public static void Draw(GameState game)
        {
            LatticeRenderer.HighlightCell(game.mainLattice, game.closestVertex, GameColors.mouseCursorBackgroundColor);

            if (game.debugMode)
            {
                GameWorldDebugRenderer.DrawBeltHighlights(game, game.selection.belts);
            }

            LatticeRenderer.DrawLatticeGrid(game.mainLattice, game.mainCam.Target, game.mainCam.Zoom, GameColors.mainGridColor);

            WorldChunkRenderer.DrawAllBeltSegments(game.mainLattice, game.mainChunk);

            //WorldChunkRenderer.DrawAllBuildings(game.mainLattice, game.mainChunk);

            if (game.debugMode)
            {
                GameWorldDebugRenderer.DrawBeltDepositConnections(game.mainLattice, game, game.selection.belts);
                if (game.mainChunk.beltSegments.Count > 0) GameWorldDebugRenderer.DrawBeltBounds(game.mainLattice, game.selection.belts);
            }

            LatticeRenderer.DrawCellOutline(game.mainLattice, game.closestVertex, 4 / game.mainCam.Zoom, GameColors.mouseCursorOutlineColor);
        }
    }
}