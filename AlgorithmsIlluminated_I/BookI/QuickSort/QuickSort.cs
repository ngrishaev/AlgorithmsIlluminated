namespace AlgorithmsIlluminated_I.BookI.QuickSort;

public static class QuickSort
{
    public static void QuickSorting(int[] input)
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

        void SortImplementation(int[] input, int left, int right)
        {
            if(left >= right - 1)
                return;

            int pivot = ChoosePivot(input, left, right);

            Swap(input, left, pivot);
            int newPivot = Partition(input, left, right);
            
            SortImplementation(input, left, newPivot);
            SortImplementation(input, newPivot + 1, right);
        }

        SortImplementation(input, 0, input.Length);
    }
    
}