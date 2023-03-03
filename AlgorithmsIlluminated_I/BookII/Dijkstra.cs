namespace AlgorithmsIlluminated_I.BookII;

public class Dijkstra
{
    public static Dictionary<WeightedNode<TValue>, int> Solve<TValue>(WeightedNode<TValue> start, List<WeightedNode<TValue>> graph)
    {
        var distances = graph.Aggregate(new Dictionary<WeightedNode<TValue>, int>(), (d, node) =>
        {
            d[node] = int.MaxValue;
            return d;
        });
        distances[start] = 0;
        
        var visited = new List<WeightedNode<TValue>>();
        var expand = new List<(WeightedNode<TValue> node, int distance)> { (start, 0) };
        while (expand.Any())
        {
            var (node, distance) = GetNextNode(expand);
            expand.Remove((node, distance));
            visited.Add(node);
            foreach (var edge in node.GetNeighbours())
            {
                if(visited.Contains(edge.End))
                    continue;
                
                var newDistance = distances[node] + edge.Weight;
                if (newDistance >= distances[edge.End])
                    continue;
                
                distances[edge.End] = newDistance;
                expand.Add((edge.End, newDistance));
            }
        }
        return distances;
        
        (WeightedNode<TValue> node, int distance) GetNextNode(List<(WeightedNode<TValue> node, int distance)> nodes) =>
            nodes.MinBy(n => n.distance);
    }
}

public class WeightedNode<TValue>
{
    private readonly HashSet<Edge<WeightedNode<TValue>>> _neighbours = new HashSet<Edge<WeightedNode<TValue>>>();
    public TValue Value { get; }

    public WeightedNode(TValue value)
    {
        Value = value;
    }

    public IEnumerable<Edge<WeightedNode<TValue>>> GetNeighbours() => _neighbours;
    public void ConnectTo(WeightedNode<TValue> neighbour, int weight) => _neighbours.Add(new Edge<WeightedNode<TValue>>(this, neighbour, weight));
    public override string ToString() => Value?.ToString() ?? "null";
    public bool Equals(WeightedNode<TValue> obj) => obj.Value?.Equals(Value) ?? false;
}

public class Edge<TValue>
{
    public TValue Start { get; }
    public TValue End { get; }
    public int Weight { get; }

    public Edge(TValue start, TValue end, int weight)
    {
        Start = start;
        End = end;
        Weight = weight;
    }

    public override string ToString() => $"{Start}--{Weight}->{End}";
} 