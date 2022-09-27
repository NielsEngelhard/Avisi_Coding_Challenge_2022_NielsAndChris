namespace TestApp;

public static class FactorialCalculator
{
    public static IList<IList<int>> Permute(int[] nums)
    {
        var list = new List<IList<int>>();
        return DoPermute(nums, 0, nums.Length - 1, list);
    }

    public static IList<IList<int>> DoPermute(int[] nums, int start, int end, IList<IList<int>> list)
    {
        if (start == end)
        {
            // We have one of our possible n! solutions,
            // add it to the list.
            list.Add(new List<int>(nums));
        }
        else
        {
            for (var i = start; i <= end; i++)
            {
                Swap(ref nums[start], ref nums[i]);
                DoPermute(nums, start + 1, end, list);
                Swap(ref nums[start], ref nums[i]);
            }
        }

        return list;
    }

    public static void Swap(ref int a, ref int b)
    {
        var temp = a;
        a = b;
        b = temp;
    }

    public static void PrintResult(IList<IList<int>> lists)
    {
        foreach (var list in lists)
        {
            Console.WriteLine($"    {string.Join(',', list)}|");
        }
    }
}