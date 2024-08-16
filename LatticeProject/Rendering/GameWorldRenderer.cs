using LatticeProject.Game;
using LatticeProject.Utility;
using Raylib_cs;

namespace LatticeProject.Rendering
{
    internal static class GameWorldRenderer
    {
        private static Color[] highlightCols = { Color.SkyBlue, Color.Pink, Color.White };

        public static void Draw(GameState game)
        {
            LatticeRenderer.HighlightCell(game.mainLattice, game.closestVertex, new Color(11, 25, 44, 255));

            LatticeRenderer.DrawLatticeGrid(game.mainLattice, game.mainCam.Target, game.mainCam.Zoom);

            int i = 0;
            if (game.mainChunk.beltSegments.Count > 0 && game.mainChunk.beltSegments[^1].vertices.Count > 1 && Raylib.IsMouseButtonUp(MouseButton.Left)) foreach (VecInt2 v in game.mainChunk.beltSegments[^1])
                {
                    LatticeRenderer.HighlightCell(game.mainLattice, v, new Color(11, 25, 44, 255));
                    LatticeRenderer.DrawCellOutline(game.mainLattice, v, 4 / game.mainCam.Zoom, new Color(57, 76, 102, 255));
                    i++;
                }
            WorldChunkRenderer.DrawAllBeltSegments(game.mainLattice, game.mainChunk);


            LatticeRenderer.DrawCellOutline(game.mainLattice, game.closestVertex, 4 / game.mainCam.Zoom, new Color(57, 76, 102, 255));
        }
    }
}