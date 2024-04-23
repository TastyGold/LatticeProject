using Raylib_cs;
using System.Numerics;
using static LatticeProject.LatticeRenderer;

namespace LatticeProject
{
    internal class BeltSegment
    {
        public List<VecInt2> vertices = new List<VecInt2>();

        public void SimplifyVertices()
        {
            int i = 2;
            while (i < vertices.Count)
            {
                if (GetDirection(vertices[i - 1], vertices[i]) == GetDirection(vertices[i - 2], vertices[i - 1]))
                {
                    vertices.RemoveAt(i - 1);
                }
                else
                {
                    i++;
                }
            }
        }

        public VecInt2 GetDirection(VecInt2 start, VecInt2 end)
        {
            VecInt2 dv = end - start;
            if (dv.x == 0 && dv.y == 0) return VecInt2.Zero;
            if (dv.x == 0) return new VecInt2(0, dv.y / dv.y);
            if (dv.y == 0) return new VecInt2(dv.x / dv.x, 0);
            if (dv.x == -dv.y) return new VecInt2(dv.x / dv.x, dv.y / dv.y);
            else return dv;
        }
    }

    internal class LatticeChunk
    {
        public List<BeltSegment> beltSegments = new List<BeltSegment>();
    }

    internal static class LatticeChunkRenderer
    {
        public static void DrawAllBeltSegments(Lattice lattice, LatticeChunk chunk)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < chunk.beltSegments.Count; i++)
                {
                    BeltRenderer.DrawBeltSegment(lattice, chunk.beltSegments[i], j == 0, i);
                }
            }
        }
    }

    internal static class BeltRenderer
    {
        private static Color beltColor = new Color(33, 38, 45, 255);
        private static Color beltOutlineColor = new Color(48, 54, 61, 255);
        public static float beltOutlineWidth = 0.1f;
        public static float beltWidth = 0.5f;

        public static void DrawBeltSegment(Lattice lattice, BeltSegment segment, bool outline, int colorIndex)
        {
            for (int i = 0; i < segment.vertices.Count - 1; i++)
            {
                Vector2 start = lattice.GetCartesianCoords(segment.vertices[i]);
                Vector2 end = lattice.GetCartesianCoords(segment.vertices[i + 1]);

                float width = !outline ? scale * beltWidth : scale * (beltWidth + beltOutlineWidth);
                Color col = outline ? beltOutlineColor : Colors.colors[i % Colors.numColors];

                Raylib.DrawLineEx(start * scale, end * scale, width, col);
                Raylib.DrawCircleV(start * scale, width / 2, col);

                if (i == segment.vertices.Count - 2)
                {
                    Raylib.DrawCircleV(end * scale, width / 2, col);
                }
            }
        }
    }

    internal static class Colors
    {
        public const int numColors = 21;
        public static Color[] colors =
        {
            Color.DarkGray, Color.Maroon, Color.Orange, Color.DarkGreen, Color.DarkBlue, Color.DarkPurple, Color.DarkBrown,
            Color.Gray, Color.Red, Color.Gold, Color.Lime, Color.Blue, Color.Violet, Color.Brown,
            Color.LightGray, Color.Pink, Color.Yellow, Color.Green, Color.SkyBlue, Color.Purple, Color.Beige
        };
    }
}