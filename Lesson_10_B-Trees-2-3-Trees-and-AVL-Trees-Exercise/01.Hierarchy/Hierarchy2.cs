namespace _01.Hierarchy
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;


    // Doesn't performs well  in performance tests - it is slow data structure O(log n) - Insert, Retrieve, Remove
    // Hash table used in Dictionary implementation is faster then tree structure implementation O(1) for average case(O(n) - worse case) - Insert, Retrieve, Remove
    public class Hierarchy2<T> : IHierarchy<T>
    {
        private Node<T> root;

        public Hierarchy2(T root)
        {
            this.root = this.CreateNode(root);
            this.Count = 1;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(this.root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node.Value;

                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get; private set; }

        public void Add(T element, T child)
        {
            var parentNode = this.FindBfs(element);
            var childNode = this.FindBfs(child);
            
            CheckIfEmptyNode(parentNode);

            if (childNode != null)
            {
                throw new ArgumentException();
            }

            var node = CreateNode(child);
            node.Parent = parentNode;
            parentNode.Children.Add(node);
            this.Count++;
        }

        public void Remove(T element)
        {
            if (this.root.Value.Equals(element))
            {
                throw new InvalidOperationException();
            }

            var searchNode = this.FindBfs(element);
            this.CheckIfEmptyNode(searchNode);

            if (searchNode.Parent != null && searchNode.Children.Count > 0)
            {
                foreach (var child in searchNode.Children)
                {
                    child.Parent = searchNode.Parent;
                    searchNode.Parent.Children.Add(child);
                }
            }
            
            searchNode.Parent?.Children.Remove(searchNode);
            this.Count--;
        }

        public IEnumerable<T> GetChildren(T element)
        {
            var searchNode = this.FindBfs(element);
            this.CheckIfEmptyNode(searchNode);

            return searchNode.Children.Select(c => c.Value);
        }

        public T GetParent(T element)
        {
            var searchNode = this.FindBfs(element);
            this.CheckIfEmptyNode(searchNode);

            return searchNode.Parent != null ? searchNode.Parent.Value : default(T);
        }

        public bool Contains(T element)
        {
            var searchNode = this.FindBfs(element);

            return searchNode != null;
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            foreach (var el in this)
            {
                if (other.Contains(el))
                {
                    yield return el;
                }
            }
        }

        private Node<T> CreateNode(T element)
        {
            var node = new Node<T>(element);

            return node;
        }

        private void CheckIfEmptyNode(Node<T> searchedNode)
        {
            if (searchedNode == null)
            {
                throw new ArgumentException();
            }
        }

        private Node<T> FindBfs(T value)
        {
            var queue = new Queue<Node<T>>();
            queue.Enqueue(this.root);

            while (queue.Count > 0)
            {
                var subtree = queue.Dequeue();

                if (subtree.Value.Equals(value))
                {
                    return subtree;
                }

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        private void ContainsItemOrThrowException(T element)
        {
            if (!Contains(element))
            {
                throw new ArgumentException();
            }
        }
    }
}