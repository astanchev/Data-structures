namespace _04.CookiesProblem
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> elements;

        public MinHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => this.elements.Count;

        public T Dequeue()
        {
            this.EnsureNotEmpty();

            var firstElement = this.Peek();

            this.Swap(0, this.Size - 1);

            this.elements.RemoveAt(this.Size - 1);

            this.HeapifyDown(0);

            return firstElement;
        }

        public void Add(T element)
        {
            this.elements.Add(element);

            this.HeapifyUp(this.Size - 1);
        }

        public T Peek()
        {
            this.EnsureNotEmpty();

            return this.elements[0];
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("MinHeap is empty!");
            }
        }

        private void Swap(int currentIndex, int parentIndex)
        {
            var temp = this.elements[currentIndex];
            this.elements[currentIndex] = this.elements[parentIndex];
            this.elements[parentIndex] = temp;
        }

        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        private bool IndexIsValid(int index)
        {
            return index > 0 && index < this.Size;
        }

        private bool IsGreater(int childIndex, int parentIndex)
        {
            return this.elements[childIndex].CompareTo(this.elements[parentIndex]) > 0;
        }

        private bool IsLess(int childIndex, int parentIndex)
        {
            return this.elements[childIndex].CompareTo(this.elements[parentIndex]) < 0;
        }

        private int GetLeftChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 1;
        }

        private int GetRightChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 2;
        }

        private void HeapifyUp(int index)
        {
            var parentIndex = this.GetParentIndex(index);

            while (this.IsLess(index, parentIndex))
            {
                this.Swap(index, parentIndex);

                index = parentIndex;
                parentIndex = this.GetParentIndex(index);
            }
        }

        private void HeapifyDown(int index)
        {
            while (true)
            {
                var swap = -1;
                var leftChildIndex = this.GetLeftChildIndex(index);
                var rightChildIndex = this.GetRightChildIndex(index);

                if (this.IndexIsValid(leftChildIndex) && this.IsLess(leftChildIndex, index))
                {
                    swap = leftChildIndex;
                }

                if (this.IndexIsValid(rightChildIndex) && this.IsLess(rightChildIndex, leftChildIndex))
                {
                    swap = rightChildIndex;
                }

                if (swap == -1)
                {
                    break;
                }

                this.Swap(swap, index);
                index = swap;
            }
        }
    }
}
