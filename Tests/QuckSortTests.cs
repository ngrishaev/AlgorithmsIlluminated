using System;
using System.Linq;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class QuckSortTests
{
    [TestCase(new []{1})]
    [TestCase(new []{3, 1})]
    [TestCase(new []{3, 1, 2})]
    [TestCase(new []{3, 1, 2, 0})]
    [TestCase(new [] { 3, 1, 2, 0, 28, 17, 33, 11, 57, 45, 117, 4, 357, 15, 9 })]
    public void QuickSort(int[] array)
    {
        var toSort = new int[array.Length];
        Array.Copy(array, toSort, array.Length);
        
        AlgorithmsIlluminated_I.QuickSort.QuickSort.QuickSorting(toSort);
        
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