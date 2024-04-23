using Raylib_cs;
using System.Numerics;

namespace LatticeProject
{
    internal static class GameManager
    {
        static Lattice mainLattice = new HexagonLattice() { horizontal = false };
        static LatticeObjectManager objManager = new LatticeObjectManager();
        static LatticeCamera mainCam = new LatticeCamera(Vector2.Zero, 1, 0, new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2), 500);
        static Camera2D _mainCam = new Camera2D()
        {
            Target = Vector2.Zero,
            Zoom = 1,
            Rotation = 0,
            Offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2),
        };
        static Vector2 mousePosition = new Vector2();
        static VecInt2 lastClosestVertex = VecInt2.Zero;
        static VecInt2 closestVertex = VecInt2.Zero;
        static VecInt2 lastDirection = VecInt2.Zero;
        static float zoom = 150;

        public static void Begin()
        {
            Raylib.InitWindow(1600, 900, "Hello World");
            LatticeRenderer.scale = 150;
        }

        public static void Update()
        {
            lastClosestVertex = closestVertex;

            mousePosition = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), mainCam.camera);
            closestVertex = mainLattice.GetClosestVertex(mousePosition / LatticeRenderer.scale);

            if (closestVertex != lastClosestVertex && Raylib.IsMouseButtonDown(0))
            {
                objManager.AddEdge(lastClosestVertex, closestVertex);
                lastDirection = lastClosestVertex - closestVertex;
            }

            mainCam.UpdateCamera();

            Vector2 movementInput = Vector2.Zero;
            if (Raylib.IsKeyDown(KeyboardKey.A)) movementInput.X--;
            if (Raylib.IsKeyDown(KeyboardKey.D)) movementInput.X++;
            if (Raylib.IsKeyDown(KeyboardKey.W)) movementInput.Y--;
            if (Raylib.IsKeyDown(KeyboardKey.S)) movementInput.Y++;

            Console.WriteLine(closestVertex.ToString());

            //if (Raylib.GetMouseWheelMove() > 0) zoom *= 1.1f;
            //if (Raylib.GetMouseWheelMove() < 0) zoom /= 1.1f;

            if (Raylib.IsKeyPressed(KeyboardKey.Left)) _mainCam.Rotation -= 30;
            if (Raylib.IsKeyPressed(KeyboardKey.Right)) _mainCam.Rotation += 30;

            LatticeRenderer.scale = zoom;
        }

        public static void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(13, 17, 23, 255));
            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            Raylib.BeginMode2D(mainCam.camera);

            //Raylib.DrawLineV(mainLattice.GetCartesianCoords(closestVertex.x, closestVertex.y) * LatticeRenderer.scale, mousePosition, Color.Blue);
            //LatticeRenderer.DrawVertices(mainLattice, -10, -10, 10, 10);
            LatticeRenderer.DrawHexagonalGrid(mainLattice, 2 / mainCam.Zoom, -8, -8, 7, 7);
            LatticeRenderer.DrawLatticeEdges(mainLattice, objManager);
            //Raylib.DrawCircleV(mousePosition, 4, Color.Purple);
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