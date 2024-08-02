using System.Collections.Generic;

public class ElementQueue<T>
{
    private List<T> _list;

    public int Count => _list.Count;

    public ElementQueue(int capacity)
    {
        _list = new List<T>(capacity);
    }

    public void Enqueue(T item)
    {
        _list.Add(item);
    }

    public bool TryRemove(T item)
    {
        if (item == null)
            throw new System.ArgumentNullException();

        if (!_list.Contains(item))
            return false;

        return _list.Remove(item);
    }

    public T Dequeue()
    {
        if (_list.Count == 0)
        {
            throw new System.ArgumentOutOfRangeException();
        }

        var item = _list[0];
        _list.RemoveAt(0);

        return item;
    }

    public void Remove(T item)
    {
        if(item == null)
            throw new System.ArgumentNullException();

        _list.Remove(item);
    }

    public void Clear() => _list.Clear();
}
