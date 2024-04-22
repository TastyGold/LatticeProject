using Raylib_cs;
using System.Numerics;

namespace LatticeProject
{
    internal static class GameManager
    {
        static Lattice mainLattice = new HexagonLattice();
        static Camera2D mainCam = new Camera2D()
        {
            Target = Vector2.Zero,
            Zoom = 1,
            Rotation = 0,
            Offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2),
        };
        static Vector2 mousePosition = new Vector2();
        static VecInt2 closestVertex = VecInt2.Zero;
        static float zoom = 150;

        public static void Begin()
        {
            Raylib.InitWindow(1600, 900, "Hello World");
            LatticeRenderer.scale = 150;
        }

        public static void Update()
        {
            mousePosition = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), mainCam);
            closestVertex = mainLattice.GetClosestVertex(mousePosition / LatticeRenderer.scale);

            Vector2 movementInput = Vector2.Zero;
            if (Raylib.IsKeyDown(KeyboardKey.A)) movementInput.X--;
            if (Raylib.IsKeyDown(KeyboardKey.D)) movementInput.X++;
            if (Raylib.IsKeyDown(KeyboardKey.W)) movementInput.Y--;
            if (Raylib.IsKeyDown(KeyboardKey.S)) movementInput.Y++;

            mainCam.Target += movementInput * Raylib.GetFrameTime() * 500f;
            Console.WriteLine(closestVertex.ToString());

            if (Raylib.GetMouseWheelMove() > 0) zoom *= 1.1f;
            if (Raylib.GetMouseWheelMove() < 0) zoom /= 1.1f;

            LatticeRenderer.scale = zoom;
        }

        public static void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(13, 17, 23, 255));
            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            Raylib.BeginMode2D(mainCam);

            //Raylib.DrawLineV(mainLattice.GetCartesianCoords(closestVertex.x, closestVertex.y) * LatticeRenderer.scale, mousePosition, Color.Blue);
            //LatticeRenderer.DrawVertices(mainLattice, -10, -10, 10, 10);
            LatticeRenderer.DrawHexagonalGrid(mainLattice, -10, -10, 10, 10);
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