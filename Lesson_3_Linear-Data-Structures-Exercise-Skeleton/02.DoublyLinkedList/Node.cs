namespace Problem02.DoublyLinkedList
{
    public class Node<T>
    {
        public T Item { get; set; }

        public Node<T> Next { get; set; }

        public Node<T> Previous { get; set; }

        public Node(){}

        public Node(T initialValue)
        {
            this.Item = initialValue;
        }
    }
}