using System.Collections;

namespace DataStructuresAndAlgorithms
{
    public class Queue<T> : IEnumerable, ISearchable<T>
    {
        public Queue()
        {
            _linkedList = new LinkedList<T>();
        }

        public int Count { get { return _linkedList.Count; } }
        public bool IsEmpty { get { return _linkedList.IsEmpty; } }
        public T this[int index] { get { return _linkedList[index]; } }

        private readonly LinkedList<T> _linkedList;

        public void Enqueue(T value)
        {
            _linkedList.Add(value);
        }

        public T Dequeue()
        {
            if (IsEmpty) throw new IndexOutOfRangeException();
            return _linkedList.RemoveAt(0);
        }

        public T Peek()
        {
            return _linkedList[0];
        }

        public IEnumerator GetEnumerator()
        {
            for(int i = 0; i < Count; i++)
            {
                yield return _linkedList[i];
            }
        }

        public bool Contains(T value)
        {
            return _linkedList.Contains(value);
        }
    }
}
