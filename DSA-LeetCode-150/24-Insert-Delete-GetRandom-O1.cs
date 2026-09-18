public class RandomizedSet
{
    private readonly List<int> values = new();
    private readonly Dictionary<int, int> indexByValue = new();
    private readonly Random random = new();

    public bool Insert(int val)
    {
        if (indexByValue.ContainsKey(val))
            return false;

        indexByValue[val] = values.Count;
        values.Add(val);
        return true;
    }

    public bool Remove(int val)
    {
        if (!indexByValue.TryGetValue(val, out int index))
            return false;

        int lastIndex = values.Count - 1;
        int lastValue = values[lastIndex];

        values[index] = lastValue;
        indexByValue[lastValue] = index;

        values.RemoveAt(lastIndex);
        indexByValue.Remove(val);
        return true;
    }

    public int GetRandom()
    {
        return values[random.Next(values.Count)];
    }
}
