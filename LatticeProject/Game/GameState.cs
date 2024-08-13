using LatticeProject.Core;
using LatticeProject.Lattices;
using LatticeProject.Utility;
using Raylib_cs;
using System.Numerics;

namespace LatticeProject.Game
{
    internal class GameState
    {
        public Lattice mainLattice = new HexagonLattice();
        public WorldChunk mainChunk = new WorldChunk();
        public WorldTerrainChunk terrainChunk = new WorldTerrainChunk();
        public LatticeCamera mainCam = new LatticeCamera(Vector2.Zero, 1, 0, new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2));

        public Vector2 mousePosition = new Vector2();
        public VecInt2 lastClosestVertex = VecInt2.Zero;
        public VecInt2 closestVertex = VecInt2.Zero;
        public VecInt2[] linePoints = new VecInt2[0];
        public int nextColor = 0;
        public bool terrainMode = false;

        public float simulationSpeed = 3;

        public bool frameAdvance = false;
    }
}