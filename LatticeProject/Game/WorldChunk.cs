namespace LatticeProject.Game
{
    internal class WorldChunk
    {
        public List<BeltSegment> beltSegments = new List<BeltSegment>();

        public void Update(float deltaTime)
        {
            foreach (BeltSegment segment in beltSegments)
            {
                segment.inventoryManager.PrepareUpdate(deltaTime);
            }
            foreach (BeltSegment segment in beltSegments)
            {
                segment.inventoryManager.UpdateInventory(deltaTime);
            }
        }
    }
}