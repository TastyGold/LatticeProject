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
        //static Lattice mainLattice = new SquareLattice();
        static Lattice mainLattice = new HexagonLattice();
        static WorldChunk mainChunk = new WorldChunk();
        static LatticeCamera mainCam = new LatticeCamera(Vector2.Zero, 1, 0, new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2));
        static Vector2 mousePosition = new Vector2();
        static VecInt2 lastClosestVertex = VecInt2.Zero;
        static VecInt2 closestVertex = VecInt2.Zero;
        static VecInt2[] linePoints = new VecInt2[0];
        static BeltInventory lastBeltInv = new BeltInventory();

        public static void Begin()
        {
            Raylib.InitWindow(1600, 900, "Hello World");
            RenderConfig.scale = 150;
            mainChunk.beltSegments.Add(new BeltSegment());
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
                mainChunk.beltSegments[^1].SimplifyVertices();
                mainChunk.beltSegments[^1].UpdateLengths(mainLattice);
                for (int i = 0; i < 10; i++)
                {
                    mainChunk.beltSegments[^1].inventory.AddItem(0.66f);
                }
                lastBeltInv = mainChunk.beltSegments[^1].inventory;
            }

            if (Raylib.IsKeyDown(KeyboardKey.J)) lastBeltInv.interItemDistances[0] -= Raylib.GetFrameTime() * 4;
            if (Raylib.IsKeyDown(KeyboardKey.K)) lastBeltInv.interItemDistances[0] += Raylib.GetFrameTime() * 4;

            mainCam.UpdateCamera();

            if (Raylib.IsKeyPressed(KeyboardKey.Left)) mainCam.camera.Rotation -= 30;
            if (Raylib.IsKeyPressed(KeyboardKey.Right)) mainCam.camera.Rotation += 30;
        }

        public static void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(13, 17, 23, 255));
            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            Raylib.BeginMode2D(mainCam.camera);

            //LatticeRenderer.DrawHexagonalGrid(mainLattice, 2 / mainCam.Zoom, -16, -16, 15, 15);
            LatticeRenderer.DrawHexagonalGrid(mainLattice, mainCam.Target, mainCam.Zoom);

            WorldChunkRenderer.DrawAllBeltSegments(mainLattice, mainChunk);

            //LatticeRenderer.DrawVertex(mainLattice, closestVertex, 0.25f, Color.DarkGray);
            //BuildingRenderer.DrawBuilding(mainLattice, new Building() { Position = closestVertex }, Color.Gray);
            LatticeRenderer.DrawVertices(mainLattice, linePoints, 0.25f, Color.Blue);
            //LatticeRenderer.HighlightNeighbours(mainLattice, closestVertex);

            Raylib.EndMode2D();

            Raylib.DrawFPS(10, 10);
            Raylib.EndDrawing();
        }

        public static void End()
        {
            Raylib.CloseWindow();
        }
    }
}