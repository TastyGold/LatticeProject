using LatticeProject.Game.Belts;
using LatticeProject.Rendering;
using Raylib_cs;

namespace LatticeProject.Game
{
    internal static class GameUpdater
    {
        public static void Update(GameState game)
        {
            //mouse position
            game.lastClosestVertex = game.closestVertex;

            game.mousePosition = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), game.mainCam.camera);
            game.closestVertex = game.mainLattice.GetClosestVertex(game.mousePosition / RenderConfig.scale);

            //camera controls
            if (Raylib.IsKeyPressed(KeyboardKey.Left)) game.mainCam.camera.Rotation -= 30;
            if (Raylib.IsKeyPressed(KeyboardKey.Right)) game.mainCam.camera.Rotation += 30;

            //simulation speed
            if (Raylib.IsKeyDown(KeyboardKey.LeftShift) && Raylib.GetMouseWheelMove() != 0)
            {
                game.simulationSpeed *= Raylib.GetMouseWheelMove() > 0 ? 1.2f : 1 / 1.2f;
            }

            if (Raylib.IsKeyPressed((KeyboardKey)91)) game.frameAdvance = !game.frameAdvance;

            //belt placing
            if (Raylib.IsMouseButtonPressed(0))
            {
                game.mainChunk.beltSegments.Add(new BeltSegment());
                game.mainChunk.beltSegments[^1].vertices.Add(game.lastClosestVertex);
            }

            if (game.closestVertex != game.lastClosestVertex && Raylib.IsMouseButtonDown(0))
            {
                game.mainChunk.beltSegments[^1].vertices.Add(game.closestVertex);
            }

            if (Raylib.IsMouseButtonReleased(0))
            {
                game.mainChunk.beltSegments[^1].SimplifyVertices(game.mainLattice);
                game.mainChunk.beltSegments[^1].UpdateLengths(game.mainLattice);
            }

            //game advancing
            if (Raylib.IsKeyPressed(KeyboardKey.P))
            {
                game.mainChunk.Update(GameRules.minItemDistance * 10);
            }
            if (Raylib.IsKeyPressed((KeyboardKey)93))
            {
                game.mainChunk.Update(1 / 60f * game.simulationSpeed);
            }
            else if (!game.frameAdvance)
            {
                game.mainChunk.Update(Math.Min(1 / 60f, Raylib.GetFrameTime()) * game.simulationSpeed);
            }

            //camera updating
            game.mainCam.UpdateCamera();
        }
    }
}