using System.Collections;

namespace JoyMap.Util
{
    public class LockedList<T> : IList<T>
    {
        private List<T> List { get; } = [];
        private object Sync { get; } = new();

        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(T item)
        {
            lock (Sync)
            {
                List.Add(item);
            }
        }
        public void Clear()
        {
            lock (Sync)
            {
                List.Clear();
            }
        }
        public bool Contains(T item)
        {
            lock (Sync)
            {
                return List.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (Sync)
            {
                List.CopyTo(array, arrayIndex);
            }
        }

        public IEnumerable<T1> Select<T1>(Func<T, T1> selector)
        {
            lock (Sync)
            {
                foreach (var item in List)
                {
                    yield return selector(item);
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (Sync)
            {
                // Create a copy to avoid issues with modification during enumeration
                return new List<T>(List).GetEnumerator();
            }
        }

        public int IndexOf(T item)
        {
            lock (Sync)
            {
                return List.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (Sync)
            {
                List.Insert(index, item);
            }
        }

        public bool Remove(T item)
        {
            lock (Sync)
            {
                return List.Remove(item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (Sync)
            { List.RemoveAt(index); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
