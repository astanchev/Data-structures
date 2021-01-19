namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> head;
        private Node<T> tail;
        public int Count { get; private set; }

        public FastQueue(){}

        public FastQueue(Node<T> item)
        {
            this.head = item;
            this.tail = item;
            this.Count++;
        }

        public FastQueue(IEnumerable<T> initialSet)
        {
            foreach (var item in initialSet)
            {
                this.Enqueue(item);
            }
        }

        public bool Contains(T item)
        {
            Node<T> current = this.head;

            while (current != null)
            {
                if (current.Element.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            this.EnsureNotEmpty();
            var headNodeElement = this.head.Element;

            if (this.Count == 1)
            {
                this.head = null;
                this.tail = null;
            }
            else
            {
                this.head = this.head.Next;
            }

            this.Count--;
            return headNodeElement;
        }

        public void Enqueue(T item)
        {
            var newNode = new Node<T>(item);

            if (this.Count == 0)
            {
                this.head = newNode;
                this.tail = newNode;
            }
            else
            {
                this.tail.Next = newNode;
                this.tail = newNode;
            }

            this.Count++;
        }

        public T Peek()
        {
            this.EnsureNotEmpty();
            return this.head.Element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.head;
            for (var i = 0; i < this.Count && current != null; i++)
            {
                yield return current.Element;

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty!");
            }
        }
    }
}