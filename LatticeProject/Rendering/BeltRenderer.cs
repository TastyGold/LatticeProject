using LatticeProject.Game;
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

        public static void DrawBeltSegment(Lattice lattice, BeltSegment segment, bool outline, int colorIndex)
        {
            for (int i = 0; i < segment.vertices.Count - 1; i++)
            {
                Vector2 start = lattice.GetCartesianCoords(segment.vertices[i]);
                Vector2 end = lattice.GetCartesianCoords(segment.vertices[i + 1]);

                float width = !outline ? scale * beltWidth : scale * (beltWidth + beltOutlineWidth);
                Color col = outline ? beltOutlineColor : beltColor; // Colors.colors[i % Colors.numColors];

                Raylib.DrawLineEx(start * scale, end * scale, width, col);
                Raylib.DrawCircleV(start * scale, width / 2, col);

                if (i == segment.vertices.Count - 2)
                {
                    Raylib.DrawCircleV(end * scale, width / 2, col);
                }
            }
        }

        public static void DrawBeltItems(Lattice lattice, BeltSegment segment)
        {
            if (segment.inventoryManager.inventory.items.First is null) return;

            BeltTraverser traverser = segment.GetTraverser();
            traverser.ResetEnd();
            bool firstItem = true;

            foreach (BeltInventoryElement item in segment.inventoryManager.inventory)
            {
                traverser.AdvanceReverse(item.distance);

                if (firstItem == true)
                {
                    //hacky method of aligning minItemDistance from tail to be exactly on tail
                    traverser.Advance(GameRules.minItemDistance);
                    firstItem = false;
                }

                Raylib.DrawCircleV(scale * segment.GetPositionAlongPiece(lattice, traverser.CurrentVertex, traverser.PositionAlongPiece), scale / 5, Colors.colors[item.itemId == -1 ? 0 : item.itemId % Colors.numColors]);
            }
        }
    }
}