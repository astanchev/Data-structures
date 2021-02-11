namespace HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private LinkedList<KeyValue<TKey, TValue>>[] slots;

        public const float LoadFactor = 0.75f;
        public const int InitialCapacity = 16;
        
        public HashTable()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[InitialCapacity];
            this.Count = 0;
        }

        public HashTable(int capacity = InitialCapacity)
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public int Capacity => this.slots.Length;

        public void Add(TKey key, TValue value)
        {
            GrowIfNeeded();

            int slotNumber = this.FindSlotNumber(key);

            if (this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in this.slots[slotNumber])
            {
                if (element.Key.Equals(key))
                {
                    throw new ArgumentException("Key already exists: " + key);
                }
            }

            var newElement = new KeyValue<TKey, TValue>(key, value);

            this.slots[slotNumber].AddLast(newElement);
            this.Count++;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public TValue Get(TKey key)
        {
            throw new NotImplementedException();
            // Note: throw an exception on missing key
        }

        public TValue this[TKey key]
        {
            get
            {
                throw new NotImplementedException();
                // Note: throw an exception on missing key
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private void GrowIfNeeded()
        {
            if ((float)(this.Count + 1) / this.Capacity > LoadFactor)
            {
                // Hash table loaded too much --> resize
                this.Grow();
            }
        }

        private void Grow()
        {
            var newHashTable = new HashTable<TKey, TValue>(2 * this.Capacity);

            foreach (var element in this)
            {
                newHashTable.Add(element.Key, element.Value);
            }

            this.slots = newHashTable.slots;
            this.Count = newHashTable.Count;
        }

        private int FindSlotNumber(TKey key)
        {
            var slotNumber = Math.Abs(key.GetHashCode()) % this.slots.Length;

            return slotNumber;
        }
    }
}
