using System.Collections;

namespace DataStructuresAndAlgorithms
{
    public class Stack<T> : IEnumerable
    {
        public Stack()
        {
            _linkedList = new LinkedList<T>();
        }

        public int Count { get{ return _linkedList.Count; } }
        public bool IsEmpty { get { return _linkedList.IsEmpty; } }
        public T this[int index] { get { return _linkedList[index]; } }

        private readonly LinkedList<T> _linkedList;

        public void Push(T value)
        {
            _linkedList.Insert(value, 0);
        }

        public T Pop()
        {
            if (IsEmpty) throw new IndexOutOfRangeException();
            return _linkedList.RemoveAt(0);
        }

        public T Peek()
        {
            if (IsEmpty) throw new IndexOutOfRangeException();
            return _linkedList[0];
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _linkedList[i];
            }
        }
    }
}
