using LatticeProject.Game;
using LatticeProject.Game.Belts;
using LatticeProject.Utility;
using Raylib_cs;

namespace LatticeProject.Rendering
{
    internal static class GameWorldRenderer
    {
        public static void Draw(GameState game)
        {
            LatticeRenderer.HighlightCell(game.mainLattice, game.closestVertex, new Color(11, 25, 44, 255));

            foreach (BeltSegment belt in game.selection.belts)
            {
                foreach (VecInt2 v in belt)
                {
                    LatticeRenderer.HighlightCell(game.mainLattice, v, new Color(11, 35, 66, 255));
                }
                if (belt.vertices.Count > 1)
                {
                    LatticeRenderer.HighlightCell(game.mainLattice, belt.vertices[0], new Color(66, 11, 35, 255)); //head
                    LatticeRenderer.HighlightCell(game.mainLattice, belt.vertices[^1], new Color(11, 66, 50, 255)); //tail
                }
            }

            LatticeRenderer.DrawLatticeGrid(game.mainLattice, game.mainCam.Target, game.mainCam.Zoom);

            WorldChunkRenderer.DrawAllBeltSegments(game.mainLattice, game.mainChunk);


            LatticeRenderer.DrawCellOutline(game.mainLattice, game.closestVertex, 4 / game.mainCam.Zoom, new Color(57, 76, 102, 255));
        }
    }
}