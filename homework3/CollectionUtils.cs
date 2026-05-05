using System;
using System.Collections.Generic;

public static class CollectionUtils
{
    public static List<T> Distinct<T>(List<T> source)
    {
        List<T> result = new List<T>();
        HashSet<T> seen = new HashSet<T>();

        foreach (T item in source)
        {
            if (seen.Add(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    public static Dictionary<TKey, List<TValue>> GroupBy<TValue, TKey>(
        List<TValue> source,
        Func<TValue, TKey> keySelector) where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> result = new Dictionary<TKey, List<TValue>>();

        foreach (TValue item in source)
        {
            TKey key = keySelector(item);

            if (!result.ContainsKey(key))
            {
                result[key] = new List<TValue>();
            }

            result[key].Add(item);
        }

        return result;
    }

    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(
        Dictionary<TKey, TValue> first,
        Dictionary<TKey, TValue> second,
        Func<TValue, TValue, TValue> conflictResolver) where TKey : notnull
    {
        Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>(first);

        foreach (KeyValuePair<TKey, TValue> pair in second)
        {
            if (result.ContainsKey(pair.Key))
            {
                result[pair.Key] = conflictResolver(result[pair.Key], pair.Value);
            }
            else
            {
                result[pair.Key] = pair.Value;
            }
        }

        return result;
    }

    public static T MaxBy<T, TKey>(List<T> source, Func<T, TKey> selector)
        where TKey : IComparable<TKey>
    {
        if (source.Count == 0)
        {
            throw new InvalidOperationException("Collection is empty");
        }

        T maxItem = source[0];
        TKey maxValue = selector(source[0]);

        for (int i = 1; i < source.Count; i++)
        {
            TKey currentValue = selector(source[i]);

            if (currentValue.CompareTo(maxValue) > 0)
            {
                maxValue = currentValue;
                maxItem = source[i];
            }
        }

        return maxItem;
    }
}
