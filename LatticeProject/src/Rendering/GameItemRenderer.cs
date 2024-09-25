using LatticeProject.Game;
using LatticeProject.Utility;
using Raylib_cs;
using System.Numerics;

namespace LatticeProject.Rendering
{
    internal static class GameItemRenderer
    {
        private static Texture2D itemAtlas;
        private static readonly int itemResolution = 128;

        public static void Initialise()
        {
            itemAtlas = Raylib.LoadTexture("..//..//..//Assets/9-tone-items-dark-128x.png");
        }

        public static void DrawGameItem(GameItem item, Vector2 position, float size)
        {
            Raylib.DrawPoly(position, 6, size, 0, Colors.colors[item.color]);
        }

        public static void DrawGameItemTextured(GameItem item, Vector2 position, float size)
        {
            Rectangle srec = new Rectangle(item.color * itemResolution, itemResolution, itemResolution, itemResolution);
            Rectangle drec = new (position.X - size / 2, position.Y - size / 2, size, size);

            Raylib.DrawTexturePro(itemAtlas, srec, drec, Vector2.Zero, 0, Color.White);
        }
    }
}