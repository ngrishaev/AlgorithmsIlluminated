namespace AlgorithmsIlluminated_I.BookII;

public interface IHashMap<TKey, TValue>
{
    public TValue Lookup(TKey key);
    public void Insert(TKey key, TValue value);
    public void Delete(TKey key);
}

public class ChainingHashMap<TKey, TValue> : IHashMap<TKey, TValue>
{
    private readonly Bucket[] _array;

    public ChainingHashMap(int size)
    {
        _array = new Bucket[size];
        for (int i = 0; i < _array.Length; i++) 
            _array[i] = new Bucket();
    }

    public TValue Lookup(TKey key) => 
        _array[GetHash(key, _array.Length)].Lookup(key);

    public void Insert(TKey key, TValue value) => 
        _array[GetHash(key, _array.Length)].Insert(key, value);

    public void Delete(TKey key) => 
        _array[GetHash(key, _array.Length)].Remove(key);

    private int GetHash(TKey key, int size) => 
        Math.Abs(key.GetHashCode()) % size;

    private class Bucket
    {
        private readonly List<MapNode> _bucket = new List<MapNode>();

        public void Insert(TKey key, TValue value)
        {
            if (Contains(key))
                _bucket.First(n => n.Key!.Equals(key)).Value = value;
            else
                _bucket.Add(new MapNode(key, value));
        }

        public void Remove(TKey key)
        {
            var index = _bucket.FindIndex(node => node.Key!.Equals(key));
            if(index >= 0)
                _bucket.RemoveAt(index);
        }
        
        public TValue Lookup(TKey key)
        {
            var index = _bucket.FindIndex(node => node.Key!.Equals(key));
            if (index < 0)
                throw new InvalidOperationException($"Key {key} not found");
            
            return _bucket[index].Value;
        }

        private bool Contains(TKey key)
        {
            return _bucket.Any(n => n.Key!.Equals(key));
        }
    }

    private class MapNode
    {
        public TKey Key { get; }
        public TValue Value { get; set; }

        public MapNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    
}

public class OpenAddressHashMap<TKey, TValue> : IHashMap<TKey, TValue>
{
    private readonly MapNode[] _array;

    public OpenAddressHashMap(int size)
    {
        _array = new MapNode[size];
    }
    
    public TValue Lookup(TKey key)
    {
        var index = FindIndex(key);
        if (index == -1)
            throw new InvalidOperationException($"Key {key} not found");

        if(_array[index] == null)
            throw new InvalidOperationException($"Key {key} not found");

        return _array[index].Value;
    }

    public void Insert(TKey key, TValue value)
    {
        var index = FindIndex(key);

        if (index == -1)
            throw new InvalidOperationException("Map is full");

        _array[index] = new MapNode(key, value);
    }

    public void Delete(TKey key)
    {
        var index = FindIndex(key);
        
        if (index == -1)
            throw new InvalidOperationException("Element already deleted");

        _array[index] = null;
    }

    private int FindIndex(TKey key)
    {
        var index = Hash(key, _array.Length);
        while ((!_array[index]?.Key!.Equals(key) ?? false) && index < _array.Length)
            index++;
        
        if (index >= _array.Length)
            return -1;
        
        return index;
    }

    private int Hash(TKey key, int size) => 
        Math.Abs(key.GetHashCode()) % size;
    
    private class MapNode
    {
        public TKey Key { get; }
        public TValue Value { get; set; }

        public MapNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}