using LatticeProject.Utility;
using System.Collections;

namespace LatticeProject.Game.Belts
{
    internal class BeltInventory : IEnumerable<BeltInventoryElement>
    {
        //list of RLE groups of items on the belt from TAIL to HEAD (same id and distance)
        public readonly LinkedList<BeltInventoryElement> items;

        //the item furthest along the belt that is not stationary (not yet bunched up at the end)
        public LinkedListNode<BeltInventoryElement>? ItemToMove { get; private set; } = null;

        //number of total items on the belt
        public int Count { get; private set; }

        //the distance from the head of the belt to the first item
        public float LeadingDistance { get; private set; }

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

        private const float maximumItemSnapDistance = 0.1f;

        public bool CanRecieveItem() => LeadingDistance >= 0;

        /// <summary> Adds a new item to the head of the belt (exact position offset can be specified with distanceToHead).</summary>
        public void AddToHead(GameItem item, float distanceFromHead)
        {
            if (items.Last is null) //list is empty
            {
                items.AddFirst(new BeltInventoryElement(item, TotalBeltLength - distanceFromHead, 1));
                ItemToMove = items.First;

                LeadingDistance = distanceFromHead;
            }
            else
            {
                LeadingDistance = GetLeadingDistanceFromItems(); //recalculate LeadingDistance

                float maxPosition = LeadingDistance - GameRules.minItemDistance;

                float newItemPosition; //the position on the belt the new item will be set to
                if (distanceFromHead >= maxPosition - maximumItemSnapDistance)
                {
                    newItemPosition = maxPosition;
                }
                else
                {
                    newItemPosition = distanceFromHead;
                }

                float newItemDistanceToNext; //distance between the new item and old first item
                if (newItemPosition >= maxPosition)
                {
                    newItemDistanceToNext = GameRules.minItemDistance;
                }
                else
                {
                    newItemDistanceToNext = LeadingDistance - newItemPosition;
                }

                if (newItemDistanceToNext.IsNearlyEqual(items.Last.Value.distance, maximumItemSnapDistance) && items.Last.Value.item.Equals(item))
                {
                    //increases the quantity of the RLE chain closest to the head of the belt
                    items.Last.Value.count++;
                }
                else
                {
                    //creates a new RLE at the head of the belt
                    items.AddLast(new BeltInventoryElement(item, newItemDistanceToNext, 1));
                    if (ItemToMove is null) ItemToMove = items.Last;
                }

                //update the leading distance after new item is added
                LeadingDistance = newItemPosition;
            }

            Count++; //update the inventory item count
        }

        public float CalculateSumOfDistances()
        {
            float sum = 0;
            foreach (BeltInventoryElement element in items)
            {
                sum += element.distance * element.count;
            }
            return sum;
        }
        public float GetLeadingDistanceFromItems()
        {
            return TotalBeltLength - CalculateSumOfDistances();
        }
        public float CalculateLeadingDistanceError()
        {
            return LeadingDistance - GetLeadingDistanceFromItems();
        }

        /// <summary> Removes the item closest to the tail of the belt from the inventory.</summary>
        public void RemoveTailingItem()
        {
            if (items.First is null) return;

            if (items.First.Value.count > 1) //if the first RLE element has multiple items
            {
                items.First.Value.count--;
                ItemToMove = SetFirstInElementDistance(items.First, items.First.Value.distance * 2);
            }
            else //first RLE element only has one item
            {
                if (items.First.Next is null) //if there is only one item on the belt
                {
                    items.RemoveFirst();
                    LeadingDistance = TotalBeltLength;
                    ItemToMove = null;
                }
                else
                {
                    LinkedListNode<BeltInventoryElement> second = items.First.Next;
                    ItemToMove = SetFirstInElementDistance(second, second.Value.distance + items.First.Value.distance);
                    items.RemoveFirst();
                }
            }

            //update the inventory item count
            Count--;
        }
        
        /// <returns>The GameItem and offset to transfer to the associated deposit inventory</returns>
        public GameItemWithOffset? MoveItems(float distance, float endOfBeltPadding, bool canTransfer)
        {
            float remainingDistance = distance;

            //item transferring
            //note: guarantees the while loop will be skipped
            GameItemWithOffset? transferItem = null;
            if (canTransfer && items.First is not null)
            {
                //reset itemToMove to tailing element
                ItemToMove = items.First;
                if (ItemToMove.Value.count != 1)
                {
                    //ensure first element has only one item
                    SeparateFirstItemFromElement(ItemToMove);
                }

                //move tailing item requiredDistance (while loop will be skipped)
                items.First.Value.distance -= remainingDistance;
                remainingDistance = 0;

                if (items.First.Value.distance < GameRules.minItemDistance)
                {
                    //send tailing item to deposit inventory
                    transferItem = new GameItemWithOffset(
                        items.First.Value.item,
                        -items.First.Value.distance
                        );
                    RemoveTailingItem();
                }
            }

            //TODO: implement endOfBeltPadding

            //terminates when all remainingDistance has been used OR when all items are stationary
            while (remainingDistance > 0 && ItemToMove is not null)
            {
                if (ItemToMove.Value.distance <= GameRules.minItemDistance) //if ItemToMove is already minItemDistance
                {
                    //try to combine element with previous and move on to next element
                    ItemToMove.Value.distance = GameRules.minItemDistance;
                    ItemToMove = TryCombineNodeWithPrevious(ItemToMove);
                }
                else
                {
                    if (ItemToMove.Value.count > 1) //if element has more than one item
                    {
                        //separate the first item in the element to be the ItemToMove
                        ItemToMove = SeparateFirstItemFromElement(ItemToMove);
                    }

                    //if remaining distance is greater than that which can be subtracted from the current ItemToMove
                    if (remainingDistance > ItemToMove.Value.distance - GameRules.minItemDistance)
                    {
                        //move ItemToMove to minItemDistance
                        remainingDistance -= ItemToMove.Value.distance - GameRules.minItemDistance;
                        ItemToMove.Value.distance = GameRules.minItemDistance;

                        //and try to combine with previous RLE chunk
                        ItemToMove = TryCombineNodeWithPrevious(ItemToMove);
                    }
                    else
                    {
                        //move ItemToMove the remainingDistance and complete execution
                        ItemToMove.Value.distance -= remainingDistance;
                        remainingDistance = 0;
                    }
                }
            }

            //updates leading distance based on how much the items on the belt moved
            LeadingDistance += distance - remainingDistance;

            return transferItem;
        }

        /// <summary>Attempts to combine the counts of two RLE elements if they have the same id and distance.</summary>
        /// <returns>The next node after the inputted node.</returns>
        public LinkedListNode<BeltInventoryElement>? TryCombineNodeWithPrevious(LinkedListNode<BeltInventoryElement> node)
        {
            LinkedListNode<BeltInventoryElement>? output;

            if (node.Previous is not null && node.Previous.Value.item.Equals(node.Value.item))
            {
                //combine ItemToMove element with previous RLE element

                //update count of previous RLE to include ItemToMove count
                node.Previous.Value.count += node.Value.count;

                //remove the ItemToMove node from items
                LinkedListNode<BeltInventoryElement> itemToRemove = node;
                output = node.Next;
                items.Remove(itemToRemove);
            }
            else
            {
                output = node.Next;
            }

            return output;
        }

        /// <returns>A reference to the new separated node</returns>
        public LinkedListNode<BeltInventoryElement> SeparateFirstItemFromElement(LinkedListNode<BeltInventoryElement> node)
        {
            if (node.Value.count <= 1)
            {
                //if node only has one item, no separation is required
                return node;
            }
            else
            {
                //creates a new linkedListNode containing the first item of the target element
                LinkedListNode<BeltInventoryElement> previous = items.AddBefore(node, new BeltInventoryElement(node.Value.item, node.Value.distance, 1));
                node.Value.count--;
                return previous;
            }
        }

        /// <returns>A reference to the new or modified node that now has the inputted distance</returns>
        public LinkedListNode<BeltInventoryElement> SetFirstInElementDistance(LinkedListNode<BeltInventoryElement> node, float distance)
        {
            if (node.Value.distance == distance) return node; //no change is made

            //separates the trailing item from a multi-item element and gives it the specified distance
            LinkedListNode<BeltInventoryElement> firstInNode = SeparateFirstItemFromElement(node);
            firstInNode.Value.distance = distance;

            return firstInNode;
        }

        public string GetInventoryDescription()
        {
            string output = $"count={Count}\n";
            int i = 0;
            int itemToMoveIndex = -1;

            foreach (BeltInventoryElement item in items)
            {
                output += $"i={i}, " + item.ToString() + '\n';
                if (item == ItemToMove?.Value) itemToMoveIndex = i;
                i++;
            }
            output += $"itemToMove={(itemToMoveIndex != -1 ? itemToMoveIndex : "null")}\n";

            return output;
        }

        public IEnumerator<BeltInventoryElement> GetEnumerator()
        {
            return new BeltInventoryEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public BeltInventory()
        {
            items = new LinkedList<BeltInventoryElement>();
        }
    }
}
