namespace AlgorithmsIlluminated_I.BookII;

public class SearchTree
{
    private Node? _root;

    public SearchTree()
    {
        
    }
    
    public void Insert(int value)
    {
        if(_root == null)
        {
            _root = new Node(value);
            return;
        }
        
        var currentNode = _root;
        while (true)
        {
            if (currentNode.Key < value)
            {
                if(currentNode.Right == null)
                {
                    currentNode.AddRightChild(value);
                    break;
                }
                else
                {
                    currentNode = currentNode.Right;
                }
            }
            else if (currentNode.Key >= value)
            {
                if(currentNode.Left == null)
                {
                    currentNode.AddLeftChild(value);
                    break;
                }
                else
                {
                    currentNode = currentNode.Left;
                }
            }
        }
    }

    public int Search(int value)
    {
        return SearchNode(value).Key;
    }

    public int Max()
    {
        if(_root == null)
            throw new InvalidOperationException($"Cant get max in empty tree");
        return MaxInSubtree(_root).Key;
    }

    public int Min()
    {
        if(_root == null)
            throw new InvalidOperationException($"Cant get min in empty tree");
        
        return MinInSubtree(_root).Key;
    }
    
    public (int value, bool success) Predecessor(int value)
    {
        Node node = SearchNode(value);
        if(node.Left != null)
            return (MaxInSubtree(node.Left).Key, true);

        var child = node;
        var parent = child.Parent;
        while (parent != null)
        {
            if (child == parent.Right)
                return (parent.Key, true);
            
            child = parent;
            parent = parent.Parent;
        }

        return (default, false);
    }
    
    public void OutputSortedRecursive()
    {
        void OutputSorted(Node? treeRoot)
        {
            if(treeRoot == null)
                return;
            
            OutputSorted(treeRoot.Left);
            Console.Write(treeRoot.Key + " ");
            OutputSorted(treeRoot.Right);
        }
        
        OutputSorted(_root);
        Console.WriteLine();
    }
    
    public void OutputSortedIterative()
    {
        if(_root == null)
            return;

        var stack = new Stack<Node>();
        var current = _root;

        while (current != null || stack.Count > 0)
        {
            while (current != null)
            {
                stack.Push(current);
                current = current.Left;
            }

            current = stack.Pop();
            Console.Write(current.Key + " ");
            current = current.Right;
        }
    }

    public void Delete(int value)
    {
        void SwapChildInParent(Node baseNode, Node? swapWith)
        {
            if (baseNode.Parent == null)
                _root = swapWith;
            else if (baseNode.Parent.Left == baseNode)
                baseNode.Parent.Left = swapWith;
            else
                baseNode.Parent.Right = swapWith;
        }

        void DeleteFullParent(Node node)
        {
            var maxLeaf = MaxInSubtree(node.Left);
            
            SwapChildInParent(node, maxLeaf);
            SwapChildInParent(maxLeaf, node);

            (maxLeaf.Parent, node.Parent) = (node.Parent, maxLeaf.Parent);
            node.Left.Parent = node.Right.Parent = maxLeaf;

            maxLeaf.Left = node.Left;
            maxLeaf.Right = node.Right;
            
            node.Left = node.Right = null;
            
            DeleteLeaf(node);
        }

        void DeleteSingleParent(Node node)
        {
            var child = (node.Left ?? node.Right)!;
            SwapChildInParent(node, child);
            
            child.Parent = node.Parent;
        }

        void DeleteLeaf(Node node)
        {
            SwapChildInParent(node, null);
        }

        var toDeletion = SearchNode(value);
        if (toDeletion.Left == null && toDeletion.Right == null)
            DeleteLeaf(toDeletion);
        else if (toDeletion.Left == null || toDeletion.Right == null)
            DeleteSingleParent(toDeletion);
        else
            DeleteFullParent(toDeletion);
    }

    public int Select(int index)
    {
        int Select(int index, Node root)
        {
            var leftSize = root.Left?.Size ?? 0;

            if(index < leftSize)
                return Select(index, root.Left!);
            if (index > leftSize)
                return Select(index - leftSize - 1, root.Right!);
            
            return root.Key;
        }
        
        if(_root == null)
            throw new InvalidOperationException($"Cant get {index} element in empty tree");
        if(_root.Size < index)
            throw new InvalidOperationException($"Cant get {index} element in tree with size {_root.Size}");
        
        return Select(index, _root);
    }
    
    public int SelectIterative(int index)
    {
        if(_root == null)
            throw new InvalidOperationException($"Cant get {index} element in empty tree");
        if(_root.Size < index)
            throw new InvalidOperationException($"Cant get {index} element in tree with size {_root.Size}");

        var currentNode = _root;
        var leftSize = currentNode.Left?.Size ?? 0;
        while (leftSize != index)
        {
            if (index < leftSize)
            {
                currentNode = currentNode.Left;
                index = index;
            }
            else
            {
                currentNode = currentNode.Right;
                index = index - leftSize - 1;
            }
            
            leftSize = currentNode.Left?.Size ?? 0;
        }

        return currentNode.Key;
    }

    private Node MaxInSubtree(Node treeRoot)
    {
        while (treeRoot.Right != null)
            treeRoot = treeRoot.Right;
        return treeRoot;
    }

    private Node MinInSubtree(Node treeRoot)
    {
        while (treeRoot.Left != null)
            treeRoot = treeRoot.Left;
        return treeRoot;
    }
    
    private Node SearchNode(int value)
    {
        if(_root == null)
            throw new InvalidOperationException($"There is no {value} in tree");
        
        Node? currentNode = _root;
        if (value == currentNode.Key)
            return currentNode;

        while (currentNode != null)
        {
            if (value < currentNode.Key)
                currentNode = currentNode.Left;
            else if (value > currentNode.Key)
                currentNode = currentNode.Right;
            else
                return currentNode;
        }
        throw new InvalidOperationException($"There is no {value} in tree");
    }

    private class Node
    {
        public readonly int Key;
        public Node? Left { get; set; }
        public Node? Parent { get; set; }
        public Node? Right { get; set; }
        public Node(int key)
        {
            Key = key;
        }
        public void AddRightChild(int value)
        {
            Right = new Node(value) { Parent = this };
        }
        public void AddLeftChild(int value)
        {
            Left = new Node(value) { Parent = this };
        }

        public int Size => (Left?.Size ?? 0) + (Right?.Size ?? 0) + 1;
        public override string ToString() => Key.ToString();
    }
}