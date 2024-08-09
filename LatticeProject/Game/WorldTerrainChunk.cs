namespace LatticeProject.Game
{
    internal class WorldTerrainChunk
    {
        public const int terrainChunkSize = 256;

        private readonly bool[,] terrain = new bool[terrainChunkSize, terrainChunkSize];

        public void SetTile(int x, int y, bool state)
        {
            terrain[x, y] = state;
        }

        public bool GetTile(int x, int y)
        {
            return terrain[x, y];
        }
    }
}