using LatticeProject.Lattices;
using LatticeProject.Utility;
using Raylib_cs;
using System.Numerics;
using static LatticeProject.Utility.LatticeMath;
using static LatticeProject.Rendering.RenderConfig;

namespace LatticeProject.Rendering
{
    internal static class LatticeRenderer
    {
        public static void DrawVertex(Lattice lattice, VecInt2 vertex, float size, Color col)
        {
            Raylib.DrawCircleV(lattice.GetCartesianCoords(vertex) * scale, size * scale, col);
        }

        public static void DrawVertices(Lattice lattice, VecInt2[] points, float size, Color col)
        {
            for (int i = 0; i < points.Length; i++)
            {
                DrawVertex(lattice, points[i], size, col);
            }
        }

        public static void DrawVertices(Lattice lattice, int minX, int minY, int maxX, int maxY, float size, Color col)
        {
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    DrawVertex(lattice, new VecInt2(x, y), size, col);
                }
            }
        }

        public static void HighlightNeighbours(Lattice lattice, VecInt2 vertex)
        {
            VecInt2[] nOffsets = lattice.GetNeighbourOffsets();
            for (int i = 0; i < nOffsets.Length; i++)
            {
                DrawVertex(lattice, vertex + nOffsets[i], 0.1f, Color.Purple);
            }
        }

        public static void DrawGridPiece(Lattice lattice, float lineWidth, int x, int y)
        {
            Vector2 center = lattice.GetCartesianCoords(x, y);
            Vector2[] gridPieceOffsets = lattice.GetGridPieceOffsets();
            for (int i = 0; i < gridPieceOffsets.Length - 1; i++)
            {
                Raylib.DrawLineEx(
                    (center + gridPieceOffsets[i]) * scale,
                    (center + gridPieceOffsets[i + 1]) * scale,
                    lineWidth, new Color(33, 38, 45, 255)
                    );
            }
        }

        public static void DrawLatticeGrid(Lattice lattice, float lineWidth, int minX, int minY, int maxX, int maxY)
        {
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    DrawGridPiece(lattice, lineWidth, x, y);
                }
            }
        }

        public static void DrawLatticeGrid(Lattice lattice, Vector2 cameraPosition, float cameraZoom)
        {
            Vector2 halfScreenSize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()) / cameraZoom / 2;
            Vector2 topLeft = cameraPosition - halfScreenSize;
            Vector2 bottomRight = cameraPosition + halfScreenSize;

            VecInt2 min = lattice.GetClosestVertex(topLeft / scale);
            VecInt2 max = lattice.GetClosestVertex(bottomRight / scale);

            min.y--;
            max.y++;

            int height = max.y - min.y;
            min.x -= (int)(height * sqrt3_3);
            max.x += (int)(height * sqrt3_3);

            DrawLatticeGrid(lattice, 2 / cameraZoom, min.x, min.y, max.x, max.y);
        }

        public static void HighlightCell(Lattice lattice, VecInt2 vertex, Color col)
        {
            lattice.HighlightCell(vertex, scale, col);
        }

        public static void DrawCellOutline(Lattice lattice, VecInt2 vertex, float lineWidth, Color col)
        {
            Vector2 center = lattice.GetCartesianCoords(vertex);
            Vector2[] gridPieceOffsets = lattice.GetGridPieceOffsets();
            for (int i = 0; i < gridPieceOffsets.Length - 1; i++)
            {
                Raylib.DrawLineEx(
                    (center + gridPieceOffsets[i]) * scale,
                    (center + gridPieceOffsets[i + 1]) * scale,
                    lineWidth, col
                    ); 
                Raylib.DrawLineEx(
                    (center - gridPieceOffsets[i]) * scale,
                    (center - gridPieceOffsets[i + 1]) * scale,
                    lineWidth, col
                    );
            }
        }
    }
}