using System.Collections.Generic;
using UnityEngine;

public static class DictExtention
{
    public static void AddOrReplace<K,V>(this Dictionary<K,V> dict, K key, V value)
    {
        lock (dict)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
                return;
            }

            dict.Add(key, value);
        }
    }

    public static void RemoveWhenHas<K, V>(this Dictionary<K, V> dict, K key)
    {
        lock (dict)
        {
            if (!dict.ContainsKey(key))
            {
                return;
            }

            dict.Remove(key);
        }
    }
}