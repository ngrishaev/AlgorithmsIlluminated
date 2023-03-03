using AlgorithmsIlluminated_I.BookI.DivideAndConquer;
using NUnit.Framework;

namespace Tests.BookI;

public class DivideAndConquerTests
{
    [TestCase(new int[]{1}, 0)]
    [TestCase(new int[]{1, 2}, 0)]
    [TestCase(new int[]{2, 1}, 1)]
    [TestCase(new int[]{1, 2, 3}, 0)]
    [TestCase(new int[]{3, 2, 1}, 3)]
    [TestCase(new int[]{4, 3, 2, 1}, 6)]
    public void Inversions(int[] a, int expected)
    {
        var result = DivideAndConquer.CountInversions(a);

        Assert.IsTrue(result == expected, $"Expected {expected}, but got {result}");
    }

    [TestCase(1, new []{2}, new int[]{2}, new int[]{4})]
    [TestCase(2, new []{1, 2, 3, 4}, new int[]{5, 6, 7, 8}, new int[]{19, 22, 43, 50})] 
    [TestCase(2, new []{1, 2, 5, 6}, new int[]{5, 6, 9, 10}, new int[]{23, 26, 79, 90})] 
    [TestCase(2, new []{3, 4, 7, 8}, new int[]{13, 14, 17, 18}, new int[]{107, 114, 227, 242})] 
    [TestCase(4,
        new []{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16},
        new []{5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20},
        new[] { 130, 140, 150, 160, 306, 332, 358, 384, 482, 524, 566, 608, 658, 716, 774, 832 })]
    public void MatricesMultiplying(int size, int[] x, int[] y, int[] expect)
    {
        var xM = new SquareMatrix(size, x);
        var yM = new SquareMatrix(size, y);

        var result = DivideAndConquer.MultiplyRec(xM, yM);

        var exceptedMatrix = new SquareMatrix(size, expect);
        Assert.IsTrue(result.Equals(exceptedMatrix));
    }
    
    [TestCase(1, new []{2}, new int[]{2}, new int[]{4})]
    [TestCase(2, new []{1, 2, 3, 4}, new int[]{5, 6, 7, 8}, new int[]{19, 22, 43, 50})] 
    [TestCase(2, new []{1, 2, 5, 6}, new int[]{5, 6, 9, 10}, new int[]{23, 26, 79, 90})] 
    [TestCase(2, new []{3, 4, 7, 8}, new int[]{13, 14, 17, 18}, new int[]{107, 114, 227, 242})] 
    [TestCase(4,
        new []{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16},
        new []{5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20},
        new[] { 130, 140, 150, 160, 306, 332, 358, 384, 482, 524, 566, 608, 658, 716, 774, 832 })]
    public void Strassen(int size, int[] x, int[] y, int[] expect)
    {
        var xM = new SquareMatrix(size, x);
        var yM = new SquareMatrix(size, y);

        var result = DivideAndConquer.Strassen(xM, yM);

        var expectedMatrix = new SquareMatrix(size, expect);
        Assert.IsTrue(result.Equals(expectedMatrix));
    }

    [TestCase(
        new []{0, 0, 1, 1},
        new []{0, 0}, new []{1, 1}
        )]
    
    [TestCase(
        new[] { 4, 5, 1, 1, 2, 3, 7, 8, 5, 6 },
        new []{4, 5}, new []{5, 6}
        )]
    public void ClosestPair(int[] coords, int[] p1ec, int[] p2ec)
    {
        Point[] ParsePoints(int[] coords)
        {
            var points = new Point[coords.Length / 2];
            for (int i = 0; i < coords.Length; i += 2) 
                points[i / 2] = new Point(coords[i], coords[i + 1]);

            return points;
        }
        Point ParsePoint(int[] coords) => new Point(coords[0], coords[1]);

        Point[] points = ParsePoints(coords);
        var p1e = ParsePoint(p1ec);
        var p2e = ParsePoint(p2ec);

        var (p1, p2) = DivideAndConquer.ClosestPair(points);

        Assert.IsTrue(
            (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y) ==
            (p2e.X - p1e.X) * (p2e.X - p1e.X) + (p2e.Y - p1e.Y) * (p2e.Y - p1e.Y),
            $"Expected points {p2e}, {p2e}. Got: {p1}, {p2}"
        );
    }
}