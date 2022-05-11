using System.Collections;

namespace DataStructuresAndAlgorithms
{
    public class LinkedList<T> : IEnumerable, ISearchable<T>
    {
        internal LinkedListNode<T>? _head;
        internal LinkedListNode<T>? _tail;
        public int Count { get; private set; }

        public LinkedList() 
        { 
            _head = null; 
            _tail = null;
            Count = 0;
        }

        public bool IsEmpty { get { return Count == 0; } }

        public void Add(T value)
        {
            if(IsEmpty)
            {
                InitializeList(value);
            }
            else
            {
                AppendToList(value);
            }
            Count++;
        }

        public void Add(LinkedList<T> linkedList)
        {
            if (linkedList.IsEmpty) return;
            if(_tail == null)
            {
                this._head = linkedList._head;
                this._tail = linkedList._tail;
                this.Count = linkedList.Count;
                return;
            }
            _tail._nextNode = linkedList._head;
            Count += linkedList.Count;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for(int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
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
            if (IsEmpty || _head == _tail || _head == null) return this;
            InitializePointers(out LinkedListNode<T>? _prev, out LinkedListNode<T>? _current, out LinkedListNode<T>? _next);
            IterateThroughListAndReversePointers(ref _prev, ref _current, ref _next);
            LinkedListNode<T> temp = _head;
            _head = _tail;
            _tail = temp;
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

        public T RemoveAt(int index)
        {
            T output;
            if(index < 0 || index >= Count) throw new IndexOutOfRangeException();
            if(index == 0)
            {
                output = _head.Value;
                RemoveHeadNode();
                return output;
            }
            if (index == Count - 1)
            {
                return Pop();
            }
            LinkedListNode<T>? node = _head;
            LinkedListNode<T>? prev = _head;
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
            output = node.Value;
            prev._nextNode = node._nextNode;
            node._nextNode = null;
            Count--;
            return output;
        }

        public T Pop()
        {
            LinkedListNode<T>? node = _head;
            LinkedListNode<T>? prev = null;
            T output;
            if (Count == 1)
            {
                output = _head.Value;
                _head = null;
                _tail = null;
                Count = 0;
                return output;
            }
            if (node == null) throw new Exception("Null linked list");
            while(node._nextNode != null)
            {
                prev = node;
                node = node._nextNode;
            }
            output = node.Value;
            _tail = prev;
            if (prev is null) return output;
            prev._nextNode = null;
            Count--;
            return output;
        }

        public void Insert(T value, int index)
        {
            if (index > Count || index < 0) throw new IndexOutOfRangeException();
            if(index == Count)
            {
                AppendToList(value);
                Count++;
                return;
            }
            LinkedListNode<T>? newNode = new LinkedListNode<T>(value);
            LinkedListNode<T>? node = _head;
            if(index == 0)
            {
                newNode._nextNode = node;
                _head = newNode;
                Count++;
                return;
            }
            for(int i = 0; i < index - 1; i++)
            {
                if (this[i] == null || node == null) throw new IndexOutOfRangeException();
                node = node._nextNode;
            }
            newNode._nextNode = node._nextNode;
            node._nextNode = newNode;
            Count++;
        }

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
            LinkedListNode<T> node = _head;
            for (int i = 0; i < index; i++)
            {
                if (node._nextNode == null) throw new IndexOutOfRangeException();
                node = node._nextNode;
            }
            return node.Value;
        }

        private void InitializeList(T value)
        {
            _head = new LinkedListNode<T>(value);
            _tail = _head;
        }

        private void AppendToList(T value)
        {
            if (_tail == null) throw new NullReferenceException("Tail is set to null");
            LinkedListNode<T> node = new LinkedListNode<T>(value);
            _tail._nextNode = node;
            _tail = node;
        }

        private void InitializePointers(out LinkedListNode<T>? _prev, out LinkedListNode<T>? _current, out LinkedListNode<T>? _next)
        {
            if (_head is null) throw new IndexOutOfRangeException();
            _prev = null;
            _current = _head;
            _next = _current._nextNode;
        }

        private void IterateThroughListAndReversePointers(ref LinkedListNode<T>? _prev, ref LinkedListNode<T>? _current, ref LinkedListNode<T>? _next)
        {
            for(int i = 0; i < Count; i++)
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

    internal class LinkedListNode<S>
    {
        internal S Value { get; }
        internal LinkedListNode<S>? _nextNode = null;

        internal LinkedListNode(S value)
        {
            if (value == null) { throw new ArgumentNullException(nameof(value)); }
            Value = value;
        }
    }
}
