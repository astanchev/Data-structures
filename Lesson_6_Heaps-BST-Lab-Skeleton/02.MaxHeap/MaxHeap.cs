namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> elements;

        public MaxHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => this.elements.Count;

        public void Add(T element)
        {
            this.elements.Add(element);

            this.HeapifyUp();
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
                throw new InvalidOperationException("MaxHeap is empty!");
            }
        }

        private void HeapifyUp()
        {
            int currentIndex = this.Size - 1;
            int parentIndex = this.GetParentIndex(currentIndex);

            while (this.IndexIsValid(currentIndex) && this.IsGreater(currentIndex, parentIndex))
            {
                this.Swap(currentIndex, parentIndex);

                currentIndex = parentIndex;
                parentIndex = this.GetParentIndex(currentIndex);
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
    }
}
