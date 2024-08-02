using System.Collections.Generic;

public class CCharpPooler<T> where T : new() {
    private Queue<T> schedule;

    public CCharpPooler(int capacity)
    {
        schedule = new Queue<T>(capacity);
    }

    public T Pool()
    {
        if (schedule.Count == 0)
        {
            return new();
        }

        var unit = schedule.Dequeue();

        return unit;
    }

    public void Push(T unit)
    {
        schedule.Enqueue(unit);
    }
}