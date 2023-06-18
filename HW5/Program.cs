
using System;
using System.Collections;
using System.Collections.Generic;

public static class Program
{

    public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (T item in source)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }
    public static IEnumerable<T> Skip<T>(this IEnumerable<T> source, int count)
    {
        int skipped = 0;
        foreach (T item in source)
        {
            if (skipped < count)
            {
                skipped++;
            }
            else
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<T> SkipWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        bool skip = true;
        foreach (T item in source)
        {
            if (skip && predicate(item))
            {
                continue;
            }
            skip = false;
            yield return item;
        }
    }
    public static IEnumerable<T> Take<T>(this IEnumerable<T> source, int count)
    {
        int taken = 0;
        foreach (T item in source)
        {
            if (taken < count)
            {
                yield return item;
                taken++;
            }
            else
            {
                yield break;
            }
        }
    }

    public static IEnumerable<T> TakeWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (T item in source)
        {
            if (predicate(item))
            {
                yield return item;
            }
            else
            {
                yield break;
            }
        }
    }
    public static T First<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (T item in source)
        {
            if (predicate(item))
            {
                return item;
            }
        }
        throw new InvalidOperationException("Sequence contains no matching element.");
    }

    public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (T item in source)
        {
            if (predicate(item))
            {
                return item;
            }
        }
        return default(T);
    }
    public static T Last<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        T lastMatch = default(T);
        bool foundMatch = false;
        foreach (T item in source)
        {
            if (predicate(item))
            {
                lastMatch = item;
                foundMatch = true;
            }
        }
        if (!foundMatch)
        {
            throw new InvalidOperationException("Sequence contains no matching element.");
        }
        return lastMatch;
    }

    public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        T lastMatch = default(T);
        foreach (T item in source)
        {
            if (predicate(item))
            {
                lastMatch = item;
            }
        }
        return lastMatch;
    }
    public static IEnumerable<TResult> Select<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
    {
        foreach (T item in source)
        {
            yield return selector(item);
        }
    }

    public static IEnumerable<TResult> SelectMany<T, TResult>(this IEnumerable<T> source, Func<T, IEnumerable<TResult>> selector)
    {
        foreach (T item in source)
        {
            foreach (TResult result in selector(item))
            {
                yield return result;
            }
        }
    }
    public static bool All<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (T item in source)
        {
            if (!predicate(item))
            {
                return false;
            }
        }
        return true;
    }

    public static bool Any<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (T item in source)
        {
            if (predicate(item))
            {
                return true;
            }
        }
        return false;
    }
    public static T[] ToArray<T>(this IEnumerable<T> source)
    {
        return new List<T>(source).ToArray();
    }

    public static List<T> ToList<T>(this IEnumerable<T> source)
    {
        return new List<T>(source);
    }

    public class TreeNode<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }

        public TreeNode(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public static void Sort<T>(this IList<T> list) where T : IComparable<T>
    {
        for (int i = 1; i < list.Count; i++)
        {
            T key = list[i];
            int j = i - 1;

            while (j >= 0 && list[j].CompareTo(key) > 0)
            {
                list[j + 1] = list[j];
                j--;
            }

            list[j + 1] = key;
        }
    }
    public static int BinarySearch<T>(this IList<T> list, T item) where T : IComparable<T>
    {
        int left = 0;
        int right = list.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int comparisonResult = list[mid].CompareTo(item);

            if (comparisonResult == 0)
            {
                return mid;
            }
            else if (comparisonResult < 0)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return -1;
    }
    public class ObservableList<T> : IList<T>
    {
        private readonly List<T> list;
        public event Action<T> ItemAdded;
        public event Action<T> ItemRemoved;

        public ObservableList()
        {
            list = new List<T>();
        }


        public void Add(T item)
        {
            list.Add(item);
            ItemAdded?.Invoke(item);
        }

        public bool Remove(T item)
        {
            bool removed = list.Remove(item);
            if (removed)
            {
                ItemRemoved?.Invoke(item);
            }
            return removed;
        }


        public IEnumerable<T> Filter(Func<T, bool> predicate)
        {
            foreach (T item in list)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<T> Skip(int count)
        {
            for (int i = count; i < list.Count; i++)
            {
                yield return list[i];
            }
        }

        public IEnumerable<T> Take(int count)
        {
            for (int i = 0; i < count && i < list.Count; i++)
            {
                yield return list[i];
            }
        }
    }

    public class PriorityQueue<T, TKey> : IEnumerable<T>
    {
        private readonly List<T> items;
        private readonly Func<T, TKey> prioritySelector;

        public PriorityQueue(Func<T, TKey> prioritySelector)
        {
            this.prioritySelector = prioritySelector;
            items = new List<T>();
        }

        public void Enqueue(T item)
        {
            items.Add(item);
            items.Sort(Comparer<T>.Create((x, y) =>
            {
                TKey priorityX = prioritySelector(x);
                TKey priorityY = prioritySelector(y);
                return Comparer<TKey>.Default.Compare(priorityX, priorityY);
            }));
        }

        public T Dequeue()
        {
            if (items.Count == 0)
            {
                throw new InvalidOperationException("PriorityQueue is empty");
            }

            T item = items[0];
            items.RemoveAt(0);
            return item;
        }

        public T Peek()
        {
            if (items.Count == 0)
            {
                throw new InvalidOperationException("PriorityQueue is empty");
            }

            return items[0];
        }

        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            foreach (T item in items)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public T FirstOrDefault()
        {
            if (items.Count > 0)
            {
                return items[0];
            }
            return default(T);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class LinqExtensions
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Skip<T>(this IEnumerable<T> source, int count)
        {
            int skipped = 0;
            foreach (T item in source)
            {
                if (skipped < count)
                {
                    skipped++;
                    continue;
                }

                yield return item;
            }
        }

        public static IEnumerable<T> Take<T>(this IEnumerable<T> source, int count)
        {
            int taken = 0;
            foreach (T item in source)
            {
                if (taken < count)
                {
                    taken++;
                    yield return item;
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}



public interface IEnumerable<T>
{
    IEnumerator<T> GetEnumerator();
}

public interface ICollection<T> : IEnumerable<T>
{
    int Count { get; }
    bool IsReadOnly { get; }

    void Add(T item);
    void Clear();
    bool Contains(T item);
    void CopyTo(T[] array, int arrayIndex);
    bool Remove(T item);
}

public interface IList<T> : ICollection<T>
{
    T this[int index] { get; set; }
    int IndexOf(T item);
    void Insert(int index, T item);
    void RemoveAt(int index);
}

public static class LinqExtensions
{
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (T item in source)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<T> Skip<T>(this IEnumerable<T> source, int count)
    {
        int skipped = 0;

        foreach (T item in source)
        {
            if (skipped < count)
            {
                skipped++;
                continue;
            }

            yield return item;
        }
    }

}


