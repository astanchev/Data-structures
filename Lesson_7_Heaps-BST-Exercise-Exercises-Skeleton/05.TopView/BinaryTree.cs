namespace _05.TopView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
        {
            this.Value = value;
            this.LeftChild = left;
            this.RightChild = right;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public List<T> TopView()
        {
            var view = new List<T>();

            if (this.Value != null)
            {
                view.Add(this.Value);
            }

            if (this.LeftChild != null)
            {
                addLeftSide(this.LeftChild, view);
            }

            if (this.RightChild != null)
            {
                addRightSide(this.RightChild, view);
            }

            return view;
        }

        private void addRightSide(BinaryTree<T> rightChild, List<T> result)
        {
            if (rightChild != null)
            {
                result.Add(rightChild.Value);

                this.addRightSide(rightChild.RightChild, result);
            }
        }

        private void addLeftSide(BinaryTree<T> leftChild, List<T> result)
        {
            if (leftChild != null)
            {
                result.Add(leftChild.Value);

                this.addLeftSide(leftChild.LeftChild, result);
            }
        }

        ////public List<T> TopView()
        ////{
        ////    var offsetToValueLevel = new SortedDictionary<int, KeyValuePair<T, int>>();

        ////    this.FillDictionaryDfs(this, 0, 1, offsetToValueLevel);

        ////    return offsetToValueLevel.Values.Select(kvp => kvp.Key).ToList();
        ////}

        ////private void FillDictionaryDfs(BinaryTree<T> subtree, int offset, int level, SortedDictionary<int, KeyValuePair<T, int>> offsetToValueLevel)
        ////{
        ////    if (subtree == null)
        ////    {
        ////        return;
        ////    }

        ////    if (!offsetToValueLevel.ContainsKey(offset))
        ////    {
        ////        offsetToValueLevel.Add(offset, new KeyValuePair<T, int>(subtree.Value, level));
        ////    }

        ////    if (level < offsetToValueLevel[offset].Value)
        ////    {
        ////        offsetToValueLevel[offset] = new KeyValuePair<T, int>(subtree.Value, level);
        ////    }

        ////    this.FillDictionaryDfs(subtree.LeftChild, offset - 1, level + 1, offsetToValueLevel);
        ////    this.FillDictionaryDfs(subtree.RightChild, offset + 1, level + 1, offsetToValueLevel);
        ////}

        //public List<T> TopView()
        //{
        //    var views = new SortedDictionary<int, KeyValuePair<T, int>>();
        //    var horizontalDistance = 0;
        //    var level = 0;
        //    var maxLevel = 0;
        //    PreOrderTraverse(this, views, horizontalDistance, level, maxLevel);

        //    var result = new List<T>();
        //    foreach (var kvp in views.Values)
        //    {
        //        result.Add(kvp.Key);
        //    }

        //    return result;
        //}

        //private void PreOrderTraverse(BinaryTree<T> current,
        //    SortedDictionary<int, KeyValuePair<T, int>> views,
        //    int horizontalDistance, int level, int maxLevel)
        //{
        //    if (views.ContainsKey(horizontalDistance))
        //    {
        //        if (level < maxLevel)
        //        {
        //            maxLevel = level;
        //            views[horizontalDistance] = new KeyValuePair<T, int>(current.Value, level);
        //        }
        //    }
        //    else
        //    {
        //        views.Add(horizontalDistance, new KeyValuePair<T, int>(current.Value, level));
        //    }

        //    if (current.LeftChild != null)
        //    {
        //        PreOrderTraverse(current.LeftChild, views, horizontalDistance - 1, level + 1, maxLevel);
        //    }

        //    if (current.RightChild != null)
        //    {
        //        PreOrderTraverse(current.RightChild, views, horizontalDistance + 1, level + 1, maxLevel);
        //    }
        //}
    }
}
