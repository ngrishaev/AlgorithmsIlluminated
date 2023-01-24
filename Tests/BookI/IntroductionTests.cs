using System;
using System.Linq;
using AlgorithmsIlluminated_I.Introduction;
using NUnit.Framework;

namespace Tests;

public class IntroductionTests
{
    [TestCase(0, 1, 0)]
    [TestCase(1, 0, 0)]
    [TestCase(1, 1, 1)]
    [TestCase(10, 10, 100)]
    [TestCase(12, 12, 12 * 12)]
    [TestCase(1852, 9452, 1852 * 9452)]
    [TestCase(1000, 1000, 1000*1000)]
    public void RecIntMult(int x, int y, int expected)
    {
        var output = Introduction.RecIntMult(x, y);
        
        Assert.IsTrue(output == expected, $"Expected {expected}, but got {output}");
    }
    
    [TestCase("0", "1", "0")]
    [TestCase("1", "0", "0")]
    [TestCase("1", "1", "1")]
    [TestCase("9", "13", "117")]
    [TestCase("10", "10", "100")]
    [TestCase("12", "12", "144")]
    [TestCase("18", "94", "1692")]
    [TestCase("52", "52", "2704")]
    [TestCase("70", "46", "3220")]
    [TestCase("70", "146", "10220")]
    [TestCase("1852", "9452", "17505104")]
    [TestCase("1000", "1000", "1000000")]
    [TestCase(
        "3141592653589793238462643383279502884197169399375105820974944592",
        "2718281828459045235360287471352662497757247093699959574966967627",
        "8539734222673567065463550869546574495034888535765114961879601127067743044893204848617875072216249073013374895871952806582723184")]
    public void Karatsuba(string x, string y, string expected)
    {
        var output = Introduction.Karatsuba(x, y);
        
        Assert.IsTrue(output == expected, $"Expected {expected}, but got {output}");
    }

    [TestCase(new[] { 1 })]
    [TestCase(new[] { 3, 1 })]
    [TestCase(new[] { 3, 1, 2 })]
    [TestCase(new[] { 3, 1, 2, 0 })]
    [TestCase(new[] { 3, 1, 2, 0, 28, 17, 33, 11, 57, 45, 117, 4, 357, 15, 9 })]
    public void MergeSort(int[] array)
    {
        var toSort = new int[array.Length];
        Array.Copy(array, toSort, array.Length);

        Introduction.MergeSort(toSort);

        Assert.IsTrue(
            Helpers.IsSorted(toSort),
            $"Array {array} is not sorted"
        );
        Assert.IsTrue(
            Helpers.SameElements(array, toSort),
            $"Array {ArrayToString(array)} is not contain same elements as {ArrayToString(toSort)}"
        );
    }

    public string ArrayToString(int[] array)
    {
        return array.Aggregate("[", (s, i) => s + $"{i}, ") + "]";
    }
}