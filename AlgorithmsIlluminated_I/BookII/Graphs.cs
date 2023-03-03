namespace AlgorithmsIlluminated_I.BookII;

public static class SimpleGraph
{
    public static IEnumerable<Node<TValue>> IterateBfs<TValue>(Node<TValue> from)
    {
        var visited = new List<Node<TValue>> { from };
        yield return from;

        var exploreFrom = new Queue<Node<TValue>>();
        exploreFrom.Enqueue(from);
        while(exploreFrom.Any())
        {
            var exploring = exploreFrom.Dequeue();

            foreach (var neighbour in exploring.GetNeighbours())
            {
                if (visited.Contains(neighbour))
                    continue;
                
                yield return neighbour;
                visited.Add(neighbour);
                exploreFrom.Enqueue(neighbour);
            }
        }
    }

    public static Dictionary<Node<TValue>, int> BfsMinDistance<TValue>(Node<TValue> from)
    {
        var distances = new Dictionary<Node<TValue>, int> { [from] = 0 };
        var exploredNodes = new List<Node<TValue>> { from };
        var nodesToExplore = new Queue<Node<TValue>>();
        nodesToExplore.Enqueue(from);
        
        while(nodesToExplore.Any())
        {
            var exploring = nodesToExplore.Dequeue();

            foreach (var neighbour in exploring.GetNeighbours())
            {
                if (exploredNodes.Contains(neighbour))
                    continue;

                distances[neighbour] = distances[exploring] + 1;
                exploredNodes.Add(neighbour);
                nodesToExplore.Enqueue(neighbour);
            }
        }

        return distances;
    }
    
    public static Dictionary<Node<TValue>, int> Ucc<TValue>(List<Node<TValue>> graphNodes)
    {
        var exploredNodes = new List<Node<TValue>>();
        var ucc = new Dictionary<Node<TValue>, int>();
        var indexCc = -1;
        foreach (var node in graphNodes)
        {
            if (exploredNodes.Contains(node))
                continue;
            
            indexCc++;
            
            var nodesToExplore = new Queue<Node<TValue>>();
            nodesToExplore.Enqueue(node);
        
            while(nodesToExplore.Any())
            {
                var exploring = nodesToExplore.Dequeue();
                ucc[exploring] = indexCc;
                foreach (var neighbour in exploring.GetNeighbours())
                {
                    if (exploredNodes.Contains(neighbour))
                        continue;
                    
                    exploredNodes.Add(neighbour);
                    nodesToExplore.Enqueue(neighbour);
                }
            }
        }
        
        return ucc;
    }
    
    public static IEnumerable<Node<TValue>> IterateDfs<TValue>(Node<TValue> from)
    {
        var exploredNodes = new List<Node<TValue>> { };

        var nodesToExplore = new Stack<Node<TValue>>();
        nodesToExplore.Push(from);
        
        while(nodesToExplore.Any())
        {
            var exploring = nodesToExplore.Pop();

            if (exploredNodes.Contains(exploring))
                continue;
            
            yield return exploring;
            exploredNodes.Add(exploring);
            
            foreach (var neighbour in exploring.GetNeighbours()) 
                nodesToExplore.Push(neighbour);
        }
    }
    
    public static IEnumerable<Node<TValue>> DfsRecursive<TValue>(Node<TValue> from)
    {
        IEnumerable<Node<TValue>> DfsImpl(Node<TValue> currentNode, List<Node<TValue>> visitedNodes)
        {
            yield return currentNode;
            visitedNodes.Add(currentNode);

            foreach (var node in currentNode.GetNeighbours())
            {
                if (visitedNodes.Contains(node))
                    continue;
                
                foreach (var nextExplored in DfsImpl(node, visitedNodes))
                    yield return nextExplored;
            }
        }

        return DfsImpl(from, new List<Node<TValue>>());
    }
    
    public static Node<TValue>[] TopologicalSort<TValue>(List<Node<TValue>> graph)
    {
        void DfsTopo(
            Node<TValue> node,
            List<Node<TValue>> visitedNodes,
            List<Node<TValue>> dfsVisited)
        {
            visitedNodes.Add(node);

            foreach (var neighbour in node.GetNeighbours())
                if (!visitedNodes.Contains(neighbour))
                    DfsTopo(neighbour, visitedNodes, dfsVisited);
            dfsVisited.Add(node);
        }

        var visited = new List<Node<TValue>>();
        var ordering = new Node<TValue>[graph.Count];
        var lastIndex = graph.Count - 1;
        foreach (Node<TValue> node in graph)
        {
            if (visited.Contains(node) == false)
            {
                var dfsTraversal = new List<Node<TValue>>();
                DfsTopo(node, visited, dfsTraversal);
                
                foreach (var dfsNode in dfsTraversal) 
                    ordering[lastIndex--] = dfsNode;
            }
        }

        return ordering;
    }

    public static Dictionary<Node<TValue>, int> Kosaraju<TValue>(List<Node<TValue>> graph)
    {
        var scc = new Dictionary<Node<TValue>, int>();
        var topoOrder = TopologicalSort(graph);
        var rev = Reverse(graph);
        var count = 0;
        var visited = new List<Node<TValue>>();
        
        foreach (var node in topoOrder)
        {
            var rNode = rev.Find(n => n.Equals(node));
            if (visited.Contains(rNode!))
                continue;
            count++;
            Dfs(rNode!);
        }

        return scc;

        void Dfs(Node<TValue> node)
        {
            visited.Add(node);
            scc[node] = count;
            foreach (var neighbour in node.GetNeighbours())
                if (!visited.Contains(neighbour))
                    Dfs(neighbour);
        }
    }


    

    public static void Print<TValue>(List<Node<TValue>> graph)
    {
        foreach (var node in graph)
        foreach (var neighbour in node.GetNeighbours())
            Console.WriteLine($"{node.Value}->{neighbour.Value}");
    }

    public static List<Node<TValue>> Reverse<TValue>(List<Node<TValue>> graph)
    {
        var transpose = graph.Select(node => new Node<TValue>(node)).ToList();

        foreach (var node in graph)
        foreach (var neighbour in node.GetNeighbours())
        {
            var dNode = transpose.Find(n => n.Value.Equals(node.Value));
            var sNode = transpose.Find(n => n.Value.Equals(neighbour.Value));
            sNode.Connect(dNode);
        }

        return transpose;
    }
}

public class Node<TValue>
{
    private readonly HashSet<Node<TValue>> _neighbours = new HashSet<Node<TValue>>();
    public TValue Value { get; }

    public Node(TValue value, params Node<TValue>[] neighbours)
    {
        Value = value;
        foreach (var neighbour in neighbours) 
            _neighbours.Add(neighbour);
    }
        
    public Node(Node<TValue> node):this(node.Value, new Node<TValue>[]{}) {}
    public Node(TValue value) : this(value, new Node<TValue>[]{}) { }
    public Node(TValue value, IEnumerable<Node<TValue>> neighbours) : this(value, neighbours.ToArray()) { }

    public IEnumerable<Node<TValue>> GetNeighbours() => _neighbours;

    public void ConnectBoth(Node<TValue> neighbour)
    {
        _neighbours.Add(neighbour);
        neighbour._neighbours.Add(this);
    }
        
    public void Connect(Node<TValue> neighbour)
    {
        _neighbours.Add(neighbour);
    }

    public void DisconnectBoth(Node<TValue> neighbour)
    {
        _neighbours.Remove(neighbour);
        neighbour._neighbours.Remove(this);
    }

    public void ClearConnections()
    {
        _neighbours.Clear();
    }

    public override string ToString() => Value?.ToString() ?? "null";

    public bool Equals(Node<TValue> obj)
    {
        return obj.Value?.Equals(Value) ?? false;
    }
}

