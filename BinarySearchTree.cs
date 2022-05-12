namespace DataStructuresAndAlgorithms
{
    public class BinarySearchTree<T> : ISearchable<T> where T : IComparable
    {
        public BinarySearchTree()
        {
            _root = null;
            Count = 0;
        }

        private BinarySearchTreeNode<T>? _root;
        public int Count { get; private set; }

        public void Add(T value)
        {
            Count++;
            if (_root == null)
            {
                InitializeTree(value);
                return;
            }
            Insert(value, _root);
        }

        private void InitializeTree(T value)
        {
            _root = new BinarySearchTreeNode<T>(value);
        }

        private void Insert(T value, BinarySearchTreeNode<T>? node)
        {
            if (node == null)
            {
                node = new BinarySearchTreeNode<T>(value);
                return;
            }
            if (value.CompareTo(node.Value) == 0) return;
            if (value.CompareTo(node.Value) > 0)
            {
                if (node.Right == null) node.SetRight(value);
                Insert(value, node.Right);
                return;
            }
            if(value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null) node.SetLeft(value);
                Insert(value, node.Left);
            }
        }

        public bool Contains(T value)
        {
            if (_root == null) return false;
            return DepthFirstSearchForValue(value, _root);
        }

        private bool DepthFirstSearchForValue(T value, BinarySearchTreeNode<T> node)
        {
            if (node == null) return false;
            if (node.Value.CompareTo(value) == 0) return true;
            if (node.Value.CompareTo(value) < 0) return DepthFirstSearchForValue(value, node.Left);
            return DepthFirstSearchForValue(value, node.Right);
        }
    }

    internal class BinarySearchTreeNode<T> where T:IComparable
    {
        internal BinarySearchTreeNode(T value)
        {
            Value = value;
        }

        internal T Value { get; }
        internal BinarySearchTreeNode<T>? Left { get; private set; } = null;
        internal BinarySearchTreeNode<T>? Right { get; private set; } = null;

        internal void SetLeft(T value)
        {
            this.Left = new BinarySearchTreeNode<T>(value);
        }

        internal void SetRight(T value)
        {
            this.Right = new BinarySearchTreeNode<T>(value);
        }
    }
}
