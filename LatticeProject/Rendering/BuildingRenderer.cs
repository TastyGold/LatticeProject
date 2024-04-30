using LatticeProject.Game;
using LatticeProject.Lattices;
using Raylib_cs;
using System.Numerics;
using static LatticeProject.Rendering.RenderConfig;

namespace LatticeProject.Rendering
{
    internal static class BuildingRenderer
    {
        public static void DrawBuilding(Lattice lattice, Building building, Color col)
        {
            Raylib.DrawPoly(lattice.GetCartesianCoords(building.Position) * scale, 6, scale / 2, 30, col);
        }
    }
}