namespace DataStructuresAndAlgorithms
{
    public class LinkedList<T>
    {
        private Node<T>? _head;
        private Node<T>? _tail;

        public LinkedList() 
        { 
            _head = null; 
            _tail = null;
        }

        public LinkedList(T value)
        {
            InitializeList(value);
        }

        public bool IsEmpty() => _head == null;

        public void Append(T value)
        {
            if(IsEmpty())
            {
                InitializeList(value);
                return;
            }
            AppendToList(value);
        }

        private void InitializeList(T value)
        {
            _head = new Node<T>(value);
            _tail = _head;
        }

        private void AppendToList(T value)
        {
            if (_tail == null) throw new NullReferenceException("Tail is set to null");
            Node<T> node = new(value);
            _tail.SetNext(node);
            _tail = node;
        }
    }

    internal class Node<S>
    {
        internal S _value;
        internal Node<S>? _nextNode = null;

        internal Node(S value)
        {
            if (value == null) { throw new ArgumentNullException(nameof(value)); }
            _value = value;
        }

        internal void SetNext(Node<S> node)
        {
            _nextNode = node;
        }

        internal Node<S>? GetNext()
        {
            return _nextNode;
        }
    }
}
