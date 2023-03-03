using AlgorithmsIlluminated_I.BookII;
using NUnit.Framework;

namespace Tests.BookII;

[TestFixture]
public class HeapTets
{

    [Test]
    public void TestMaxHeap()
    {
        var maxHeap = new MaxHeap<int>();
        
        maxHeap.Insert(1);
        maxHeap.Insert(2);
        maxHeap.Insert(3);
        maxHeap.Insert(4);
        maxHeap.Insert(5);
        maxHeap.Insert(6);
        
        maxHeap.Extract();
        maxHeap.Extract();
        maxHeap.Extract();

        Assert.Pass();
    }
    
    [Test]
    public void TestMinHeap()
    {
        var maxHeap = new MaxHeap<int>();
        
        maxHeap.Insert(1);
        maxHeap.Insert(2);
        maxHeap.Insert(3);
        maxHeap.Insert(4);
        maxHeap.Insert(5);
        maxHeap.Insert(6);
        
        maxHeap.Extract();
        maxHeap.Extract();
        maxHeap.Extract();

        Assert.Pass();
    }
    
    [Test]
    public void TestMinHeap2()
    {
        var maxHeap = new MinHeap<int>();
        
        maxHeap.Insert(6);
        maxHeap.Insert(5);
        maxHeap.Insert(3);
        maxHeap.Insert(2);
        maxHeap.Insert(4);
        maxHeap.Insert(1);
        
        maxHeap.Extract();
        maxHeap.Extract();
        maxHeap.Extract();

        Assert.Pass();
    }

    [Test]
    public void TestMaxHeap2()
    {
        var maxHeap = new MaxHeap<int>();
        
        maxHeap.Insert(70);
        maxHeap.Insert(50);
        maxHeap.Insert(60);
        maxHeap.Insert(40);

        maxHeap.Extract();
        maxHeap.Extract();
        maxHeap.Extract();

        Assert.Pass();
    }

    [Test]
    public void MedianTest()
    {
        var median = new Median();
        
        median.Insert(1);
        median.Insert(2);
        median.Insert(3);
        median.Insert(4);
        median.Insert(5);
    }
}