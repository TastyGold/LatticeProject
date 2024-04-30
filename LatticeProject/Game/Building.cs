using LatticeProject.Utility;

namespace LatticeProject.Game
{
    internal class Building
    {
        public BuildingType type = BuildingType.HexRouter;

        private VecInt2 _position;
        public VecInt2 Position
        {
            get
            { 
                return _position;
            }
            set
            {
                _position = value;
            }
        }
    }
}