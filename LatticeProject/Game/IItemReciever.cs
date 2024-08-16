using LatticeProject.Utility;

namespace LatticeProject.Game
{
    internal interface IItemReciever
    {
        public GameItem? RecievedItem { get; }
        public float RecievedItemOffset { get; }
        public VecInt2 RecieverTile { get; }

        /// <summary>
        /// The amount of free space from the head of the associated inventory.
        /// </summary>
        public float AvailableDistance { get; }

        /// <summary>
        /// Adds the RecievedItem to the associated inventory.
        /// </summary>
        /// <remarks>All inventories should run this method BEFORE updating their contents.</remarks>
        /// <returns>Wether or not RecievedItem was successfully accepted</returns>
        public bool TryAcceptRecievedItem();

        /// <summary>
        /// Attempts to assign a given item to the RecievedItem variable.
        /// </summary>
        /// <param name="item">The item being delivered to this reciever</param>
        /// <param name="offset">The offset in distance at which the recieved item should be accepted</param>
        /// <returns>Wether or not the provided item was assigned to the RecievedItem variable</returns>
        public bool TryRecieveItem(GameItem item, float offset); //during update
    }
}
