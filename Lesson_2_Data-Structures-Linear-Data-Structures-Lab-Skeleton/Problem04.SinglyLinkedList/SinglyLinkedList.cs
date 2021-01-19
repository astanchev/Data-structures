namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        public int Count { get; private set; }

        public SinglyLinkedList() { }

        public SinglyLinkedList(Node<T> head)
        {
            this.head = head;
            this.Count = 1;
        }

        public SinglyLinkedList(IEnumerable<T> initialSet)
        {
            foreach (var item in initialSet)
            {
                this.AddLast(item);
            }
        }

        public void AddFirst(T item)
        {
            var newNode = new Node<T>(item);
            newNode.Next = this.head;
            this.head = newNode;
            this.Count++;
        }

        public void AddLast(T item)
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

        public T GetFirst()
        {
            this.EnsureNotEmpty();
            return this.head.Element;
        }

        public T GetLast()
        {
            this.EnsureNotEmpty();
            var currentNode = this.head;

            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }

            return currentNode.Element;
        }

        public T RemoveFirst()
        {
            this.EnsureNotEmpty();
            var headNodeElement = this.head.Element;
            var newHead = this.head.Next;

            this.head.Next = null;

            this.head = newHead;
            this.Count--;

            return headNodeElement;
        }

        public T RemoveLast()
        {
            this.EnsureNotEmpty();
            var currentNode = this.head;
            var lastNode = new Node<T>();

            if (this.Count == 1)
            {
                lastNode = currentNode;
                this.head = null;
            }
            else
            {
                while (currentNode.Next.Next != null)
                {
                    currentNode = currentNode.Next;
                }

                lastNode = currentNode.Next;
                currentNode.Next = null;
            }

            this.Count--;
            return lastNode.Element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this.head;

            while (current != null)
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
                throw new InvalidOperationException("Linked List is empty!");
            }
        }
    }
}