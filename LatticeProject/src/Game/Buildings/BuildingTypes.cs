using LatticeProject.Utility;

namespace LatticeProject.Game.Buildings
{
    internal static class BuildingTypes
    {
        public static readonly List<BuildingType> buildingTypes = new();

        public static void LoadBuildingData()
        {
            buildingTypes.Add(new BuildingType(
                name: "bob",
                tiles: new List<VecInt2>()
                {
                    new(0, 0),
                    new(0, 1),
                    new(1, -1),
                    new(-1, 0),
                }
                )); 
            buildingTypes.Add(new BuildingType(
                name: "phil",
                tiles: new List<VecInt2>()
                {
                    new(0, 0),
                    new(0, 1),
                    new(1, -1),
                    new(2, -2),
                }
                )); 
            buildingTypes.Add(new BuildingType(
                name: "james",
                tiles: new List<VecInt2>()
                {
                    new(0, 0),
                    new(2, -1),
                    new(1, -1),
                    new(1, 0),
                }
                ));
        }
    }
}