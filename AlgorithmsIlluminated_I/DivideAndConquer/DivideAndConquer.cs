using System.Collections;

namespace AlgorithmsIlluminated_I.DivideAndConquer;

public static class DivideAndConquer
{
    public static int CountInversions(int[] a)
    {
        int CountSplitInversions(int[] a, int mergeFrom, int mergeTo, int middle)
        {
            var inversionsCount = 0;

            var leftIndex = mergeFrom;
            var rightIndex = middle;
            var merged = new int[mergeTo - mergeFrom];

            for (var i = 0; i < merged.Length; i++)
            {
                if (leftIndex >= middle)
                    merged[i] = a[rightIndex++];
                else if (rightIndex >= mergeTo)
                    merged[i] = a[leftIndex++];
                else
                {
                    if (a[leftIndex] < a[rightIndex])
                        merged[i] = a[leftIndex++];
                    else
                    {
                        merged[i] = a[rightIndex++];
                        inversionsCount += middle - leftIndex;
                    }
                }
            }

            for (int i = 0; i < merged.Length; i++)
                a[mergeFrom + i] = merged[i];
            return inversionsCount;
        }

        int CountInversionsImpl(int[] a, int sortFrom, int sortTo)
        {
            var len = sortTo - sortFrom;
            if (len <= 1)
                return 0;

            var middle = sortFrom + len / 2;

            var leftInversions = CountInversionsImpl(a, sortFrom, middle);
            var rightInversions = CountInversionsImpl(a, middle, sortTo);
            var splitInversions = CountSplitInversions(a, sortFrom, sortTo, middle);
            return leftInversions + rightInversions + splitInversions;
        }

        return CountInversionsImpl(a, 0, a.Length);
    }

    public static SquareMatrix MultiplyRec(SquareMatrix x, SquareMatrix y)
    {
        if (x.Size != y.Size)
            throw new ArgumentOutOfRangeException("Expect matrices of same size");

        if (x.Size == 1)
            return new SquareMatrix(1, new[] { x[0] * y[0] });

        var (a, b, c, d) = SquareMatrix.Divide(x);
        var (e, f, g, h) = SquareMatrix.Divide(y);

        var m1 = MultiplyRec(a, e) + MultiplyRec(b, g);
        var m2 = MultiplyRec(a, f) + MultiplyRec(b, h);
        var m3 = MultiplyRec(c, e) + MultiplyRec(d, g);
        var m4 = MultiplyRec(c, f) + MultiplyRec(d, h);
        var result = SquareMatrix.Combine(
            m1,
            m2,
            m3,
            m4
        );

        return result;
    }

    public static SquareMatrix Strassen(SquareMatrix x, SquareMatrix y)
    {
        if (x.Size != y.Size)
            throw new ArgumentOutOfRangeException("Expect matrices of same size");

        if (x.Size == 1)
            return new SquareMatrix(1, new[] { x[0] * y[0] });

        var (a, b, c, d) = SquareMatrix.Divide(x);
        var (e, f, g, h) = SquareMatrix.Divide(y);

        var p1 = Strassen(a, f - h);
        var p2 = Strassen(a + b, h);
        var p3 = Strassen(c + d, e);
        var p4 = Strassen(d, g - e);
        var p5 = Strassen(a + d, e + h);
        var p6 = Strassen(b - d, g + h);
        var p7 = Strassen(a - c, e + f);

        return SquareMatrix.Combine(p5 + p4 - p2 + p6, p1 + p2, p3 + p4, p1 + p5 - p3 - p7);
    }

    public static (Point p1, Point p2) ClosestPair(Point[] p)
    {
        (Point p1, Point p2, float m) BruteForce(Point[] points, int left, int right)
        {
            Point p1 = null;
            Point p2 = null;
            float bestMagnitude = float.MaxValue;
            for (int i = left; i < right; i++)
            for (int j = i + 1; j < right; j++)
            {
                var magnitude = Magnitude(points[i], points[j]);
                if (magnitude < bestMagnitude)
                {
                    bestMagnitude = magnitude;
                    p1 = points[i];
                    p2 = points[j];
                }
            }

            return (p1, p2, bestMagnitude);
        }
        
        (Point p1, Point p2, float m) SolveSplit(Point[] points, int left, int right, float magnitude)
        {
            var xMedian = points[points.Length / 2].X;
            var stripLeftBorder = xMedian - magnitude;
            var stripRightBorder = xMedian + magnitude;
            var strip = new Point[6];
            var stripLen = 0;
            for (int i = left; i < right && stripLen < 6; i++)
            {
                if (points[i].X > stripLeftBorder && points[i].X < stripRightBorder)
                    strip[stripLen++] = points[i];
            }

            return BruteForce(strip, 0, stripLen);
        }

        float Magnitude(Point p1, Point p2) => 
            (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);

        (Point p1, Point p2, float m) Solve(Point[] pXSorted, Point[] pYSorted, int left, int right)
        {
            var len = right - left;
            if (len <= 3)
                return BruteForce(pXSorted, left, right);

            var middle = left + len / 2;

            var (l1, l2, lM) = Solve(pXSorted, pYSorted, 0, middle);
            var (r1, r2, rM) = Solve(pXSorted, pYSorted, middle, right);
            var (s1, s2, sM) = SolveSplit(pXSorted, left, right, Math.Min(lM, rM));

            if (lM < rM && lM < sM)
                return (l1, l2, lM);
            if (rM < lM && rM < sM)
                return (r1, r2, rM);
            return (s1, s2, sM);
        }

        var pXSorted = new Point[p.Length];
        Array.Copy(p, pXSorted, p.Length);
        var pYSorted = new Point[p.Length];
        Array.Copy(p, pYSorted, p.Length);
        
        Array.Sort(pXSorted, (x, y) =>
        {
            if (x.X < y.X)
                return -1;
            if (x.X > y.X)
                return 1;
            return 0;
        });
        
        Array.Sort(pXSorted, (x, y) =>
        {
            if (x.Y < y.Y)
                return -1;
            if (x.Y > y.Y)
                return 1;
            return 0;
        });

        var closestPair = Solve(pXSorted, pYSorted, 0, p.Length);
        return (closestPair.p1, closestPair.p2);
    }
}

public class SquareMatrix
{
    private readonly int[,] _values;
    private int _size;

    public int Size => _size;
    public int Count => _size * _size;

    public int this[int index] => _values[index / _size, index % _size];
    public int this[int x, int y] => _values[x, y];

    public SquareMatrix(int size, params int[] values)
    {
        if (values.Length != size * size)
            throw new ArgumentOutOfRangeException(
                $"For matrix with size {size} expected {size * size} values, but got {values.Length}");

        _size = size;
        _values = new int[size, size];

        for (int i = 0; i < size; i++)
        for (int j = 0; j < size; j++)
            _values[i, j] = values[size * i + j];
    }

    public static SquareMatrix operator +(SquareMatrix a, SquareMatrix b)
    {
        if (a.Size != b.Size)
            throw new ArgumentOutOfRangeException("Cant sum matrices with different sizes");

        var result = new int[a.Count];
        for (int i = 0; i < a.Count; i++)
            result[i] = a[i] + b[i];

        try
        {
            return new SquareMatrix(a.Size, result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static SquareMatrix operator -(SquareMatrix a, SquareMatrix b)
    {
        if (a.Size != b.Size)
            throw new ArgumentOutOfRangeException("Cant sum matrices with different sizes");

        var result = new int[a.Count];
        for (int i = 0; i < a.Count; i++)
            result[i] = a[i] - b[i];

        return new SquareMatrix(a.Size, result);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not SquareMatrix other)
            return false;

        if (other.Size != Size)
            return false;

        for (int i = 0; i < Count; i++)
            if (this[i] != other[i])
                return false;

        return true;
    }

    public override string ToString()
    {
        string result = "(";
        foreach (var value in _values)
            result += $"{value} ";

        result = result.TrimEnd() + ")";

        return result;
    }

    public static (SquareMatrix a, SquareMatrix b, SquareMatrix c, SquareMatrix d) Divide(SquareMatrix m)
    {
        var smallCount = m.Count / 4;
        var smallSize = m.Size / 2;
        var a = new int[smallCount];
        var b = new int[smallCount];
        var c = new int[smallCount];
        var d = new int[smallCount];
        int aIndex, bIndex, cIndex, dIndex;
        aIndex = bIndex = cIndex = dIndex = 0;
        for (int mIndex = 0; mIndex < m.Count; mIndex++)
        {
            if (mIndex < m.Count / 2)
            {
                if (mIndex / smallSize % 2 == 0)
                    a[aIndex++] = m[mIndex];
                else
                    b[bIndex++] = m[mIndex];
            }
            else
            {
                if (mIndex / smallSize % 2 == 0)
                    c[cIndex++] = m[mIndex];
                else
                    d[dIndex++] = m[mIndex];
            }
        }

        return (
            new SquareMatrix(m.Size / 2, a),
            new SquareMatrix(m.Size / 2, b),
            new SquareMatrix(m.Size / 2, c),
            new SquareMatrix(m.Size / 2, d)
        );
    }

    public static SquareMatrix Combine(SquareMatrix a, SquareMatrix b, SquareMatrix c, SquareMatrix d)
    {
        var m = new int[a.Count * 4];
        int aIndex, bIndex, cIndex, dIndex;
        aIndex = bIndex = cIndex = dIndex = 0;
        for (int mIndex = 0; mIndex < m.Length; mIndex++)
        {
            if (mIndex < m.Length / 2)
            {
                if (mIndex / a.Size % 2 == 0)
                    m[mIndex] = a[aIndex++];
                else
                    m[mIndex] = b[bIndex++];
            }
            else
            {
                if (mIndex / a.Size % 2 == 0)
                    m[mIndex] = c[cIndex++];
                else
                    m[mIndex] = d[dIndex++];
            }
        }

        return new SquareMatrix(a.Size * 2, m);
    }
}

public class Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => 
        $"({X}, {Y})";
}