namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;
        public int Count { get; private set; }

        public DoublyLinkedList() { }

        public DoublyLinkedList(Node<T> item)
        {
            this.head = item;
            this.tail = item;
            this.Count = 1;
        }

        public DoublyLinkedList(IEnumerable<T> initialSet)
        {
            foreach (var item in initialSet)
            {
                this.AddLast(item);
            }
        }

        public void AddFirst(T item)
        {
            var newNode = new Node<T>(item);

            if (this.Count == 0)
            {
                this.head = newNode;
                this.tail = newNode;
            }
            else
            {
                this.head.Previous = newNode;
                newNode.Next = this.head;
                this.head = newNode;
            }

            this.Count++;
        }

        public void AddLast(T item)
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
                newNode.Previous = this.tail;
                this.tail = newNode;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.EnsureNotEmpty();
            return this.head.Item;
        }

        public T GetLast()
        {
            this.EnsureNotEmpty();
            return this.tail.Item;
        }

        public T RemoveFirst()
        {
            this.EnsureNotEmpty();
            var headNodeElement = this.head.Item;

            var newHead = this.head.Next;

            if (newHead != null)
            {
                newHead.Previous = null;
            }

            this.head.Next = null;

            this.head = newHead;
            this.Count--;

            return headNodeElement;
        }

        public T RemoveLast()
        {
            this.EnsureNotEmpty();
            var tailNodeElement = this.tail.Item;

            var newTail = this.tail.Previous;

            if (newTail != null)
            {
                newTail.Next = null;
            }

            this.tail.Previous = null;

            this.tail = newTail;
            this.Count--;

            return tailNodeElement;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this.head;

            while (current != null)
            {
                yield return current.Item;
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
                throw new InvalidOperationException("Linked List is empty!");
            }
        }
    }
}