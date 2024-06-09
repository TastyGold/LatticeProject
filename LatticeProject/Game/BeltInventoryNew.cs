namespace LatticeProject.Game
{
    internal class BeltInventoryNew
    {
        //list of RLE groups of items on the belt from TAIL to HEAD (same id and distance)
        public readonly LinkedList<BeltInventoryElement> items;

        //the item furthest along the belt that is not stationary (not yet bunched up at the end)
        public LinkedListNode<BeltInventoryElement>? itemToMove = null;

        //number of total items on the belt
        public int Count { get; private set; }

        //the distance from the head of the belt to the first item
        public float LeadingDistance { get; private set; }

        public const float minItemDistance = 2 / 3f;

        private int _totalBeltLength = 0;
        public int TotalBeltLength
        {
            get => _totalBeltLength;
            set
            {
                LeadingDistance += value - LeadingDistance;
                _totalBeltLength = value;
            }
        }

        /// <summary> Adds a new item to the head of the belt (exact position offset can be specified with distanceToHead).</summary>
        public void AddToHead(int itemId, float distanceFromHead)
        {
            if (items.Last is null) //list is empty
            {
                items.AddFirst(new BeltInventoryElement(itemId, TotalBeltLength - distanceFromHead, 1));
                itemToMove = items.First;

                LeadingDistance = distanceFromHead;
            }
            else
            {
                float newItemPosition = distanceFromHead; //the position on the belt the new item will be set to

                float maxPosition = LeadingDistance - minItemDistance;
                if (distanceFromHead > maxPosition)
                {
                    //if the new item is too close to the current item closest to the head, it is shifted back to be minItemDistance away
                    newItemPosition = maxPosition;
                }

                float newItemDistanceToNext = LeadingDistance - newItemPosition; //distance between the new item and old first item

                if (newItemDistanceToNext == items.Last.Value.distance)
                {
                    //increases the quantity of the RLE chain closest to the head of the belt
                    items.Last.Value.count++;
                }
                else
                {
                    //creates a new RLE at the head of the belt
                    items.AddLast(new BeltInventoryElement(itemId, newItemDistanceToNext, 1));
                }

                //update the leading distance after new item is added
                LeadingDistance = newItemPosition;
            }
        }

        /// <summary> Removes the item closest to the tail of the belt from the inventory.</summary>
        public void RemoveTailingItem()
        {
            if (items.First is not null)
            {
                if (items.First.Value.count > 1)
                {
                    items.First.Value.count--;
                    _ = SetFirstInElementDistance(items.First, items.First.Value.distance * 2);
                }
            }
        }

        /// <returns>A reference to the new or modified node that now has the inputted distance</returns>
        public LinkedListNode<BeltInventoryElement> SetFirstInElementDistance(LinkedListNode<BeltInventoryElement> node, float distance)
        {
            if (node.Value.distance == distance) return node; //no change is made

            if (node.Value.count == 1) //change is made to element of count 1
            {
                node.Value.distance = distance;
                return node;
            }
            else
            {
                //separates the trailing item from a multi-item element and gives it the specified distance

                LinkedListNode<BeltInventoryElement> previous = items.AddBefore(
                    node, new BeltInventoryElement(node.Value.itemId, distance, 1)
                    );

                node.Value.count--;
                return previous;
            }

        }

        public BeltInventoryNew()
        {
            items = new LinkedList<BeltInventoryElement>();
        }
    }
}
