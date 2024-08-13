using LatticeProject.Lattices;
using LatticeProject.Rendering;
using LatticeProject.Utility;
using Raylib_cs;
using System.Numerics;

namespace LatticeProject.Game
{
    internal class GameState
    {
        public Lattice mainLattice;
        public WorldChunk mainChunk = new WorldChunk();
        public LatticeCamera mainCam = new LatticeCamera(Vector2.Zero, 150f/RenderConfig.scale, 0, new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2));

        public Vector2 mousePosition = new Vector2();
        public VecInt2 lastClosestVertex = VecInt2.Zero;
        public VecInt2 closestVertex = VecInt2.Zero;

        public float simulationSpeed = 3;
        public bool frameAdvance = false;

        public GameState(Lattice lattice)
        {
            mainLattice = lattice;
        }
    }
}