
namespace HeapDataStructure.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T>
    {
        private readonly List<T> _list;
        private readonly IComparer<T> _comparer;

        public PriorityQueue(IComparer<T> comparer = null)
        {
            _comparer = comparer ?? Comparer<T>.Default;
            _list = new List<T>();
        }

        public PriorityQueue(IEnumerable<T> items, IComparer<T> comparer = null)
            : this(comparer)
        {
            foreach (var i in items)
                Add(i);
        }

        public void Add(T x)
        {
            _list.Add(x);
            var child = Count - 1;
            while (child > 0)
            {
                int parent = (child - 1) / 2;// parent index
                if (_comparer.Compare(_list[parent], x) <= 0) break;
                _list[child] = _list[parent];
                child = parent;
            }
            if (Count > 0) _list[child] = x;
        }

        public T Peek() => _list[0];

        public T Poll()
        {
            var ret = Peek();
            var root = _list[Count - 1];
            _list.RemoveAt(Count - 1);
            var i = 0;
            while (i * 2 + 1 < Count)
            {
                var left = 2 * i + 1; //left child
                if (left > Count) break; // no children so we're done
                var right = 2 * i + 2; // right child
                var c = right < Count && _comparer.Compare(_list[right], _list[left]) < 0 ? right : left;
                if (_comparer.Compare(_list[c], root) >= 0) break;
                _list[i] = _list[c];
                i = c;
            }

            if (Count > 0) _list[i] = root;
            return ret;
        }

        public int Count => _list.Count;
        public void DisplayHeap() => _list.ForEach(x => Console.WriteLine(x));
    }
}
