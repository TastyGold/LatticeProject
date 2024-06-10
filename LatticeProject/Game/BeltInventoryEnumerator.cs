using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace LatticeProject.Game
{
    internal class BeltInventoryEnumerator : IEnumerator<BeltInventoryElement>
    {
        public BeltInventory inventory;

        public int itemIndex = 0; //item index within an element
        public LinkedListNode<BeltInventoryElement>? currentElementNode; //current element from linkedlist items

        public BeltInventoryElement Current => currentElementNode is null ? new BeltInventoryElement(-1, 0, 0) : currentElementNode.Value;

        object? IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (currentElementNode is null) return false;
            if (itemIndex < 0)
            {
                itemIndex = 0;
                currentElementNode = currentElementNode.Next;
                if (currentElementNode is null) return false;
            }

            itemIndex++;
            if (itemIndex >= currentElementNode.Value.count)
            {
                itemIndex = -1;
            }

            return true;
        }

        public void Reset()
        {
            itemIndex = 0;
            currentElementNode = inventory.items.First;
        }

        public BeltInventoryEnumerator(BeltInventory inventory)
        {
            this.inventory = inventory;
            currentElementNode = inventory.items.First;
        }

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
