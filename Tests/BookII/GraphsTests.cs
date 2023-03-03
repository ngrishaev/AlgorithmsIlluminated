using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmsIlluminated_I.BookII;
using NUnit.Framework;

namespace Tests.BookII;

[TestFixture]
public class BFSTests
{
    [Test]
    public void Test()
    {
        var s = new Node<char>('s');
        var a = new Node<char>('a');
        var b = new Node<char>('b');
        var c = new Node<char>('c');
        var d = new Node<char>('d');
        var e = new Node<char>('e');
        var f = new Node<char>('f');

        s.ConnectBoth(a);
        s.ConnectBoth(b);
        
        a.ConnectBoth(s);
        a.ConnectBoth(c);
        
        b.ConnectBoth(s);
        b.ConnectBoth(c);
        b.ConnectBoth(d);
        
        c.ConnectBoth(a);
        c.ConnectBoth(b);
        c.ConnectBoth(d);
        c.ConnectBoth(e);
        
        d.ConnectBoth(b);
        d.ConnectBoth(c);
        d.ConnectBoth(e);

        e.ConnectBoth(c);
        e.ConnectBoth(d);

        var graph = new []{s, a, b, c, d, e, f};

        foreach (var node in SimpleGraph.IterateBfs(s)) 
            Console.Write(node + " ");

        Console.WriteLine();
        
        foreach (var node in SimpleGraph.IterateDfs(s)) 
            Console.Write(node + " ");
        
        Console.WriteLine();
        
        foreach (var node in SimpleGraph.DfsRecursive(s)) 
            Console.Write(node + " ");
    }

    [Test]
    public void TopologicalSort()
    {
        var a = new Node<char>('a');
        var b = new Node<char>('b');
        var c = new Node<char>('c');
        var d = new Node<char>('d');
        var e = new Node<char>('e');
        var f = new Node<char>('f');
        var g = new Node<char>('g');
        var h = new Node<char>('h');
        var i = new Node<char>('i');
        var j = new Node<char>('j');
        var k = new Node<char>('k');
        
        a.Connect(b);
        a.Connect(d);
        b.Connect(e);
        c.Connect(f);
        d.Connect(e);
        d.Connect(f);
        e.Connect(h);
        e.Connect(g);
        f.Connect(g);
        f.Connect(i);
        g.Connect(j);
        g.Connect(i);
        h.Connect(j);

        var graph = new []{a, b, c, d, e, f, g, h, i, j, k};

        foreach (var node in SimpleGraph.TopologicalSort(graph.ToList())) 
            Console.Write(node + " ");
    }

    [Test]
    public void Kosaraju()
    {
        var n1 = new Node<string>("1");
        var n2 = new Node<string>("2");
        var n3 = new Node<string>("3");
        var n4 = new Node<string>("4");
        var n5 = new Node<string>("5");
        var n6 = new Node<string>("6");
        var n7 = new Node<string>("7");
        var n8 = new Node<string>("8");
        var n9 = new Node<string>("9");
        var n10 = new Node<string>("10");
        var n11 = new Node<string>("11");
        
        n1.Connect(n3);
        
        n3.Connect(n11);
        n3.Connect(n5);
        
        n5.Connect(n1);
        n5.Connect(n9);
        n5.Connect(n7);
        
        n11.Connect(n6);
        n11.Connect(n8);
        
        n6.Connect(n10);
        n10.Connect(n8);
        n8.Connect(n6);
        
        n7.Connect(n9);
        
        n9.Connect(n8);
        n9.Connect(n2);
        n9.Connect(n4);
            
        n4.Connect(n7);
        
        n2.Connect(n4);
        n2.Connect(n10);

        var g = new List<Node<string>>(new[]
        {
            n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11
        });
        
        // Console.WriteLine("Original:");
        // SimpleGraph<string>.Print(g);
        // Console.WriteLine("");
        // Console.WriteLine("Reversed");
        // SimpleGraph<string>.Print(SimpleGraph<string>.Reverse(g));
        
        foreach (var kvp in SimpleGraph.Kosaraju(g))
        {
            Console.WriteLine($"Node: {kvp.Key}. SCC: {kvp.Value}");
        }
        
    }
    
    [Test]
    public void Kosaraju2()
    {
        var n1 = new Node<string>("1");
        var n2 = new Node<string>("2");
        var n3 = new Node<string>("3");
        var n4 = new Node<string>("4");
        var n5 = new Node<string>("5");
        var n6 = new Node<string>("6");
        var n7 = new Node<string>("7");
        var n8 = new Node<string>("8");
        var n9 = new Node<string>("9");

        n1.Connect(n7);
        n7.Connect(n4);
        n4.Connect(n1);
        n7.Connect(n9);
        
        n9.Connect(n6);
        n3.Connect(n9);
        n6.Connect(n3);
        n6.Connect(n8);
        
        n8.Connect(n2);
        n5.Connect(n8);
        n2.Connect(n5);
        
        var g = new List<Node<string>>(new[]
        {
            n1, n2, n3, n4, n5, n6, n7, n8, n9,
        });
        
        // Console.WriteLine("Original:");
        // SimpleGraph<string>.Print(g);
        // Console.WriteLine("");
        // Console.WriteLine("Reversed");
        // SimpleGraph<string>.Print(SimpleGraph<string>.Reverse(g));
        
        foreach (var kvp in SimpleGraph.Kosaraju(g))
        {
            Console.WriteLine($"Node: {kvp.Key}. SCC: {kvp.Value}");
        }
        
    }
    
    [Test]
    public void Kosaraju3()
    {
        var n0 = new Node<string>("0");
        var n1 = new Node<string>("1");
        var n2 = new Node<string>("2");
        var n3 = new Node<string>("3");
        var n4 = new Node<string>("4");
        var n5 = new Node<string>("5");
        var n6 = new Node<string>("6");
        var n7 = new Node<string>("7");

        n0.Connect(n1);
        n1.Connect(n2);
        n2.Connect(n0);
        n2.Connect(n3);
        
        n3.Connect(n4);
        
        n4.Connect(n5);
        n4.Connect(n7);
        n5.Connect(n6);
        n6.Connect(n7);
        n6.Connect(n5);

        var g = new List<Node<string>>(new[]
        {
            n0, n1, n2, n3, n4, n5, n6, n7,
        });
        
        // Console.WriteLine("Original:");
        // SimpleGraph<string>.Print(g);
        // Console.WriteLine("");
        // Console.WriteLine("Reversed");
        // SimpleGraph<string>.Print(SimpleGraph<string>.Reverse(g));
        
        foreach (var kvp in SimpleGraph.Kosaraju(g))
        {
            Console.WriteLine($"Node: {kvp.Key}. SCC: {kvp.Value}");
        }
        
    }
    
    
    [Test]
    public void Kosaraju4()
    {
        var a = new Node<string>("a");
        var b = new Node<string>("b");
        var c = new Node<string>("c");
        var d = new Node<string>("d");
        var e = new Node<string>("e");
        var f = new Node<string>("f");

        a.Connect(b);
        b.Connect(c);
        c.Connect(a);
        c.Connect(e);
        
        e.Connect(f);
        f.Connect(d);
        d.Connect(e);

        var g = new List<Node<string>>(new[]
        {
            a, b, c, d, e, f
        });
        
        // Console.WriteLine("Original:");
        // SimpleGraph<string>.Print(g);
        // Console.WriteLine("");
        // Console.WriteLine("Reversed");
        // SimpleGraph<string>.Print(SimpleGraph<string>.Reverse(g));
        
        foreach (var kvp in SimpleGraph.Kosaraju(g))
        {
            Console.WriteLine($"Node: {kvp.Key}. SCC: {kvp.Value}");
        }
        
    }

    [Test]
    public void Reverse()
    {
        var a = new Node<string>("a");
        var b = new Node<string>("b");
        var c = new Node<string>("c");
        
        a.Connect(b);
        b.Connect(c);
        c.Connect(a);  
        
        Console.WriteLine("Original:");
        var graph = new List<Node<string>>(new []{a, b, c});
        SimpleGraph.Print(graph);
        Console.WriteLine("");
        Console.WriteLine("Reversed");
        SimpleGraph.Print(SimpleGraph.Reverse(graph));
    }
}