using System.Collections;

namespace Task5.Tests;

public class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    where TKey : notnull
{
    private readonly List<KeyValuePair<TKey, TValue>> _items;
    private readonly Dictionary<TKey, int> _indexMap;

    public OrderedDictionary()
        : this(EqualityComparer<TKey>.Default) { }

    public OrderedDictionary(IEqualityComparer<TKey>? comparer)
    {
        comparer ??= EqualityComparer<TKey>.Default;
        _items = [];
        _indexMap = new Dictionary<TKey, int>(comparer);
    }

    public int Count => _items.Count;
    public bool IsReadOnly => false;

    public TValue this[TKey key]
    {
        get =>
            _indexMap.TryGetValue(key, out int index)
                ? _items[index].Value
                : throw new KeyNotFoundException($"Key '{key}' not found");
        set
        {
            if (_indexMap.TryGetValue(key, out int index))
            {
                _items[index] = new KeyValuePair<TKey, TValue>(key, value);
            }
            else
            {
                Add(key, value);
            }
        }
    }

    public KeyValuePair<TKey, TValue> this[int index] => _items[index];

    public ICollection<TKey> Keys => GetKeys();
    public ICollection<TValue> Values => GetValues();

    private List<TKey> GetKeys()
    {
        return _items.Select(kvp => kvp.Key).ToList();
    }

    private List<TValue> GetValues()
    {
        return _items.Select(kvp => kvp.Value).ToList();
    }

    public void Add(TKey key, TValue value)
    {
        if (_indexMap.ContainsKey(key))
        {
            throw new ArgumentException($"Key '{key}' already exists");
        }

        _items.Add(new KeyValuePair<TKey, TValue>(key, value));
        _indexMap[key] = _items.Count - 1;
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }

    public bool Remove(TKey key)
    {
        if (!_indexMap.TryGetValue(key, out int index))
        {
            return false;
        }

        _items.RemoveAt(index);
        _ = _indexMap.Remove(key);

        for (int i = index; i < _items.Count; i++)
        {
            _indexMap[_items[i].Key] = i;
        }

        return true;
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        return TryGetValue(item.Key, out TValue? value)
            && EqualityComparer<TValue>.Default.Equals(value, item.Value)
            && Remove(item.Key);
    }

    public bool ContainsKey(TKey key)
    {
        return _indexMap.ContainsKey(key);
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return TryGetValue(item.Key, out TValue? value)
            && EqualityComparer<TValue>.Default.Equals(value, item.Value);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        if (_indexMap.TryGetValue(key, out int index))
        {
            value = _items[index].Value!;
            return true;
        }
        value = default!;
        return false;
    }

    public int IndexOf(TKey key)
    {
        return _indexMap.TryGetValue(key, out int index) ? index : -1;
    }

    public void Clear()
    {
        _items.Clear();
        _indexMap.Clear();
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
