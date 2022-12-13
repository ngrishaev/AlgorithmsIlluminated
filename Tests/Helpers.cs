using System.Collections.Generic;

namespace Tests;

public class Helpers
{
    public static bool IsSorted(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
            if (array[i] < array[i - 1])
                return false;
        return true;
    }
    
    public static bool SameElements(int[] first, int[] second)
    {
        if (first.Length != second.Length)
            return false;
        
        Dictionary<int, int> countTable = new Dictionary<int, int>();
        
        foreach (var val in first)
            if (countTable.ContainsKey(val))
                countTable[val]++;
            else
                countTable[val] = 1;
        
        foreach (var val in second)
            if (countTable.ContainsKey(val))
                countTable[val]--;
            else
                return false;

        foreach (var kvp in countTable)
            if (kvp.Value != 0)
                return false;

        return true;
    }
}