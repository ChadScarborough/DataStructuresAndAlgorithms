using System.Collections;

namespace DataStructuresAndAlgorithms
{
    public class LinkedList<T> : IEnumerable, ISearchable<T>
    {
        internal Node<T>? _head;
        internal Node<T>? _tail;
        public int Count { get; private set; }

        public LinkedList() 
        { 
            _head = null; 
            _tail = null;
            Count = 0;
        }

        public LinkedList(T value)
        {
            InitializeList(value);
            Count = 1;
        }

        public bool IsEmpty() => Count == 0;

        public void Add(T value)
        {
            
            if(IsEmpty())
            {
                InitializeList(value);
            }
            else
            {
                AppendToList(value);
            }
            Count++;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LinkedListEnumerator<T>(this);
        }

        public bool Contains(T value)
        {
            foreach(T t in this)
            {
                if (t.Equals(value)) return true;
            }
            return false;
        }

        public LinkedList<T> Reverse()
        {
            if (IsEmpty() || _head == _tail || _head == null) return this;
            InitializePointers(out Node<T>? _prev, out Node<T>? _current, out Node<T>? _next);
            IterateThroughListAndReversePointers(ref _prev, ref _current, ref _next);
            return this;
        }

        public T this[int index]
        {
            get
            {
                if(index >= 0 && index < Count)
                {
                    return IterateThroughListAndReturnValueAtIndex(index);
                }
                throw new IndexOutOfRangeException();
            }
        }

        public void RemoveAt(int index)
        {
            if(index < 0 || index >= Count) throw new IndexOutOfRangeException();
            if(index == 0)
            {
                RemoveHeadNode();
                return;
            }
            Node<T>? node = _head;
            Node<T>? prev = _head;
            for(int i = 0; i < index; i++)
            {
                if (node is null || prev is null || node._nextNode is null || prev._nextNode is null)
                {
                    throw new IndexOutOfRangeException();
                }
                node = node._nextNode;
                if (i != 0) prev = prev._nextNode;
            }
            if (prev is null || node is null) throw new IndexOutOfRangeException();
            prev._nextNode = node._nextNode;
            node._nextNode = null;
        }



        //TODO: Remove, Insert, Pop, Add(overload for another linked list)

        private void RemoveHeadNode()
        {
            if (_head == null) throw new IndexOutOfRangeException();
            if (Count == 1)
            {
                _head = null;
                _tail = null;
            }
            else
            {
                _head = _head._nextNode;
            }
            Count--;
            return;
        }

        private T IterateThroughListAndReturnValueAtIndex(int index)
        {
            if (_head == null) throw new IndexOutOfRangeException();
            Node<T> node = _head;
            for (int i = 0; i < index; i++)
            {
                if (node._nextNode == null) throw new IndexOutOfRangeException();
                node = node._nextNode;
            }
            return node._value;
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

        private void InitializePointers(out Node<T>? _prev, out Node<T>? _current, out Node<T>? _next)
        {
            if (_head is null) throw new IndexOutOfRangeException();
            _prev = null;
            _current = _head;
            _next = _current._nextNode;
        }

        private void IterateThroughListAndReversePointers(ref Node<T>? _prev, ref Node<T>? _current, ref Node<T>? _next)
        {
            foreach (Node<T> node in this)
            {
                if (_current is null) throw new IndexOutOfRangeException();
                _current._nextNode = _prev;
                _prev = _current;
                _current = _next;
                if (_current != null)
                {
                    _next = _current._nextNode;
                }
            }
        }
    }

    internal class LinkedListEnumerator<T> : IEnumerator<Node<T>>
    {
        private LinkedList<T> _linkedList;

        public LinkedListEnumerator(LinkedList<T> linkedList)
        {
            if (linkedList == null) throw new ArgumentNullException("Null linked list");
            if (linkedList._head == null) throw new Exception("Null head node");
            _linkedList = linkedList;
            Current = linkedList._head;
        }

        public Node<T> Current { get; private set; }

        object? IEnumerator.Current => Current;

        public void Dispose(){ }

        public bool MoveNext()
        {
            if(Current._nextNode == null)
            {
                return false;
            }
            Current = Current._nextNode;
            return true;
        }

        public void Reset()
        {
            if (_linkedList._head == null) throw new Exception("Null head node");
            Current = _linkedList._head;
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
    }
}
