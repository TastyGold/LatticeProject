using LatticeProject.Game;
using Raylib_cs;

namespace LatticeProject.Rendering
{
    internal static class GameWorldRenderer
    {
        public static void Draw(GameState game)
        {
            LatticeRenderer.HighlightCell(game.mainLattice, game.closestVertex, new Color(11, 25, 44, 255));

            LatticeRenderer.DrawLatticeGrid(game.mainLattice, game.mainCam.Target, game.mainCam.Zoom);

            WorldChunkRenderer.DrawAllBeltSegments(game.mainLattice, game.mainChunk);

            LatticeRenderer.DrawCellOutline(game.mainLattice, game.closestVertex, 3 / game.mainCam.Zoom, new Color(57, 76, 102, 255));
        }
    }
}