namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> children;

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this.children.AsReadOnly();

        public bool IsRootDeleted { get; private set; }

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this.children = new List<Tree<T>>();
            this.IsRootDeleted = false;
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (Tree<T> child in children)
            {
                child.Parent = this;
                this.children.Add(child);
            }
        }

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            if (this.IsRootDeleted)
            {
                return result;
            }

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Tree<T> subtree = queue.Dequeue();

                result.Add(subtree.Value);

                foreach (Tree<T> child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            var result = new List<T>();

            if (this.IsRootDeleted)
            {
                return result;
            }

            this.DfsIteration(this, result);
            
            //this.DfsRecursion(this, result);

            return result;
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var searchedNode = this.FindBfs(parentKey);

            CheckIfEmptyNode(searchedNode);

            searchedNode.children.Add(child);
        }

        public void RemoveNode(T nodeKey)
        {
            var searchNode = this.FindBfs(nodeKey);
            this.CheckIfEmptyNode(searchNode);

            foreach (var child in searchNode.Children)
            {
                child.Parent = null;
            }

            searchNode.children.Clear();

            var searchParent = searchNode.Parent;

            if (searchParent == null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                searchParent.children.Remove(searchNode);
            }

            searchNode.Value = default(T);
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindBfs(firstKey);
            this.CheckIfEmptyNode(firstNode);

            var secondNode = this.FindBfs(secondKey);
            this.CheckIfEmptyNode(secondNode);

            var firstParent = firstNode.Parent;
            var secondParent = secondNode.Parent;

            if (firstParent == null)
            {
                SwapRoot(this, secondNode);
                return;
            }

            if (secondParent == null)
            {
                SwapRoot(this, firstNode);
                return;
            }
            
            firstNode.Parent = secondParent;
            secondNode.Parent = firstParent;

            int indexOfFirst = firstParent.children.IndexOf(firstNode);
            int indexOfSecond = secondParent.children.IndexOf(secondNode);

            firstParent.children[indexOfFirst] = secondNode;
            secondParent.children[indexOfSecond] = firstNode;
        }

        private void DfsRecursion(Tree<T> tree, List<T> result)
        {
            foreach (var child in tree.Children)
            {
                this.DfsRecursion(child, result);
            }

            result.Add(tree.Value);
        }

        private List<T> DfsIteration(Tree<T> tree, List<T> result)
        {
            var tempResult = new Stack<T>();
            var stack = new Stack<Tree<T>>();

            stack.Push(tree);

            while (stack.Count != 0)
            {
                var subtree = stack.Pop();

                foreach (var child in subtree.Children)
                {
                    stack.Push(child);
                }

                tempResult.Push(subtree.Value);
            }
            
            result = new List<T>(tempResult);

            return result;
        }

        private void CheckIfEmptyNode(Tree<T> searchedNode)
        {
            if (searchedNode == null)
            {
                throw new ArgumentNullException();
            }
        }

        private Tree<T> FindDfs(T value, Tree<T> subtree)
        {
            if (subtree.Value.Equals(value))
            {
                return subtree;
            }

            foreach (var child in subtree.Children)
            {
                Tree<T> current = this.FindDfs(value, child);

                if (current != null && current.Value.Equals(value))
                {
                    return current;
                }
            }

            return null;
        }

        private Tree<T> FindBfs(T value)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

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

        private void SwapRoot(Tree<T> root, Tree<T> node)
        {
            root.Value = node.Value;
            root.children.Clear();

            foreach (var child in node.Children)
            {
                root.children.Add(child);
            }
        }
    }
}
