using System.Collections;
using System.Collections.Generic;

namespace LatticeProject.Game
{
    internal class NewBeltInventory
    {
        public readonly LinkedList<BeltInventoryElement> items = new();

        private int _count = 0;
        public int Count => _count;

        public float leadingDistance = 0; //distance from head to first item
        public float trailingDistance = 0; //distance from last item to tail
        public const float minItemDistance = 2 / 3f; //minimum distance items can be between themselves

        public void AddToHead(int itemId, float distanceFromHead)
        {
            if (items.First is null) items.AddFirst(new BeltInventoryElement(itemId, 1, 0));
            else
            {
                float newItemPosition = distanceFromHead; //the position on the belt the new item will be set to
                float newFirstDistance = leadingDistance - distanceFromHead; // the distance between the new item and old first item

                float maxPosition = leadingDistance - minItemDistance;
                if (distanceFromHead > maxPosition)
                {
                    //if the new item is too close to the current first item on the belt, it is shifted back to be minItemDistance away
                    newItemPosition = maxPosition;
                    newFirstDistance = minItemDistance;
                }

                float newFirstDistance = leadingDistance - newItemPosition;

                if (items.First.Value.ItemDistanceMatches(itemId, newFirstDistance))
                {
                    items.First.Value.count++;
                }
                else
                {
                    items.AddFirst(new BeltInventoryElement(itemId, 1, newFirstDistance));
                }

                leadingDistance = newItemPosition;
            }
        }
    }

    internal class BeltInventoryElement
    {
        public int itemId;
        public int count;
        public float distance;

        public bool ItemDistanceMatches(int itemId, float distance)
        {
            return this.itemId == itemId
                && this.distance == distance;
        }

        public BeltInventoryElement(int itemId, int count, float distance)
        {
            this.itemId = itemId;
            this.count = count;
            this.distance = distance;
        }
    }
}
