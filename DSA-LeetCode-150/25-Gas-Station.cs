public static class GasStation
{
    public static int CanCompleteCircuit(int[] gas, int[] cost)
    {
        int total = 0;
        int tank = 0;
        int start = 0;

        for (int i = 0; i < gas.Length; i++)
        {
            int diff = gas[i] - cost[i];
            total += diff;
            tank += diff;

            if (tank < 0)
            {
                start = i + 1;
                tank = 0;
            }
        }

        return total >= 0 ? start : -1;
    }
}
