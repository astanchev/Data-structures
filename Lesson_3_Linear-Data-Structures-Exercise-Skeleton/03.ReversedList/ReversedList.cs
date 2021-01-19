namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;
        private T[] items;

        public int Capacity { get; private set; }
        public int Count { get; private set; }
        public bool IsReadOnly => false;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            this.Capacity = capacity;
            this.items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(this.Count - 1 - index);
                return this.items[this.Count - 1 - index];
            }
            set
            {
                this.ValidateIndex(index);
                this.items[index] = value;
            }
        }

        public void Add(T item)
        {
            this.ResizeIfNecessary();
            this.items[this.Count++] = item;
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) >= 0;
        }

        public int IndexOf(T item)
        {
            for (var i = 0; i < this.Count; i++)
            {
                if ((item == null && this.items[i] == null) || this.items[this.Count - 1 - i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);
            this.ResizeIfNecessary();

            for (var i = this.Count; i > this.Count - index; i--)
            {
                this.items[i] = this.items[i - 1];
            }
            this.items[this.Count - index] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            var index = this.IndexOf(item);
            if (index < 0)
            {
                return false;
            }

            this.RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);

            for (var i = this.Count - 1 - index; i < this.Count - 1; i++)
            {
                this.items[i] = this.items[i + 1];
            }
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = this.Count - 1; i >= 0; i--)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ResizeIfNecessary()
        {
            if (this.Count == this.Capacity)
            {
                this.Resize();
            }
        }

        private void Resize()
        {
            var newCapacity = this.Capacity * 2;
            var newArray = new T[newCapacity];
            for (var i = 0; i < this.Count; i++)
            {
                newArray[i] = this.items[i];
            }

            this.items = newArray;
            this.Capacity = newCapacity;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}