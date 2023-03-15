using System;
using AlgorithmsIlluminated_I.BookII;
using NUnit.Framework;

namespace Tests.BookII;

[TestFixture]
public class SearchTreeTests
{
    [Test]
    public void TestOutput()
    {
        var tree = new SearchTree();
        
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(1);
        tree.Insert(3);
        
        tree.OutputSortedIterative();
        Console.WriteLine();
        tree.OutputSortedRecursive();
        Assert.Pass();
    }

    
    [Test]
    public void TestSearch()
    {
        var tree = new SearchTree();
        
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(1);
        tree.Insert(3);
        
        Assert.AreEqual(tree.Search(1), 1);
        Assert.AreEqual(tree.Search(2), 2);
        Assert.AreEqual(tree.Search(3), 3);
        Assert.AreEqual(tree.Search(4), 4);
        Assert.AreEqual(tree.Search(5), 5);
    }
    
    [Test]
    public void TestPredecessors()
    {
        var tree = new SearchTree();
        
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(1);
        tree.Insert(3);
        
        Assert.AreEqual(tree.Predecessor(2).value, 1);
        Assert.AreEqual(tree.Predecessor(3).value, 2);
        Assert.AreEqual(tree.Predecessor(4).value, 3);
        Assert.AreEqual(tree.Predecessor(5).value, 4);
    }

    [Test]
    public void TestMax()
    {
        var tree = new SearchTree();
        
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(1);
        tree.Insert(3);
        
        Assert.AreEqual(tree.Max(), 5);
    }
    
    [Test]
    public void TestMin()
    {
        var tree = new SearchTree();
        
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(1);
        tree.Insert(3);
        
        Assert.AreEqual(tree.Min(), 1);
    }
    
    [Test]
    public void TestDeleteLeaf()
    {
        var tree = new SearchTree();
        
        tree.Insert(3);
        tree.Insert(1);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(2);
        
        tree.Delete(2);
        
        Assert.Pass();
    }
    
    [Test]
    public void TestDeleteWithOneChild()
    {
        var tree = new SearchTree();
        
        tree.Insert(3);
        tree.Insert(1);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(2);
        
        tree.Delete(5);
        
        Assert.Pass();
    }
    
    [Test]
    public void TestDeleteFullParent()
    {
        var tree = new SearchTree();
        
        tree.Insert(3);
        tree.Insert(1);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(2);
        
        tree.Delete(3);
        tree.Delete(5);
        tree.Delete(2);
        tree.Delete(1);
        tree.Delete(4);
        
        Assert.Pass();
    }
    
    [Test]
    public void TestSelect()
    {
        var tree = new SearchTree();
        
        tree.Insert(3);
        tree.Insert(1);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(2);
        
        Assert.AreEqual(tree.Select(0), 1);
        Assert.AreEqual(tree.Select(1), 2);
        Assert.AreEqual(tree.Select(2), 3);
        Assert.AreEqual(tree.Select(3), 4);
        Assert.AreEqual(tree.Select(4), 5);
    }
    
    [Test]
    public void TestSelectIterative()
    {
        var tree = new SearchTree();
        
        tree.Insert(3);
        tree.Insert(1);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(2);
        
        Assert.AreEqual(tree.SelectIterative(0), 1);
        Assert.AreEqual(tree.SelectIterative(1), 2);
        Assert.AreEqual(tree.SelectIterative(2), 3);
        Assert.AreEqual(tree.SelectIterative(3), 4);
        Assert.AreEqual(tree.SelectIterative(4), 5);
    }
}