namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> children;
        
        public T Key { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this.children.AsReadOnly();
        
        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this.children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
                child.Parent = this;
            }
        }


        public void AddChild(Tree<T> child)
        {
            this.children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            StringBuilder result = new StringBuilder();

            this.OrderDfsForString(0, result, this);

            return result.ToString().Trim();
        }

        public List<T> GetLeafKeys()
        {
            var leafKeys = new List<T>();
            var nodes = new Queue<Tree<T>>();
            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();

                if (this.IsLeaf(currentNode))
                {
                    leafKeys.Add(currentNode.Key);
                }

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }

            return leafKeys;

            //Func<Tree<T>, bool> leafKeysPredicate = (node) => this.IsLeaf(node);
            //return this.OrderBfs(leafKeysPredicate);
        }

        public List<T> GetMiddleKeys()
        {
            var middleKeys = new List<T>();
            var nodes = new Queue<Tree<T>>();
            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();

                if (this.IsMiddle(currentNode))
                {
                    middleKeys.Add(currentNode.Key);
                }

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }

            return middleKeys;
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            var DictionaryOfNodes = new Dictionary<int,List<Tree<T>>>();

            var dept = 0;

            this.GetDeepestLeftomostNodeWithDFS(this, DictionaryOfNodes, dept);
           
            return DictionaryOfNodes[DictionaryOfNodes.Count][0];
        }

        public List<T> GetLongestPath()
        {
            var DictionaryOfNodes = new Dictionary<int, List<Tree<T>>>();

            var dept = 0;

            this.GetDeepestLeftomostNodeWithDFS(this, DictionaryOfNodes, dept);

            var lastElementInLongestPath = DictionaryOfNodes[DictionaryOfNodes.Count][0];

            var longestPath = new List<T>();

            var currentElementInLongestPath = lastElementInLongestPath;

            while (currentElementInLongestPath != null)
            {
                longestPath.Add(currentElementInLongestPath.Key);
                currentElementInLongestPath = currentElementInLongestPath.Parent;
            }

            longestPath.Reverse();

            return longestPath;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var result = new List<List<T>>();

            var currentPath = new List<T>();
            currentPath.Add(this.Key);

            int currentSum = Convert.ToInt32(this.Key);

            this.PathsWithGivenSumWithDFS(this, currentPath, result, ref currentSum, sum);

            return result;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var result = new List<Tree<T>>();
            var listOfNodes = this.GetAllNodesWithBFS(this);

            foreach (var node in listOfNodes)
            {
                int subtreeSum = this.GetSubtreeSumWithDFS(node);

                if (subtreeSum == sum)
                {
                    result.Add(node);
                }
            }

            return result;
        }

        private void OrderDfsForString(int depth, StringBuilder result, Tree<T> subtree)
        {
            result.Append(new string(' ', depth))
                .Append(subtree.Key)
                .Append(Environment.NewLine);

            foreach (var child in subtree.Children)
            {
                this.OrderDfsForString(depth + 2, result, child);
            }
        }

        private bool IsLeaf(Tree<T> node)
        {
            return node.Children.Count == 0;
        }

        private bool IsRoot(Tree<T> node)
        {
            return node.Parent == null;
        }

        private bool IsMiddle(Tree<T> node)
        {
            return node.Parent != null && node.Children.Count > 0;
        }

        private List<T> OrderBfs(Func<Tree<T>, bool> predicate)
        {
            var result = new List<T>();
            var nodes = new Queue<Tree<T>>();
            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();

                if (predicate.Invoke(currentNode))
                {
                    result.Add(currentNode.Key);
                }

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }

            return result;
        }

        private List<Tree<T>> OrderBfs()
        {
            var result = new List<Tree<T>>();
            var nodes = new Queue<Tree<T>>();
            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();
                
                result.Add(currentNode);

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }

            return result;
        }

        private void GetDeepestLeftomostNodeWithDFS(Tree<T> currentNode, Dictionary<int, List<Tree<T>>> listOfNodes, int dept)
        {
            dept++;

            foreach (var childNode in currentNode.children)
            {
                if (!listOfNodes.ContainsKey(dept))
                {
                    listOfNodes.Add(dept, new List<Tree<T>>());
                }

                listOfNodes[dept].Add(childNode);

                this.GetDeepestLeftomostNodeWithDFS(childNode, listOfNodes, dept);
            }
        }

        private List<Tree<T>> GetAllNodesWithBFS(Tree<T> tree)
        {
            var result = new List<Tree<T>>();
            var nodes = new Queue<Tree<T>>();

            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();
                result.Add(currentNode);

                foreach (var child in currentNode.children)
                {
                    nodes.Enqueue(child);
                }
            }

            return result;
        }

        private int GetSubtreeSumWithDFS(Tree<T> parentNode)
        {
            int currentSum = Convert.ToInt32(parentNode.Key);
            int childSum = 0;

            foreach (var child in parentNode.children)
            {
                childSum += this.GetSubtreeSumWithDFS(child);
            }

            return currentSum + childSum;
        }
        
        private void PathsWithGivenSumWithDFS(
            Tree<T> currentNode,
            List<T> currentPath,
            List<List<T>> expectedPath,
            ref int currentSum,
            int expectedSum)
        {
            foreach (var child in currentNode.children)
            {
                currentPath.Add(child.Key);
                currentSum += Convert.ToInt32(child.Key);
                PathsWithGivenSumWithDFS(child, currentPath, expectedPath, ref currentSum, expectedSum);
            }

            if (currentSum == expectedSum)
            {
                expectedPath.Add(new List<T>(currentPath));
            }

            currentPath.RemoveAt(currentPath.Count - 1);
            currentSum -= Convert.ToInt32(currentNode.Key);
        }
    }
}
