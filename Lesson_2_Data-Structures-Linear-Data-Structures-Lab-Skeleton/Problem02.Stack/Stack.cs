namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> top;

        public int Count { get; private set; }

        public Stack(){}

        public Stack(Node<T> top)
        {
            this.top = top;
            this.Count = 1;
        }

        public Stack(IEnumerable<T> initialSet)
        {
            foreach (var item in initialSet)
            {
                this.Push(item);
            }
        }

        public bool Contains(T item)
        {
            Node<T> current = this.top;

            while (current != null)
            {
                if (current.Element.Equals(item))
                {
                    return true;
                }

                current = current.Previous;
            }

            return false;
        }

        public T Peek()
        {
            this.EnsureNotEmpty();
            return this.top.Element;
        }

        public T Pop()
        {
            this.EnsureNotEmpty();

            var topNodeElement = this.top.Element;
            var newTop = this.top.Previous;

            this.top.Previous = null;

            this.top = newTop;
            this.Count--;

            return topNodeElement;
        }

        public void Push(T item)
        {
            var newNode = new Node<T>
            {
                Element = item,
                Previous = top
            };

            this.top = newNode;
            this.Count++;

        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.top;
            for (var i = 0; i < this.Count && current != null; i++)
            {
                yield return current.Element;

                current = current.Previous;
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
                throw new InvalidOperationException("Stack is empty!");
            }
        }
    }
}