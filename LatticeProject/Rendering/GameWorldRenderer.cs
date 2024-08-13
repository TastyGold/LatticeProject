using LatticeProject.Game;
using Raylib_cs;

namespace LatticeProject.Rendering
{
    internal static class GameWorldRenderer
    {
        public static void Draw(GameState game)
        {
            LatticeRenderer.DrawHexagonalGrid(game.mainLattice, game.mainCam.Target, game.mainCam.Zoom);

            WorldChunkRenderer.DrawAllBeltSegments(game.mainLattice, game.mainChunk);

            LatticeRenderer.DrawVertex(game.mainLattice, game.closestVertex, 0.25f, Color.DarkGray);
            //BuildingRenderer.DrawBuilding(mainLattice, new Building() { Position = closestVertex }, Color.Gray);
            LatticeRenderer.DrawVertices(game.mainLattice, game.linePoints, 0.25f, Color.Blue);
            LatticeRenderer.HighlightNeighbours(game.mainLattice, game.closestVertex);
        }
    }
}