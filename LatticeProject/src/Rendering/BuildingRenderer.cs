using LatticeProject.Game.Buildings;
using LatticeProject.Lattices;
using LatticeProject.Utility;
using Raylib_cs;
using System.Numerics;

namespace LatticeProject.Rendering
{
    internal static class BuildingRenderer
    {
        public static void DrawBuilding(Lattice lattice, Building building)
        {
            BuildingType type = BuildingTypes.buildingTypes[building.buildingType];

            foreach (VecInt2 v in type.tiles)
            {
                DrawBuildingPiece(lattice, building.center + v, RenderConfig.scale / LatticeMath.sqrt3, Color.DarkGray);
            }
        }

        public static void DrawBuildingPiece(Lattice lattice, VecInt2 tile, float size, Color col)
        {
            Vector2 pos = lattice.GetCartesianCoords(tile);
            Raylib.DrawPoly(pos * RenderConfig.scale, 6, size, 30, col);
        }
    }
}