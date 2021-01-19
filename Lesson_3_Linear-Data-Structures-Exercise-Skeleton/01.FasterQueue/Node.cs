namespace Problem01.FasterQueue
{
    public class Node<T>
    {
        public T Element { get; set; }
        public Node<T> Next { get; set; }

        public Node(){}

        public Node(T initialValue)
        {
            this.Element = initialValue;
        }
    }
}