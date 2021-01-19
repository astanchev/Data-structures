namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> head;
        public int Count { get; private set; }

        public Queue(){}

        public Queue(Node<T> head)
        {
            this.head = head;
            this.Count++;
        }

        public Queue(IEnumerable<T> initialSet)
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
            var newHead = this.head.Next;

            this.head.Next = null;

            this.head = newHead;
            this.Count--;

            return headNodeElement;
        }

        public void Enqueue(T item)
        {
            var newNode = new Node<T>(item);
            var currentNode = this.head;

            if (currentNode == null)
            {
                this.head = newNode;
            }
            else
            {
                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }

                currentNode.Next = newNode;
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