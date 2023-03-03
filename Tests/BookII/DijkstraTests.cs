using System.Collections.Generic;
using AlgorithmsIlluminated_I.BookII;
using NUnit.Framework;

namespace Tests.BookII;

[TestFixture]
public class DijkstraTests
{

    [Test]
    public void Test()
    {
        var n0 = new WeightedNode<string>("0");
        var n1 = new WeightedNode<string>("1");
        var n2 = new WeightedNode<string>("2");
        var n3 = new WeightedNode<string>("3");
        var n4 = new WeightedNode<string>("4");
        
        n0.ConnectTo(n1, 4);
        n0.ConnectTo(n2, 1);
        n1.ConnectTo(n3, 1);
        n2.ConnectTo(n1, 2);
        n2.ConnectTo(n3, 5);
        n3.ConnectTo(n4, 3);

        var g = new List<WeightedNode<string>>() { n0, n1, n2, n3, n4 };

        var result = Dijkstra.Solve(n0, g);
        Assert.Pass();
    }
    
}