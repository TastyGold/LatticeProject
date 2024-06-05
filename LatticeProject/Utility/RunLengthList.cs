using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatticeProject.Utility
{
    internal class RunLengthList<T> : ICollection<T>
    {
        private readonly LinkedList<RunLengthElement<T>> list = new();

        public int _count = 0;
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (item is null || list.Last is null) return;

            if (item.Equals(list.Last.Value.item))
            {
                list.Last.Value = new RunLengthElement<T>(list.Last.Value.item, list.Last.Value.count + 1);
            }
            else
            {
                list.AddLast(new RunLengthElement<T>(item, 1));
            }

            _count++;
        }

        public void Clear()
        {
            list.Clear();
            _count = 0;
        }

        public bool Contains(T item)
        {

        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    internal class RunLengthEnumerator : IEnumerator
    {
        public object Current => throw new NotImplementedException();

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    internal struct RunLengthElement<T>
    {
        public T item;
        public int count;

        public RunLengthElement(T item, int count)
        {
            this.item = item;
            this.count = count;
        }
    }

    internal struct ItemGapPair
    {
        public int itemId;
        public float distance;

        public ItemGapPair(int itemId, float distance)
        {
            this.itemId = itemId;
            this.distance = distance;
        }

        public override bool Equals(object? obj)
        {
            return obj is ItemGapPair pair
                && itemId == pair.itemId
                && distance == pair.distance;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(itemId, distance);
        }
    }
}
