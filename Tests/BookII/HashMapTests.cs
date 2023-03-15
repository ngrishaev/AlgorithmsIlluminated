using System;
using AlgorithmsIlluminated_I.BookII;
using NUnit.Framework;

namespace Tests.BookII;

[TestFixture]
public class HashMapTests
{

    [Test]
    public void TestContains()
    {
        var map = new ChainingHashMap<int, int>(100000);
        
        map.Insert(100, 100);
        
        Assert.True(map.Lookup(100) == 100);
    }
    
    [Test]
    public void TestException()
    {
        var map = new ChainingHashMap<int, int>(100000);
        
        Assert.Catch<InvalidOperationException>(() => map.Lookup(101));
    }
    
    [Test]
    public void TestCollision()
    {
        var map = new ChainingHashMap<int, int>(10);
        
        map.Insert(1, 1);
        map.Insert(11, 11);
        
        Assert.True(map.Lookup(1) == 1);
        Assert.True(map.Lookup(11) == 11);
    }
    
    [Test]
    public void TestDeletion()
    {
        var map = new ChainingHashMap<int, int>(10);
        
        map.Insert(1, 1);
        map.Delete(1);
        
        Assert.Catch<InvalidOperationException>(() => map.Lookup(1));
    }
    
    [Test]
    public void TestContainsOpenAddress()
    {
        var map = new OpenAddressHashMap<int, int>(100000);
        
        map.Insert(100, 100);
        
        Assert.True(map.Lookup(100) == 100);
    }
    
    [Test]
    public void TestExceptionOpenAddress()
    {
        var map = new OpenAddressHashMap<int, int>(100000);
        
        Assert.Catch<InvalidOperationException>(() => map.Lookup(101));
    }
    
    [Test]
    public void TestCollisionOpenAddress()
    {
        var map = new OpenAddressHashMap<int, int>(10);
        
        map.Insert(1, 1);
        map.Insert(11, 11);
        
        Assert.True(map.Lookup(1) == 1);
        Assert.True(map.Lookup(11) == 11);
    }
    
    [Test]
    public void TestDeletionOpenAddress()
    {
        var map = new OpenAddressHashMap<int, int>(10);
        
        map.Insert(1, 1);
        map.Delete(1);
        
        Assert.Catch<InvalidOperationException>(() => map.Lookup(1));
    }

}