using System.Collections;

namespace LatticeProject.Game
{
    internal class BeltInventory : IEnumerable<BeltInventoryElement>
    {
        public readonly LinkedList<BeltInventoryElement> items = new();

        private int _count = 0;
        public int Count => _count;

        public float leadingDistance = 0; //distance from head to first item
        public float trailingDistance = 0; //distance from last item to tail
        public const float minItemDistance = 2 / 3f; //minimum distance items can be between themselves

        private int _totalBeltLength = 0;
        public int TotalBeltLength
        {
            get => _totalBeltLength;
            set 
            {
                trailingDistance += value - trailingDistance;
                _totalBeltLength = value;
            }
        }

        public bool CanRecieveItem() => leadingDistance >= 0;

        public void AddToHead(int itemId, float distanceFromHead)
        {
            _count++;

            if (items.First is null)
            {
                //if there are no items on the belt, creates a new rle element
                items.AddFirst(new BeltInventoryElement(itemId, 0, 1));
                leadingDistance = distanceFromHead;
                trailingDistance = TotalBeltLength - distanceFromHead;
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
            if (Count <= 0) return;

            //moves all items on the belt by increasing the distance from the head (start) of the belt to the first item
            leadingDistance += distance;
            trailingDistance -= distance;
        }

        public void RemoveLast()
        {
            if (items.Last is not null)
            {
                _count--;
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
            _count = 0;
            trailingDistance = TotalBeltLength;
        }

        public IEnumerator<BeltInventoryElement> GetEnumerator()
        {
            return new BeltInventoryEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
