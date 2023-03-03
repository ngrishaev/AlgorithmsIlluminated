namespace AlgorithmsIlluminated_I.BookII;

public abstract class Heap<TValue> where TValue : IComparable<TValue>
{
    private readonly List<TValue> _elements = new List<TValue>();

    public void Insert(TValue value)
    {
        _elements.Add(value);
        Swim(_elements.Count - 1);
    }

    public TValue Extract()
    {
        var val = _elements[0];
        SwapValues(0, _elements.Count - 1);
        _elements.RemoveAt(_elements.Count - 1);
        Sink(0);
        return val;
    }

    public TValue Look() => _elements[0];

    public int Count() => _elements.Count;

    public bool Empty() => Count() == 0;

    protected abstract bool Compare(TValue value1, TValue value2);

    private void Swim(int index)
    {
        while (index > 0 && Compare(_elements[(index - 1) / 2], _elements[index]))
        {
            SwapValues((index - 1) / 2, index);
            index = (index - 1) / 2 ;
        }
    }

    private void Sink(int k)
    {
        while (2 * k + 1 < _elements.Count)
        {
            int j = 2 * k + 1;
            
            if (j < _elements.Count - 1 && Compare(_elements[j], _elements[j + 1]))
                j++;
            
            if(!Compare(_elements[k], _elements[j]))
                break;
            
            SwapValues(k, j);
            k = j;
        }
    }

    private void SwapValues(int firstIndex, int secondIndex) => 
        (_elements[firstIndex], _elements[secondIndex]) = (_elements[secondIndex], _elements[firstIndex]);

    public override string ToString() => 
        _elements.Aggregate("", (s, value) => s + value + " ").TrimEnd();
}

public class MinHeap<TValue> : Heap<TValue> where TValue : IComparable<TValue>
{
    protected override bool Compare(TValue value1, TValue value2) => value1.CompareTo(value2) > 0;
}

public class MaxHeap<TValue> : Heap<TValue> where TValue : IComparable<TValue>
{
    protected override bool Compare(TValue value1, TValue value2) => value1.CompareTo(value2) < 0;
}

public class Median
{
    private readonly MinHeap<int> _minHeap = new MinHeap<int>();
    private readonly MaxHeap<int> _maxHeap = new MaxHeap<int>();

    public void Insert(int value)
    {
        if (_minHeap.Empty() || value > _minHeap.Look())
            _minHeap.Insert(value);
        else
            _maxHeap.Insert(value);

        KeepBalance();
    }

    private void KeepBalance()
    {
        if (_minHeap.Count() - _maxHeap.Count() > 1)
            _maxHeap.Insert(_minHeap.Extract());
        else if (_maxHeap.Count() - _minHeap.Count() > 1)
            _minHeap.Insert(_maxHeap.Extract());
    }

    public int GetMedian => _minHeap.Look();
}