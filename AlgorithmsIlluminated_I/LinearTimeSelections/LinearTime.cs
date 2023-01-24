namespace AlgorithmsIlluminated_I.LinearTimeSelections;

public static class LinearTime
{
    public static int RSelect(int[] input, int ithOrder)
    {
        void Swap(int[] array, int index1, int index2) => (array[index1], array[index2]) = (array[index2], array[index1]);
        
        int ChoosePivot(int[] array, int left, int right)
        {
            int Lazy(int[] array, int left, int right) => left;

            return Lazy(array, left, right);
        }
        
        int Partition(int[] array, int left, int right)
        {
            var pivotValue = array[left];
            var pivotBorder = left + 1;

            for (int i = left + 1; i < right; i++)
                if (array[i] < pivotValue)
                    Swap(array, i, pivotBorder++);
            
            Swap(array, left, --pivotBorder);
            return pivotBorder;
        }
        
        int RSelectImpl(int[] input, int ithOrder, int left, int right)
        {
            if (left >= right - 1)
                return input[left];
            
            int pivot = ChoosePivot(input, left, right);
            Swap(input, left, pivot);
            int newPivot = Partition(input, left, right);

            if (newPivot == ithOrder)
                return input[newPivot];
            else if (newPivot > ithOrder)
                return RSelectImpl(input, ithOrder, left, newPivot);
            else
                return RSelectImpl(input, ithOrder - newPivot, newPivot + 1, right);
        }
        return RSelectImpl(input, ithOrder, 0, input.Length);
    }
}