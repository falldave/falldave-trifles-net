using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FallDave.TriflesTests
{
    // Implementation of IList<T> that hides the true class of another IList<T>.
    internal class WrappedList<T> : IList<T>
    {
        private IList<T> source;

        internal WrappedList(IList<T> source)
        {
            if (source == null) { throw new ArgumentNullException("source"); }
            this.source = source;
        }

        public T this[int index]
        {
            get { return source[index]; }
            set { source[index] = value; }
        }

        public int Count
        {
            get { return source.Count; }
        }

        public bool IsReadOnly
        {
            get { return source.IsReadOnly; }
        }

        public void Add(T item)
        {
            source.Add(item);
        }

        public void Clear()
        {
            source.Clear();
        }

        public bool Contains(T item)
        {
            return source.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            source.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in source)
            {
                yield return item;
            }
        }

        public int IndexOf(T item)
        {
            return source.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            source.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return source.Remove(item);
        }

        public void RemoveAt(int index)
        {
            source.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}