using AlgorithmsIlluminated_I.LinearTimeSelections;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class SelectionTests
{
    [TestCase(new [] { 3, 1, 2, 0, 28, 17, 33, 11, 57, 45, 117, 4, 357, 15, 9 }, 0, 0)]
    [TestCase(new [] { 3,}, 0, 3)]
    [TestCase(new [] { 3, 1, 2, 0, 28, 17, 33, 11, 57, 45, 117, 4, 357, 15, 9 }, 3, 3)]
    public void RSelectionTest(int[] array, int ithPosition, int expectedValue)
    {
        var result = LinearTime.RSelect(array, ithPosition);
        
        Assert.IsTrue(result == expectedValue);
    }
}