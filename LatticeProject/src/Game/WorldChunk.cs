using LatticeProject.Game.Belts;
using LatticeProject.Game.Buildings;

namespace LatticeProject.Game
{
    internal class WorldChunk
    {
        public List<BeltSegment> beltSegments = new List<BeltSegment>();
        public List<Building> buildings = new List<Building>();

        public void Update(float deltaTime)
        {
            foreach (BeltSegment segment in beltSegments)
            {
                segment.inventoryManager.UpdateInventory(deltaTime);
            }
            foreach (BeltSegment segment in beltSegments)
            {
                segment.inventoryManager.PrepForNextUpdate(deltaTime);
            }
        }
    }
}