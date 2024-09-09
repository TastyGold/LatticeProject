using LatticeProject.Game;
using LatticeProject.Lattices;
using Raylib_cs;

namespace LatticeProject.Rendering
{
    internal static class BuildingRenderer
    {
        public static void DrawBuilding(Lattice lattice, Building building, Color col)
        {
            Raylib.DrawPoly(lattice.GetCartesianCoords(building.Position) * RenderConfig.scale, 6, RenderConfig.scale / 2, 30, col);
        }
    }
}