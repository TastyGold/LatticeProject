﻿using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace LatticeProject.Game.Belts
{
    internal class BeltInventoryEnumerator : IEnumerator<BeltInventoryElement>
    {
        private readonly LinkedListNode<BeltInventoryElement>? headNode;

        private int itemIndex = 0; //item index within an element
        private LinkedListNode<BeltInventoryElement>? currentElementNode; //current element from linkedlist items

        public BeltInventoryElement Current => currentElementNode is null ? new BeltInventoryElement(new GameItem(-1), 0, 0) : currentElementNode.Value;

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
            currentElementNode = headNode;
        }

        public BeltInventoryEnumerator(LinkedListNode<BeltInventoryElement>? first)
        {
            headNode = first;
            currentElementNode = first;
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
