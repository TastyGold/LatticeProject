using LatticeProject.Game;
using LatticeProject.Utility;
using Raylib_cs;

namespace LatticeProject.Rendering
{
    internal static class GameWorldRenderer
    {
        public static void Draw(GameState game)
        {
            LatticeRenderer.HighlightCell(game.mainLattice, game.closestVertex, new Color(11, 25, 44, 255));

            if (game.selectedBelt is not null) foreach (VecInt2 v in game.selectedBelt)
                {
                    LatticeRenderer.HighlightCell(game.mainLattice, v, new Color(11, 35, 66, 255));
                }

            LatticeRenderer.DrawLatticeGrid(game.mainLattice, game.mainCam.Target, game.mainCam.Zoom);

            WorldChunkRenderer.DrawAllBeltSegments(game.mainLattice, game.mainChunk);


            LatticeRenderer.DrawCellOutline(game.mainLattice, game.closestVertex, 4 / game.mainCam.Zoom, new Color(57, 76, 102, 255));
        }
    }
}