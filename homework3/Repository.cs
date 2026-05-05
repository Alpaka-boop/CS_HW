using System;
using System.Collections.Generic;

public class Repository<T> where T : IEntity
{
    private readonly Dictionary<int, T> items = new Dictionary<int, T>();

    public int Count => items.Count;

    public void Add(T item)
    {
        if (items.ContainsKey(item.Id))
        {
            throw new InvalidOperationException("Item with this Id already exists");
        }

        items[item.Id] = item;
    }

    public bool Remove(int id)
    {
        return items.Remove(id);
    }

    public T? GetById(int id)
    {
        if (items.TryGetValue(id, out T? item))
        {
            return item;
        }

        return default;
    }

    public IReadOnlyList<T> GetAll()
    {
        return new List<T>(items.Values);
    }

    public IReadOnlyList<T> Find(Predicate<T> predicate)
    {
        List<T> result = new List<T>();

        foreach (T item in items.Values)
        {
            if (predicate(item))
            {
                result.Add(item);
            }
        }

        return result;
    }
}
