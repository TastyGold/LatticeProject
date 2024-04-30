using LatticeProject.Utility;

namespace LatticeProject.Game
{
    internal class BuildingType
    {
        public readonly VecInt2[] neighbourVectors;

        public readonly VecInt2[] footprint;
        public readonly BuildingNeighbour[] inputs;
        public readonly BuildingNeighbour[] outputs;

        private static readonly BuildingNeighbour[] hexNeighbours = {
            new BuildingNeighbour(0, 0),
            new BuildingNeighbour(0, 1),
            new BuildingNeighbour(0, 2),
            new BuildingNeighbour(0, 3),
            new BuildingNeighbour(0, 4),
            new BuildingNeighbour(0, 5),
        };
        public static BuildingType HexRouter = new BuildingType(
            neighbours: LatticeMath.hexNeighbours,
            footprint: new VecInt2[] {
                new VecInt2(0, 0)
            },
            inputs: hexNeighbours,
            outputs: hexNeighbours
            );

        public BuildingType(VecInt2[] neighbours, VecInt2[] footprint, BuildingNeighbour[] inputs, BuildingNeighbour[] outputs)
        {
            neighbourVectors = neighbours;
            this.footprint = footprint;
            this.inputs = inputs;
            this.outputs = outputs;
        }
    }
}