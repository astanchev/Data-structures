namespace Problem02.Stack
{
    public class Node<T>
    {
        public T Element { get; set; }
        public Node<T> Previous { get; set; }

        public Node(){}

        public Node(T initialValue)
        {
            this.Element = initialValue;
        }
    }
}