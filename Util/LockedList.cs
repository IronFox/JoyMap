using System.Collections;

namespace JoyMap.Util
{
    public class LockedList<T> : IList<T>
    {
        private readonly List<T> list = [];
        private readonly object sync = new();

        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(T item)
        {
            lock (sync)
            {
                list.Add(item);
            }
        }
        public void Clear()
        {
            lock (sync)
            {
                list.Clear();
            }
        }
        public bool Contains(T item)
        {
            lock (sync)
            {
                return list.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (sync)
            {
                list.CopyTo(array, arrayIndex);
            }
        }

        public IEnumerable<T1> Select<T1>(Func<T, T1> selector)
        {
            lock (sync)
            {
                foreach (var item in list)
                {
                    yield return selector(item);
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (sync)
            {
                // Create a copy to avoid issues with modification during enumeration
                return new List<T>(list).GetEnumerator();
            }
        }

        public int IndexOf(T item)
        {
            lock (sync)
            {
                return list.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (sync)
            {
                list.Insert(index, item);
            }
        }

        public bool Remove(T item)
        {
            lock (sync)
            {
                return list.Remove(item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (sync)
            { list.RemoveAt(index); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
