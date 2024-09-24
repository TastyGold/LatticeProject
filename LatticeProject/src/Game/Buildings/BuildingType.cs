using LatticeProject.Utility;

namespace LatticeProject.Game.Buildings
{
    internal class BuildingType
    {
        public string name;
        public readonly List<VecInt2> tiles;

        public BuildingType(string name, List<VecInt2> tiles)
        {
            this.name = name;
            this.tiles = tiles;
        }
    }
}