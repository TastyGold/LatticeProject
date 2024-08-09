using LatticeProject.Game.Belts;
using LatticeProject.Lattices;
using Raylib_cs;
using System.Numerics;
using static LatticeProject.Rendering.RenderConfig;

namespace LatticeProject.Rendering
{
    internal static class BeltRenderer
    {
        private static Color beltColor = new Color(33, 38, 45, 255);
        private static Color beltOutlineColor = new Color(48, 54, 61, 255);
        public static float beltOutlineWidth = 0.1f;
        public static float beltWidth = 0.4f;

        public static void DrawBeltSegment(Lattice lattice, BeltSegment segment)
        {
            //outline
            DrawBeltPieces(lattice, segment, (beltWidth + beltOutlineWidth) * scale, beltOutlineColor);

            //belt
            DrawBeltPieces(lattice, segment, beltWidth * scale, beltColor);
        }

        public static void DrawBeltPieces(Lattice lattice, BeltSegment segment, float width, Color col)
        {
            for (int i = 0; i < segment.vertices.Count - 1; i++)
            {
                Vector2 start = lattice.GetCartesianCoords(segment.vertices[i]);
                Vector2 end = lattice.GetCartesianCoords(segment.vertices[i + 1]);

                Raylib.DrawLineEx(start * scale, end * scale, width, col);
                Raylib.DrawCircleV(start * scale, width / 2, col);

                if (i == segment.vertices.Count - 2)
                {
                    Raylib.DrawCircleV(end * scale, width / 2, col);
                }
            }
        }
    }
}