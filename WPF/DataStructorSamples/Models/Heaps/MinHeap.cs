using System;
using System.Collections.Generic;

namespace DataStructorSamples.Models.Heaps
{
    public class MinHeap
    {
        public MinHeap()
        {
            _heaps.Add(Int32.MinValue);
        }

        public List<int> _heaps = new List<int>();

        public Stack<int> _stacks = new Stack<int>();

        public bool IsEmpty => _heaps.Count == 1;
        private int _startIndex = 1;

        public int Index => _heaps.Count;

        public int TailIndex => _heaps.Count - 1;

        public void Add(int val)
        {
            _stacks.Push(val);
            int index = Index;
            int parent = index / 2;
            _heaps.Add(val);

            while (_heaps[parent] > _heaps[index])
            {
                _heaps[parent] = _heaps[parent] + _heaps[index];
                _heaps[index] = _heaps[parent] - _heaps[index];
                _heaps[parent] = _heaps[parent] - _heaps[index];
                index = parent;
                parent /= 2;
            }
        }

        public int Del()
        {
            int ret = _heaps[_startIndex];

            _heaps[_startIndex] = _heaps[_startIndex] + _heaps[TailIndex];
            _heaps[TailIndex] = _heaps[_startIndex] - _heaps[TailIndex];
            _heaps[_startIndex] = _heaps[_startIndex] - _heaps[TailIndex];

            _heaps.RemoveAt(TailIndex);

            int parentIndex = _startIndex;
            int son = 0;

            while (true)
            {
                son = parentIndex * 2;
                if (son < Index)
                {
                    if (son + 1 < Index)
                    {
                        if (_heaps[son] > _heaps[son + 1])
                        {
                            son = son + 1;
                        }
                    }

                    if (_heaps[son] < _heaps[parentIndex])
                    {
                        _heaps[son] = _heaps[son] + _heaps[parentIndex];
                        _heaps[parentIndex] = _heaps[son] - _heaps[parentIndex];
                        _heaps[son] = _heaps[son] - _heaps[parentIndex];
                        parentIndex = son;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }

            }

            return ret;
        }

        public int GetRoot()
        {
            return _heaps[_startIndex];
        }

        public int Top()
        {
            return _heaps[TailIndex];
        }

        public int Pop()
        {
            int ret = _stacks.Pop();
            int index = Index - 1;
            int flag = 0;
            while (index >= 1)
            {
                if (ret != _heaps[index])
                {
                    flag = index % 2;
                    index /= 2;
                }
                else
                {
                    break;
                }
            }

            if (index >= 1)
            {
                while (index < Index)
                {
                    int son = index * 2 + flag;

                    if (son < Index)
                    {
                        _heaps[index] = _heaps[son];
                        index = son;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            _heaps.RemoveAt(Index - 1);

            return ret;
        }
    }
}
