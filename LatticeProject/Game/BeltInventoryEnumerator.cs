﻿using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace LatticeProject.Game
{
    internal class BeltInventoryEnumerator : IEnumerator<BeltInventoryElement>
    {
        public BeltInventoryNew inventory;

        public int itemIndex = 0; //item index within an element
        public LinkedListNode<BeltInventoryElement>? currentElementNode; //current element from linkedlist items

        public BeltInventoryElement Current => currentElementNode is null ? new BeltInventoryElement(-1, 0, 0) : currentElementNode.Value;

        object? IEnumerator.Current => Current;

        public bool MoveNext()
        {
            itemIndex++;
            if (currentElementNode is null) return false;
            else if (itemIndex >= currentElementNode.Value.count)
            {
                itemIndex = 0;
                currentElementNode = currentElementNode.Next;
            }

            return true;
        }

        public void Reset()
        {
            itemIndex = 0;
            currentElementNode = inventory.items.First;
        }

        public BeltInventoryEnumerator(BeltInventoryNew inventory)
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
