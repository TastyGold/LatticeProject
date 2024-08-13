using LatticeProject.Game.Belts;
using LatticeProject.Rendering;
using LatticeProject.Utility;
using Raylib_cs;

namespace LatticeProject.Game
{
    internal static class GameManager
    {
        static readonly GameState game = new GameState();

        public static void Run()
        {
            Begin();

            while (!Raylib.WindowShouldClose())
            {
                Update();
                Draw();
            }

            End();
        }

        private static void Begin()
        {
            Raylib.InitWindow(1600, 900, "Hello World");
            Raylib.SetTargetFPS(120);
            RenderConfig.scale = 150;
        }

        private static void Update()
        {
            game.lastClosestVertex = game.closestVertex;

            game.mousePosition = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), game.mainCam.camera);
            game.closestVertex = game.mainLattice.GetClosestVertex(game.mousePosition / RenderConfig.scale);

            //linePoints = mainLattice.GetLinePoints(VecInt2.Zero, closestVertex);

            if (Raylib.IsKeyPressed(KeyboardKey.Left)) game.mainCam.camera.Rotation -= 30;
            if (Raylib.IsKeyPressed(KeyboardKey.Right)) game.mainCam.camera.Rotation += 30;

            if (Raylib.IsKeyDown(KeyboardKey.LeftShift) && Raylib.GetMouseWheelMove() != 0)
            {
                game.simulationSpeed *= Raylib.GetMouseWheelMove() > 0 ? 1.2f : 1 / 1.2f;
            }

            if (Raylib.IsKeyPressed((KeyboardKey)91)) game.frameAdvance = !game.frameAdvance;

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
            if (Raylib.IsKeyPressed(KeyboardKey.T)) game.terrainMode = !game.terrainMode;

            if (game.terrainMode)
            {
                if (game.closestVertex.x > 0 && game.closestVertex.x < 255 && game.closestVertex.y > 0 && game.closestVertex.y < 255)
                {
                    if (Raylib.IsMouseButtonDown(MouseButton.Left))
                    {
                        for (int i = 0; i < LatticeMath.hexNeighbours.Length; i++)
                        {
                            game.terrainChunk.SetTile(game.closestVertex.x + LatticeMath.hexNeighbours[i].x, game.closestVertex.y + LatticeMath.hexNeighbours[i].y, true);
                        }
                    }
                    if (Raylib.IsMouseButtonDown(MouseButton.Right))
                    {
                        for (int i = 0; i < LatticeMath.hexNeighbours.Length; i++)
                        {
                            game.terrainChunk.SetTile(game.closestVertex.x + LatticeMath.hexNeighbours[i].x, game.closestVertex.y + LatticeMath.hexNeighbours[i].y, false);
                        }
                    }
                }
            }
            else
            {
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
            }

            game.mainCam.UpdateCamera();
        }

        private static void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(13, 17, 23, 255));

            Raylib.BeginMode2D(game.mainCam.camera);

            GameWorldRenderer.Draw(game);

            Raylib.EndMode2D();

            GameUIRenderer.Draw(game);

            Raylib.EndDrawing();
        }

        private static void End()
        {
            Raylib.CloseWindow();
        }
    }
}