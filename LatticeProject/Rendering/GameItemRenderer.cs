using LatticeProject.Game;
using Raylib_cs;
using System.Numerics;

namespace LatticeProject.Rendering
{
    internal static class GameItemRenderer
    {
        public static void DrawGameItem(GameItem item, Vector2 position, float size)
        {
            Raylib.DrawPoly(position, 6, size, 0, Color.Red);
        }
    }
}