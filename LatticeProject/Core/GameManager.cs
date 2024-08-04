using LatticeProject.Game;
using LatticeProject.Lattices;
using LatticeProject.Rendering;
using LatticeProject.Utility;
using Raylib_cs;
using System.Numerics;

namespace LatticeProject.Core
{
    internal static class GameManager
    {
        static Lattice mainLattice = new HexagonLattice();
        static WorldChunk mainChunk = new WorldChunk();
        static LatticeCamera mainCam = new LatticeCamera(Vector2.Zero, 1, 0, new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2));
        
        static Vector2 mousePosition = new Vector2();
        static VecInt2 lastClosestVertex = VecInt2.Zero;
        static VecInt2 closestVertex = VecInt2.Zero;
        static VecInt2[] linePoints = new VecInt2[0];
        public static int nextColor = 0;

        static float simulationSpeed = 3;

        public static void Begin()
        {
            Raylib.InitWindow(1600, 900, "Hello World");
            Raylib.SetTargetFPS(60);
            RenderConfig.scale = 150;
        }

        public static void Update()
        {
            lastClosestVertex = closestVertex;

            mousePosition = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), mainCam.camera);
            closestVertex = mainLattice.GetClosestVertex(mousePosition / RenderConfig.scale);

            //linePoints = mainLattice.GetLinePoints(VecInt2.Zero, closestVertex);

            if (Raylib.IsMouseButtonPressed(0))
            {
                mainChunk.beltSegments.Add(new BeltSegment());
                mainChunk.beltSegments[^1].vertices.Add(lastClosestVertex);
            }

            if (closestVertex != lastClosestVertex && Raylib.IsMouseButtonDown(0))
            {
                mainChunk.beltSegments[^1].vertices.Add(closestVertex);
            }

            if (Raylib.IsMouseButtonReleased(0))
            {
                mainChunk.beltSegments[^1].SimplifyVertices(mainLattice);
                mainChunk.beltSegments[^1].UpdateLengths(mainLattice);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Left)) mainCam.camera.Rotation -= 30;
            if (Raylib.IsKeyPressed(KeyboardKey.Right)) mainCam.camera.Rotation += 30;

            if (Raylib.IsKeyDown(KeyboardKey.LeftShift) && Raylib.GetMouseWheelMove() != 0)
            {
                simulationSpeed *= Raylib.GetMouseWheelMove() > 0 ? 1.2f : 1 / 1.2f;
            }

            mainChunk.Update(Raylib.GetFrameTime() * simulationSpeed);

            mainCam.UpdateCamera();
        }

        public static void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(13, 17, 23, 255));
            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            Raylib.BeginMode2D(mainCam.camera);

            LatticeRenderer.DrawHexagonalGrid(mainLattice, mainCam.Target, mainCam.Zoom);

            WorldChunkRenderer.DrawAllBeltSegments(mainLattice, mainChunk);

            LatticeRenderer.DrawVertex(mainLattice, closestVertex, 0.25f, Color.DarkGray);
            //BuildingRenderer.DrawBuilding(mainLattice, new Building() { Position = closestVertex }, Color.Gray);
            LatticeRenderer.DrawVertices(mainLattice, linePoints, 0.25f, Color.Blue);
            LatticeRenderer.HighlightNeighbours(mainLattice, closestVertex);

            Raylib.EndMode2D();

            Raylib.DrawFPS(10, 10);
            Raylib.DrawText("Simulation speed = " + simulationSpeed.ToString()[..Math.Min(simulationSpeed.ToString().Length, 5)] + "x", 10, 30, 20, Color.LightGray);
            if (mainChunk.beltSegments.Count > 0)
            {
                BeltInventory inv = mainChunk.beltSegments[^1].inventoryManager.inventory;
                Raylib.DrawText("Total belt length = " + inv.TotalBeltLength.ToString(), 10, 50, 20, Color.Purple);
                Raylib.DrawText("Item count = " + inv.Count.ToString(), 10, 70, 20, Color.Blue);
                Raylib.DrawText("Leading distance = " + inv.LeadingDistance.ToString(), 10, 90, 20, Color.Blue);
                Raylib.DrawText("Leading error = " + inv.CalculateLeadingDistanceError().ToString(), 10, 110, 20, Color.Maroon);
                Raylib.DrawText("Item to move = " + inv.ItemToMove?.ToString(), 10, 130, 20, Color.DarkGreen);
                int i = 150;
                foreach (BeltInventoryElement item in inv.items)
                {
                    Raylib.DrawText($"i={(i - 110) / 20}, {item}", 10, i, 20, Color.Lime);
                    i += 20;
                }
            }
            Raylib.EndDrawing();
        }

        public static void End()
        {
            Raylib.CloseWindow();
        }
    }
}