using LatticeProject.Game;
using LatticeProject.Lattices;
using LatticeProject.Utility;
using Raylib_cs;
using System.Numerics;

namespace LatticeProject.Rendering
{
    internal static class WorldTerrainRenderer
    {
        public static Color terrainColor = new Color(23, 28, 37, 255);
        public static Color terrainOutlineColor = new Color(7, 10, 14, 255);

        public static void DrawTerrainChunk(Lattice lattice, WorldTerrainChunk chunk)
        {
            for (int y = 0; y < WorldTerrainChunk.terrainChunkSize; y++)
            {
                for (int x = 0; x < WorldTerrainChunk.terrainChunkSize; x++)
                {
                    if (chunk.GetTile(x, y))
                    {
                        Vector2 center = lattice.GetCartesianCoords(x, y);

                        Raylib.DrawPoly(center * RenderConfig.scale, 6, RenderConfig.scale / LatticeMath.sqrt3, 30, terrainColor);
                    }
                }
            }
        }

        public static void DrawTerrainOutline(Lattice lattice, WorldTerrainChunk chunk)
        {
            for (int y = 0; y < WorldTerrainChunk.terrainChunkSize; y++)
            {
                for (int x = 0; x < WorldTerrainChunk.terrainChunkSize; x++)
                {
                    if (chunk.GetTile(x, y))
                    {
                        Vector2 center = lattice.GetCartesianCoords(x, y);

                        Raylib.DrawPoly(center * RenderConfig.scale, 6, RenderConfig.scale / (LatticeMath.sqrt3 - 0.2f), 30, terrainOutlineColor);
                    }
                }
            }
        }
    }
}