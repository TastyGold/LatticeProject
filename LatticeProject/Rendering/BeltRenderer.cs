using LatticeProject.Game.Belts;
using LatticeProject.Lattices;
using Raylib_cs;
using System.Numerics;

namespace LatticeProject.Rendering
{
    internal static class BeltRenderer
    {
        private static Color beltColor = new(33, 38, 45, 255);
        private static Color beltOutlineColor = new(48, 54, 61, 255);
        private const float beltOutlineWidth = 0.1f;
        private const float beltWidth = 0.5f;

        public static void DrawBeltSegment(Lattice lattice, BeltSegment segment)
        {
            DrawBeltOutline(lattice, segment);
            DrawBeltConveyor(lattice, segment);
        }

        public static void DrawBeltOutline(Lattice lattice, BeltSegment segment)
        {
            DrawBeltPieces(lattice, segment, (beltWidth + beltOutlineWidth) * RenderConfig.scale, beltOutlineColor);
        }

        public static void DrawBeltConveyor(Lattice lattice, BeltSegment segment)
        {
            DrawBeltPieces(lattice, segment, beltWidth * RenderConfig.scale, beltColor);
        }

        private static void DrawBeltPieces(Lattice lattice, BeltSegment segment, float width, Color col)
        {
            for (int i = 0; i < segment.vertices.Count - 1; i++)
            {
                Vector2 start = lattice.GetCartesianCoords(segment.vertices[i]);
                Vector2 end = lattice.GetCartesianCoords(segment.vertices[i + 1]);

                Raylib.DrawLineEx(start * RenderConfig.scale, end * RenderConfig.scale, width, col);
                DrawBeltVertex(start, width / 2, col);

                if (i == segment.vertices.Count - 2)
                {
                    DrawBeltVertex(end, width / 2, col);
                }
            }
        }

        private static void DrawBeltVertex(Vector2 position, float width, Color col)
        {
            Raylib.DrawCircleV(position * RenderConfig.scale, width, col);
        }
    }
}