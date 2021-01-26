namespace _02.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class PriorityQueue<TPriority, TValue>
    {
        private List<KeyValuePair<TPriority, TValue>> _baseHeap;
        private IComparer<TPriority> _comparer;

        public PriorityQueue()
            : this(Comparer<TPriority>.Default)
        {
        }

        public PriorityQueue(IComparer<TPriority> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException();

            _baseHeap = new List<KeyValuePair<TPriority, TValue>>();
            _comparer = comparer;
        }

        public int Count => this._baseHeap.Count;

        public List<TValue> GetAll()
        {
            var result = new List<TValue>();

            foreach (var kvp in _baseHeap)
            {
                result.Add(kvp.Value);
            }

            return result;
        }

        public TValue FindByPriority(TPriority priority)
        {
            var result = _baseHeap
                .FirstOrDefault(x => _comparer.Compare(x.Key, priority) == 0)
                .Value;

            return result;
        }

        public TValue FindByIndex(int index)
        {
            return _baseHeap[index].Value;
        }

        public void Enqueue(TPriority priority, TValue value)
        {
            Insert(priority, value);
        }

        private void Insert(TPriority priority, TValue value)
        {
            KeyValuePair<TPriority, TValue> val =
                new KeyValuePair<TPriority, TValue>(priority, value);
            _baseHeap.Add(val);

            // heapify after insert, from end to beginning
            HeapifyFromEndToBeginning(_baseHeap.Count - 1);
        }

        private int HeapifyFromEndToBeginning(int pos)
        {
            if (pos >= _baseHeap.Count) return -1;

            // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];

            while (pos > 0)
            {
                int parentPos = (pos - 1) / 2;
                if (_comparer.Compare(_baseHeap[parentPos].Key, _baseHeap[pos].Key) > 0)
                {
                    ExchangeElements(parentPos, pos);
                    pos = parentPos;
                }
                else break;
            }
            return pos;
        }

        private void ExchangeElements(int pos1, int pos2)
        {
            KeyValuePair<TPriority, TValue> val = _baseHeap[pos1];
            _baseHeap[pos1] = _baseHeap[pos2];
            _baseHeap[pos2] = val;
        }

        public TValue DequeueValue()
        {
            return Dequeue().Value;
        }

        public KeyValuePair<TPriority, TValue> Dequeue()
        {
            if (!IsEmpty)
            {
                KeyValuePair<TPriority, TValue> result = _baseHeap[0];
                DeleteRoot();
                return result;
            }
            else
            {
                throw new InvalidOperationException("Priority queue is empty");
            }
        }
        
        public KeyValuePair<TPriority, TValue> DequeueLast()
        {
            if (!IsEmpty)
            {
                KeyValuePair<TPriority, TValue> result = _baseHeap[this.Count - 1];
                _baseHeap.RemoveAt(this.Count - 1);
                return result;
            }
            else
            {
                throw new InvalidOperationException("Priority queue is empty");
            }
        }

        public KeyValuePair<TPriority, TValue> PeekLast()
        {
            if (!IsEmpty)
            {
                KeyValuePair<TPriority, TValue> result = _baseHeap[this.Count - 1];
                return result;
            }
            else
            {
                throw new InvalidOperationException("Priority queue is empty");
            }
        }

        private void DeleteRoot()
        {
            if (_baseHeap.Count <= 1)
            {
                _baseHeap.Clear();
                return;
            }

            _baseHeap[0] = _baseHeap[_baseHeap.Count - 1];
            _baseHeap.RemoveAt(_baseHeap.Count - 1);

            // heapify
            HeapifyFromBeginningToEnd(0);
        }

        private void HeapifyFromBeginningToEnd(int pos)
        {
            if (pos >= _baseHeap.Count) return;

            // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];

            while (true)
            {
                // on each iteration exchange element with its smallest child
                int smallest = pos;
                int left = 2 * pos + 1;
                int right = 2 * pos + 2;
                
                if (left < _baseHeap.Count &&
                    _comparer.Compare(_baseHeap[smallest].Key, _baseHeap[left].Key) > 0)
                {
                    smallest = left;
                }

                if (right < _baseHeap.Count &&
                    _comparer.Compare(_baseHeap[smallest].Key, _baseHeap[right].Key) > 0)
                {
                    smallest = right;
                }

                if (smallest != pos)
                {
                    ExchangeElements(smallest, pos);
                    pos = smallest;
                }
                else
                {
                    break;
                }
            }
        }

        public KeyValuePair<TPriority, TValue> Peek()
        {
            if (!IsEmpty)
                return _baseHeap[0];
            else
            {
                throw new InvalidOperationException("Priority queue is empty");
            }
        }

        public TValue PeekValue()
        {
            return Peek().Value;
        }

        public bool IsEmpty
        {
            get { return _baseHeap.Count == 0; }
        }

        public bool Remove(KeyValuePair<TPriority, TValue> item)
        {
            // find element in the collection and remove it
            int elementIdx = _baseHeap.IndexOf(item);
            if (elementIdx < 0) return false;

            //remove element
            _baseHeap[elementIdx] = _baseHeap[_baseHeap.Count - 1];
            _baseHeap.RemoveAt(_baseHeap.Count - 1);

            // heapify
            int newPos = HeapifyFromEndToBeginning(elementIdx);
            if (newPos == elementIdx)
                HeapifyFromBeginningToEnd(elementIdx);

            return true;
        }
    }
}

