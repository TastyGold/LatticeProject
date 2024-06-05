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

        public float totalBeltLength = 0;

        public void AddToHead(int itemId, float distanceFromHead)
        {
            if (items.First is null)
            {
                //if there are no items on the belt, creates a new rle element
                items.AddFirst(new BeltInventoryElement(itemId, 0, 1));
                leadingDistance = distanceFromHead;
                trailingDistance = totalBeltLength - distanceFromHead;
            }
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

                if (items.First.Value.ItemDistanceMatches(itemId, newFirstDistance))
                {
                    //increases the quantity of the first RLE chain on the belt
                    items.First.Value.count++;
                }
                else
                {
                    //creates a new RLE and inserts it at the start of the belt
                    items.AddFirst(new BeltInventoryElement(itemId, newFirstDistance, 1));
                }

                leadingDistance = newItemPosition;
            }
        }

        public void MoveItems(float distance)
        {
            //moves all items on the belt by increasing the distance from the head (start) of the belt to the first item
            leadingDistance += distance;
            trailingDistance -= distance;
        }

        public void RemoveLast()
        {
            if (items.Last is not null)
            {
                if (items.Last.Value.count > 1)
                {
                    //if last rle chain has more than 1 element, decreases rle chain count
                    items.Last.Value.count--;
                    trailingDistance += items.Last.Value.distance;
                }
                else
                {
                    //else removes the last rle chain
                    items.RemoveLast();
                }
            }
            else throw new Exception("Cannot remove item from empty belt inventory.");
        }

        public void Clear()
        {
            items.Clear();
        }
    }

    internal class BeltInventoryElement
    {
        public int itemId;
        public float distance;
        public int count;

        public bool ItemDistanceMatches(int itemId, float distance)
        {
            return this.itemId == itemId
                && this.distance == distance;
        }

        public BeltInventoryElement(int itemId, float distance, int count)
        {
            this.itemId = itemId;
            this.distance = distance;
            this.count = count;
        }
    }
}
