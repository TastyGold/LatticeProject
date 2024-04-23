using Raylib_cs;
using System.Numerics;

namespace LatticeProject
{
    internal static class GameManager
    {
        //static Lattice mainLattice = new SquareLattice();
        static Lattice mainLattice = new HexagonLattice();
        static LatticeChunk mainChunk = new LatticeChunk();
        static LatticeCamera mainCam = new LatticeCamera(Vector2.Zero, 1, 0, new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2));
        static Vector2 mousePosition = new Vector2();
        static VecInt2 lastClosestVertex = VecInt2.Zero;
        static VecInt2 closestVertex = VecInt2.Zero;

        public static void Begin()
        {
            Raylib.InitWindow(1600, 900, "Hello World");
            LatticeRenderer.scale = 150;
            mainChunk.beltSegments.Add(new BeltSegment());
        }

        public static void Update()
        {
            lastClosestVertex = closestVertex;

            mousePosition = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), mainCam.camera);
            closestVertex = mainLattice.GetClosestVertex(mousePosition / LatticeRenderer.scale);

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
            }

            mainCam.UpdateCamera();

            Console.WriteLine(closestVertex.ToString());

            if (Raylib.IsKeyPressed(KeyboardKey.P))
            {
                Console.WriteLine("Hi!");
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Left)) mainCam.camera.Rotation -= 30;
            if (Raylib.IsKeyPressed(KeyboardKey.Right)) mainCam.camera.Rotation += 30;
        }

        public static bool IsValidDirectionChange(VecInt2 lastDirection, VecInt2 currentDirection)
        {
            int last = -1;
            int current = -1;
            VecInt2[] nOffsets = mainLattice.GetNeighbourOffsets();

            int i = 0;
            while (i < nOffsets.Length && (last == -1 || current == -1))
            {
                if (lastDirection == nOffsets[i]) last = i;
                if (currentDirection == nOffsets[i]) current = i;
                i++;
            }

            if (last == current || last == -1 || current == -1) return true;

            int difference = Math.Abs(last - current);
            return difference == 1 || difference == nOffsets.Length - 1;
        }

        public static void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(13, 17, 23, 255));
            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            Raylib.BeginMode2D(mainCam.camera);

            //Raylib.DrawLineV(mainLattice.GetCartesianCoords(closestVertex.x, closestVertex.y) * LatticeRenderer.scale, mousePosition, Color.Blue);
            //LatticeRenderer.DrawVertices(mainLattice, 0, 0, 15, 15);
            LatticeRenderer.DrawHexagonalGrid(mainLattice, 2 / mainCam.Zoom, 0, 0, 15, 15);
            //BeltRenderer.DrawBeltSegments(mainLattice, objManager);
            LatticeChunkRenderer.DrawAllBeltSegments(mainLattice, mainChunk);
            Raylib.DrawCircleV(mainLattice.GetCartesianCoords(closestVertex.x, closestVertex.y) * LatticeRenderer.scale, LatticeRenderer.scale / 4, Color.DarkGray);
            LatticeRenderer.HighlightNeighbours(mainLattice, closestVertex);

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