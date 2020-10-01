
/* This class is an implementation of Heap Data Structure. It support both Min/Max heap. Min/Max can be initialized at the constructor level.
   It only supports integer data type and can be modified to support generic types.*/

namespace HeapDataStructure.Heap
{
    using System;

    /// <summary>
    /// Heap Data Structure class for Integer dataType. This class support both Min and Max Heap.
    /// </summary>
    public class Heap
    {
        /// <summary>
        /// If capacity is not defined, default capacity is set to this value.
        /// </summary>
        private const int DefaultMax = 100;

        /// <summary>
        /// A delegate to support Min/Max heap.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private delegate bool Compare(int a, int b);

        /// <summary>
        /// Data container of Heap.
        /// </summary>
        private int[] _data;

        /// <summary>
        /// Capacity of heap.
        /// </summary>
        private int _capacity;

        /// <summary>
        /// Size of heap at any given moment.
        /// </summary>
        private int _heapSize;

        /// <summary>
        /// Min/Max operation definer. default set to Max Heap.
        /// </summary>
        private readonly Compare _operator = (a, b) => a > b;

        /// <summary>
        /// Default Constructor. Initialize a heap with default capacity and no data.
        /// </summary>
        public Heap()
        {
            if (_capacity != 0)
            {
                _capacity = DefaultMax;
                _data = new int[_capacity];
            }
        }

        /// <summary>
        /// Creates a Heap of specified capacity and type.
        /// </summary>
        /// <param name="capacity">Default capacity of heap.</param>
        /// <param name="heapType">Type of Heap. Min or Max.</param>
        public Heap(int capacity, HeapType heapType)
        {
            _capacity = capacity;
            _data = new int[capacity];
            if (heapType == HeapType.MaxHeap)
            {
                _operator = (a, b) => a > b;
            }
            else
            {
                _operator = (a, b) => a < b;
            }
        }

        /// <summary>
        /// Creates an heap object with given capacity, data and type.
        /// </summary>
        /// <param name="capacity">Default capacity of heap.</param>
        /// <param name="data">Input data to heap.</param>
        /// <param name="heapType">Type of Heap. Min or Max.</param>
        public Heap(int capacity, int[] data, HeapType heapType) : this(capacity, heapType)
        {
            if (data == null || data.Length > capacity)
            {
                throw new Exception("Data length must be less than heap capacity");
            }

            BuildHeap(data);
        }

        /// <summary>
        /// Extracts(removes) top data of heap.
        /// </summary>
        /// <returns>Heap Top(Min/Max) data.</returns>
        public int ExtractTop()
        {
            if(_heapSize == 0)
            {
                throw new Exception("Heap Empty. Cannot extract");
            }

            var result = _data [0];
            _data [0] = _data [_heapSize - 1];
            _heapSize--;
            TopDown(0);
            return result;
        }

        /// <summary>
        /// Gets Top data without removing it.
        /// </summary>
        public int Peek
        {
            get
            {
                if (_heapSize == 0)
                {
                    return int.MinValue;
                }

                return _data[0];
            }
        }

        /// <summary>
        /// Inserts a new element into Heap.
        /// </summary>
        /// <param name="element"></param>
        public void Insert(int element)
        {
            if (_heapSize == _capacity)
            {
                ResizeHeap();
            }

            _data[_heapSize] = element;
            _heapSize++;
            BottomUp(_heapSize - 1);
        }

        /// <summary>
        /// Gets current heap size.
        /// </summary>
        public int HeapSize => _heapSize;

        /// <summary>
        /// Gets Current heap data.
        /// </summary>
        public int[] HeapData
        {
            get
            {
                var result = new int[_heapSize];
                for (int i = 0; i < _heapSize; i++)
                {
                    result[i] = _data[i];
                }

                return result;
            }
        }

        /// <summary>
        /// Increases capacity of Heap.
        /// </summary>
        private void ResizeHeap()
        {
            _capacity = _capacity * 2;
            var tempData = new int[_capacity];
            Array.Copy(_data, 0, tempData, 0, _data.Length);
            _data = tempData;
        }

        /// <summary>
        /// Builds an Heap.
        /// </summary>
        /// <param name="data"></param>
        private void BuildHeap(int[] data)
        {
            Array.Copy(data, 0, _data, 0, data.Length);
            _heapSize = data.Length;
            for (var iterator = (data.Length-1)/2; iterator >=0; iterator--)
            {
                TopDown(iterator);
            }
        }

        /// <summary>
        /// Insures Heap property is maintain using Top Down approach.
        /// </summary>
        /// <param name="position"></param>
        private void TopDown(int position)
        {
            if (_heapSize == 0)
            {
                return;
            }
            if (position >= _heapSize)
            {
                throw new Exception("Index out of bound");
            }

            var left = 2 * position + 1;
            var right = 2 * position + 2;
            var max = position;
            if (left < _heapSize &&  _operator(_data[left],_data[max]))
            {
                max = left;
            }

            if (right < _heapSize && _operator(_data[right], _data[max]))
            {
                max = right;
            }

            if (max != position)
            {
                var temp = _data[position];
                _data[position] = _data[max];
                _data[max] = temp;
                TopDown(max);
            }
        }

        /// <summary>
        /// Insures Heap property is maintain using Bottom Up approach.
        /// </summary>
        /// <param name="position"></param>
        private void BottomUp(int position)
        {
            if (position > _heapSize)
            {
                throw new Exception("Index out of Bound");
            }

            if (position == 0)
            {
                return;
            }

            var parent = (position - 1) / 2;
            if (!_operator(_data[parent], _data[position]))
            {
                var temp = _data[parent];
                _data[parent] = _data[position];
                _data[position] = temp;
                BottomUp(parent);
            }
        }
    }
}
